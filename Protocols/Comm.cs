﻿using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.Remoting.Messaging;
using System.Data;
using System.Net;

namespace Protocols
{
    //基本的接口
    public interface IComm:IDisposable
    {
        string Send(string str);
        byte[] Send(byte[] sendData);
        byte[] Send(Action<Stream> action, byte[] sendData);
        Task<byte[]> SendAsync(byte[] sendData);
        void Close();
    }

    //为减少代码重写的抽象类
    public abstract class AComm : IComm, IDisposable
    {
        protected int waitReadDelay = 0;
        private int bufferSize = 1024;
        protected static SemaphoreSlim sem = new SemaphoreSlim(1, 1);
        private static int _minSemaphore = 1;
        private static int _maxSemaphore = 1;
        protected abstract Stream GetStream();
        protected abstract Stream GetStream(Action<Stream> action);

        /// <summary>
        /// 经过包装的字符串格式发送接收方法
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string Send(string str)
        {
            return Encoding.UTF8.GetString(Send(Encoding.UTF8.GetBytes(str)));
        }

        /// <summary>
        /// 通用场景的收发方法
        /// </summary>
        /// <param name="sendData"></param>
        /// <returns></returns>
        public byte[] Send(byte[] sendData)
        {
            byte[] ret = new byte[bufferSize];//单次读写最多480字对应960字节，加上固定的报文头，1024字节以内             
            sem.Wait();//限制并发连接数
            var s = GetStream();
            s.Write(sendData, 0, sendData.Length);
            Thread.Sleep(waitReadDelay); 
            int n = s.Read(ret, 0, ret.Length);
            sem.Release();
            Array.Resize(ref ret, n);
            return ret;
        }

        /// <summary>
        /// 指定Stream的版本，用于特定情况下发送握手包的场景
        /// </summary>
        /// <param name="s"></param>
        /// <param name="sendData"></param>
        /// <returns></returns>
        public byte[] Send(Stream s,byte[] sendData)
        {
            byte[] ret = new byte[bufferSize];//单次读写最多480字对应960字节，加上固定的报文头，1024字节以内
            //sem.Wait();//限制并发连接数                 
            s.Write(sendData, 0, sendData.Length);
            Thread.Sleep(waitReadDelay);
            int n = s.Read(ret, 0, ret.Length);
            //sem.Release();
            Array.Resize(ref ret, n);
            return ret;
        }

        /// <summary>
        /// 带一个委托的收发方法，适用于需要额外握手的协议
        /// </summary>
        /// <param name="sendData"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public byte[] Send(Action<Stream> action,byte[] sendData)
        {
            byte[] ret = new byte[bufferSize];//单次读写最多480字对应960字节，加上固定的报文头，1024字节以内 
            sem.Wait();//限制并发连接数
            var s = GetStream(action);

            //var s1 = BitConverter.ToString(sendData).Replace("-"," ");

            s.Write(sendData, 0, sendData.Length);
            Thread.Sleep(waitReadDelay);
            int n = s.Read(ret, 0, ret.Length);
            sem.Release();
            Array.Resize(ref ret, n);
            return ret;
        }

        public Task<Byte[]> SendAsync(byte[] sendData)
        {      
            return Task.Run<Byte[]>(() => {
                sem.Wait();//限制并发连接数
                var s = GetStream();
                s.Write(sendData, 0, sendData.Length);
                Thread.Sleep(waitReadDelay);
                //Thread.Sleep(50);
                int n = 0;
                MemoryStream ms = new MemoryStream();
                byte[] ret = new byte[bufferSize];//单次读写最多480字对应960字节，加上固定的报文头，1024字节以内
                int count = 0;
                while (true)
                {
                    Array.Clear(ret, 0, ret.Length);
                    n = s.Read(ret, 0, ret.Length);
                    ms.Write(ret, 0, n);

                    if (ret.Length > 5)
                    {
                        sem.Release();
                        return Task.FromResult<Byte[]>(ret);
                    }
                    count++;
                    if (count > 500)
                    {
                        sem.Release();
                        throw new Exception("接收超时！");
                        //return Task.FromResult<Byte[]>(Array.Empty<byte>());
                    }
                    Thread.Sleep(1);
                }                
            });            
        }
        public abstract void Close();
        public abstract void Dispose();  
    }

    //通讯层父类-可选串口或以太网
    public class CommNet : AComm, IComm
    {
        private string _ip;
        private int _port;
        private int _timeOut = 1000;

        private TcpClient client =null;
        readonly object lckObj = new Object();

        public string LocalIp = "";
        public string RemoteIp = "";

        //带IP，端口号设置的构造函数
        public CommNet(string ip, int port)
        {
            _ip = ip;
            _port = port;
        }

        //带IP，端口号，超时时间设置的构造函数
        public CommNet(string ip, int port, int timeOut)
        {
            _ip = ip;
            _port = port;
            _timeOut = timeOut;
        }

        //带IP，端口号，超时时间，以及信号量最小值最大值设置的构造函数
        public CommNet(string ip, int port, int timeOut, int minSemaphore, int maxSemaphore)
        {
            _ip = ip;
            _port = port;
            _timeOut = timeOut;
            sem = new SemaphoreSlim(minSemaphore, maxSemaphore);
        }

        ~CommNet()
        {
            client.Close();
            client.Dispose();
        }

        //获取Tcp连接的Stream
        protected override Stream GetStream()
        {
            lock (lckObj)
            {
                waitReadDelay = 0;
                if (client == null) client = new TcpClient();
                client.ReceiveTimeout = _timeOut;
                if (!client.Connected) client.Connect(_ip, _port);
                if (client.Connected) return client.GetStream();

                LocalIp = ((IPEndPoint)client.Client.LocalEndPoint).Address.ToString();
                RemoteIp = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
                return null;
            }
        }

        /// <summary>
        /// 获取连接的Stream，带委托的版本，适用于需要独立握手的协议
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        protected override Stream GetStream(Action<Stream> action)
        {
            lock (lckObj)
            {
                waitReadDelay = 0;
                if (client == null)
                {
                    client = new TcpClient();
                    client.Connect(_ip, _port);
                    LocalIp = ((IPEndPoint)client.Client.LocalEndPoint).Address.ToString();
                    RemoteIp = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();

                    //应在此时调用父类的方法发送握手包
                    if (client.Connected)
                    {
                        action?.Invoke(client.GetStream());//调用委托发送握手包
                    }
                }
                
                if (!client.Connected) client.Connect(_ip, _port);
                if (client.Connected)
                {
                    client.ReceiveTimeout = _timeOut;

                    return client.GetStream();
                }

                
                return null;
            }
        }

        public override void Close()
        {
            if (client != null && client.Connected) client?.Close(); 
        }

        public override void Dispose()
        {            
            try
            {                
                client?.Close();
                client?.Dispose();
                client = null;
            }
            catch (Exception)
            {
                //throw;
            }
        }
    }

    //通讯层父类-可选串口或以太网
    public class CommSerialPort : AComm, IComm
    {
        private string _portName;
        private int _baudRate;
        private int _dataBits;
        private Parity _parity;
        private StopBits _stopBits;
        private int _timeOut = 1000;

        private SerialPort sp ;
        readonly object lckObj = new Object();

        //最简构造函数
        public CommSerialPort(string portName, int baudRate, int dataBits, Parity parity, StopBits stopBits)
        {
            _portName = portName;
            _baudRate = baudRate;
            _parity = parity;
            _dataBits = dataBits;
            _stopBits = stopBits;
        }

        //不带信号量初始的构造函数
        public CommSerialPort(string portName, int baudRate, int dataBits, Parity parity, StopBits stopBits, int timeOut)
        {
            _portName = portName;
            _baudRate = baudRate;
            _parity = parity;
            _dataBits = dataBits;
            _stopBits = stopBits;
            _timeOut = timeOut;
        }

        //全参构造函数
        public CommSerialPort(string portName, int baudRate, int dataBits, Parity parity, StopBits stopBits, int timeOut, int minSemaphore, int maxSemaphore)
        {
            _portName = portName;
            _baudRate = baudRate;
            _parity = parity;
            _dataBits = dataBits;
            _stopBits = stopBits;
            _timeOut = timeOut;
            sem = new SemaphoreSlim(minSemaphore, maxSemaphore);
        }

        ~CommSerialPort()
        {
            sp?.Close();
            sp?.Dispose();
        }

        //获取Tcp连接的Stream
        protected override Stream GetStream()
        {
            lock (lckObj)
            {
                waitReadDelay = 50;
                //判断串口是否存在
                var ports = SerialPort.GetPortNames();
                if (!ports.Any(p => p.Contains(_portName))) throw new InvalidDataException($"串口:{_portName}不存在");
                if (sp == null)
                {
                    sp = new SerialPort();
                    sp.PortName = _portName;
                    sp.BaudRate = _baudRate;
                    sp.DataBits = _dataBits;
                    sp.Parity = _parity;
                    sp.StopBits = _stopBits;
                }
                if (!sp.IsOpen) sp.Open();
                sp.WriteTimeout = _timeOut;
                sp.ReadTimeout = _timeOut;
                return sp.BaseStream;
            }
        }

        public override void Close()
        {
            if (sp != null && sp.IsOpen) sp?.Close();  
        }

        public override void Dispose()
        {
            try
            { 
                sp?.Close();
                sp?.Dispose();
                sp = null;
            }
            catch (Exception)
            {

                //throw;
            }
        }

        protected override Stream GetStream(Action<Stream> action)
        {
            throw new Exception("串口方式不支持此方法！");
            lock (lckObj)
            {
                waitReadDelay = 50;
                //判断串口是否存在
                var ports = SerialPort.GetPortNames();
                if (!ports.Any(p => p.Contains(_portName))) throw new InvalidDataException($"串口:{_portName}不存在");
                if (sp == null)
                {
                    sp = new SerialPort();
                    sp.PortName = _portName;
                    sp.BaudRate = _baudRate;
                    sp.DataBits = _dataBits;
                    sp.Parity = _parity;
                    sp.StopBits = _stopBits;
                }
                if (!sp.IsOpen) sp.Open();
                sp.WriteTimeout = _timeOut;
                sp.ReadTimeout = _timeOut; 
                return sp.BaseStream;
            }
        }
    }
}
