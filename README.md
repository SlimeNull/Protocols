# Protocols-��ҵ�豸ͨ��Э���
һ��Ϊ��������Ŀ��������������   
��VS2022+.Net Framework4.8 + HslCommunication ���ز���ͨ����ֻ������M,D�Ĵ�����     
Ŀǰֻ֧��TCP��ʽ�ʹ��ڷ�ʽ����ӭ����bug   
��л���º����ṩ�İ�����   
����(https://github.com/kongdetuo)   
SlimeNull(https://github.com/SlimeNull)   
Steve(https://github.com/steveworkshop)   
�ֵ���(https://github.com/lindexi)   
������Bot   
��Ů֮��(https://github.com/ilyfairy)   
Shompinice(https://github.com/MicaApps)   
      
20240819������ModbusRTU��    
20240820������ModbusASCII��ModbusTCP����������Mewtocol��д���λʱ����bugδ�޸�����   
20240828�������˽ṹ��������ŷķ��HostLink_Serial��ʽ��ʹ��ͷ����FA��������ͷ�����Fins��ʽ���������   
20240901��������ŷķ��FinsTCP��ʽ
```
                
        static void Main(string[] args)
        {
            //TestMC(new MC_3E("127.0.0.1", 6000));

            //TestMewtocol(new Mewtocol(new CommSerialPort("COM1", 9600, 8, Parity.None, StopBits.One)));

            //TestOmron(new HostLink_Serial(new CommSerialPort("COM1", 9600, 7, Parity.Even, StopBits.One)));
            TestMC(new HostLink_Serial(new CommSerialPort("COM1", 9600, 7, Parity.Even, StopBits.One)));

            //TestModbus(new ASCII(new CommSerialPort("COM1", 9600, 8, Parity.None, StopBits.One)));
            //TestModbus(new RTU(new CommSerialPort("COM1", 9600, 8, Parity.None, StopBits.One)));
            //TestModbus(new TCP(new CommNet("127.0.0.1", 502)));

        }

        static void TestOmron(ProtocolBase m)
        {
            //������д
            //������д
            m.WriteBool("D", 10000, true).Dump("WriteBool");
            m.ReadBool("D", 10000).Dump("ReadBool");

            //16λ��д
            m.WriteInt16("D", 100, -1).Dump("WriteInt16");
            m.ReadInt16("D", 100).Dump("ReadInt16");

            m.WriteUInt16("D", 100, 1).Dump("WriteUInt16");
            m.ReadUInt16("D", 100).Dump("ReadUInt16");

            //32λ��д
            m.WriteInt32("D", 100, -1111).Dump("WriteInt32");
            m.ReadInt32("D", 100).Dump("ReadInt32");

            m.WriteUInt32("D", 100, 1111).Dump("WriteUInt32");
            m.ReadUInt32("D", 100).Dump("ReadUInt32");

            //��������д
            m.WriteSingle("D", 100, -11.11f).Dump("WriteSingle");
            m.ReadSingle("D", 100).Dump("ReadSingle");

            //�ַ�����д
            m.WriteString("D", 100, "abcdefghijklmnopqrstuvwxyz").Dump("WriteString");
            m.ReadString("D", 100, 26).Dump("ReadString");

            //�����д
            //������д
            m.WriteBool("D", 10000, new bool[] { true,false,true,false,true }).Dump("WriteBool");
            m.ReadBool("D", 10000, 5).Dump("ReadBool");

            //16λ��д
            m.WriteInt16("D",100,new short[] {-11,-22,-33,-44,-55 }).Dump("WriteInt16");
            m.ReadInt16("D", 100, 5).Dump("ReadInt16");

            m.WriteUInt16("D", 100, new ushort[] { 11, 22, 33, 44, 55 }).Dump("WriteUInt16");
            m.ReadUInt16("D", 100, 5).Dump("ReadUInt16");

            //32λ��д
            m.WriteInt32("D", 100, new int[] { -1111, -2222, -3333, -4444, -5555 }).Dump("WriteInt32");
            m.ReadInt32("D", 100, 5).Dump("ReadInt32");

            m.WriteUInt32("D", 100, new uint[] { 1111, 2222, 3333, 4444, 5555 }).Dump("WriteUInt32");
            m.ReadUInt32("D", 100, 5).Dump("ReadUInt32");

            //��������д
            m.WriteSingle("D", 100, new float[] { -11.11f, -22.22f, -33.33f, -44.44f, -55.55f }).Dump("WriteSingle");
            m.ReadSingle("D", 100, 5).Dump("ReadSingle");

            //�ַ�����д
            m.WriteString("D", 100, "abcdefghijklmnopqrstuvwxyz").Dump("WriteString");
            m.ReadString("D", 100, 26).Dump("ReadString");

            Console.Read();

        }


        static void TestModbus(ModbusBase m)
        {

            "".Dump("��д�����Ĵ���"); 
            m.WriteSingleCoil(1, 100, true).Dump();//ֻ��д��Ȧ
            m.ReadCoils(1, 100, 1).Dump();//�˴�Ҳֻ�ܶ���Ȧ������ɢ���������� 
            m.WriteSingleCoil(1, 100, false).Dump();//ֻ��д��Ȧ
            m.ReadCoils(1, 100, 1).Dump();//�˴�Ҳֻ�ܶ���Ȧ������ɢ����������

            m.WriteSingleCoil(1, 100, true);
             
            m.WriteMultipleRegisters<Int16>(1, (Int16)100, new Int16[] { 1234 }).Dump(); 
            m.ReadHoldingRegisters<Int16>(1, (Int16)100, 1).Dump();
             
            m.WriteMultipleRegisters<UInt16>(1, (Int16)100, new UInt16[] { 1234 }).Dump(); 
            m.ReadHoldingRegisters<UInt16>(1, (Int16)100, 1).Dump();
             
            m.WriteMultipleRegisters<Int32>(1, (Int16)100, new Int32[] { 1234 }).Dump(); 
            m.ReadHoldingRegisters<Int32>(1, (Int16)100, 1).Dump();
             
            m.WriteMultipleRegisters<UInt32>(1, (Int16)100, new UInt32[] { 1234 }).Dump(); 
            m.ReadHoldingRegisters<UInt32>(1, (Int16)100, 1).Dump();
             
            m.WriteMultipleRegisters<Single>(1, (Int16)100, new Single[] { 3.141592f }).Dump(); 
            m.ReadHoldingRegisters<Single>(1, (Int16)100, 1).Dump();


            "��д����Ĵ���".Dump("��д����Ĵ���"); 
            m.WriteMultipleCoils(1, 100, new bool[] { true, false, true, false, true, true, false, true, false, true, true, false, true, false, true }).Dump(); 
            m.ReadCoils(1, 100, 20).Dump();
             
            m.WriteMultipleRegisters<Int16>(1, (Int16)100, new Int16[] { 1234, 1234, 1234, 1234, 1234 }).Dump(); 
            m.ReadHoldingRegisters<Int16>(1, (Int16)100, 5).Dump();
             
            m.WriteMultipleRegisters<UInt16>(1, (Int16)100, new UInt16[] { 1234, 1234, 1234, 1234, 1234 }).Dump(); 
            m.ReadHoldingRegisters<UInt16>(1, (Int16)100, 5).Dump();

 
            m.WriteMultipleRegisters<Int32>(1, (Int16)100, new Int32[] { 1234, 1234, 1234, 1234, 1234 }).Dump(); 
            m.ReadHoldingRegisters<Int32>(1, (Int16)100, 5).Dump();
             
            m.WriteMultipleRegisters<UInt32>(1, (Int16)100, new UInt32[] { 1234, 1234, 1234, 1234, 1234 }).Dump(); 
            m.ReadHoldingRegisters<UInt32>(1, (Int16)100, 5).Dump();

             
            m.WriteMultipleRegisters<Single>(1, (Int16)100, new Single[] { 3.141592f, 3.141592f, 3.141592f, 3.141592f, 3.141592f }).Dump(); 
            m.ReadHoldingRegisters<Single>(1, (Int16)100, 5).Dump();


            "�ַ�����д".Dump("�ַ�����д");
            m.WriteMultipleRegisters<string>(1, 100, new string[] { "abcdefg" }).Dump();
            m.ReadHoldingRegisters<string>(1, 100, 10).Dump();

            "Done.".Dump();
            Console.ReadLine();
        }

        static void TestMC(ProtocolBase m)
        {
            //��������
            try
            { 
                //MC_3E mc = new MC_3E("COM1",9600,8,Parity.None,StopBits.One);

                //����ƴ�ӷ�ʽ-������
                //MC_3E2 mc = new MC_3E2("127.0.0.1", 6000);
                //MC_3E mc = new MC_3E("COM1", 9600, 8, Parity.None, StopBits.One);

                Console.WriteLine("�����������ԣ�");
                Console.WriteLine("��д����Ԫ��");
                m.WriteBool("M", 100, true).Dump("д����ֵ��");
                m.ReadBool("M", 100).Dump("������ֵ��");
                 
                m.WriteInt16("D", 100, 1234).Dump("д�з����֣�");
                m.ReadInt16("D", 100).Dump("�������֣�");
                 
                m.WriteUInt16("D", 100, 1234).Dump("д�޷����֣�");
                m.ReadUInt16("D", 100).Dump("���޷����֣�");
                 
                m.WriteInt32("D", 100, 1234567).Dump("д�з���˫�֣�");
                m.ReadInt32("D", 100).Dump("���з���˫�֣�");
                 
                m.WriteUInt32("D", 100, 1234567).Dump("д�޷���˫�֣�");
                m.ReadUInt32("D", 100).Dump("���޷���˫�֣�");
                 
                m.WriteSingle("D", 100, 3.141592653f).Dump("д��������");
                m.ReadSingle("D", 100).Dump("����������");


                Console.WriteLine("��д���Ԫ��"); 
                m.WriteBool("M", 100, new bool[] { true, false, true, false, true }).Dump("д5������ֵ��");
                m.ReadBool("M", 100, 5).Dump("��5������ֵ��");
                 
                m.WriteInt16("D", 100, new Int16[] { 1, 2, 3, 4, 5 }).Dump("д5���з����֣�");
                m.ReadInt16("D", 100, 5).Dump("��5�������֣�");
                 
                m.WriteUInt16("D", 100, new UInt16[] { 1, 2, 3, 4, 5 }).Dump("д5���޷����֣�");
                m.ReadUInt16("D", 100, 5).Dump("��5���޷����֣�");
                 
                m.WriteInt32("D", 100, new Int32[] { 12, 23, 34, 45, 56 }).Dump("д5���з���˫�֣�");
                m.ReadInt32("D", 100, 5).Dump("��5���з���˫�֣�");
                 
                m.WriteUInt32("D", 100, new UInt32[] { 11, 22, 33, 44, 55 }).Dump("д5���޷���˫�֣�");
                m.ReadUInt32("D", 100, 5).Dump("��5���޷���˫�֣�");
                 
                m.WriteSingle("D", 100, new Single[] { 1.1f, 2.2f, 3.3f, 4.4f, 5.5f }).Dump("д5����������");
                m.ReadSingle("D", 100, 5).Dump("��5����������");
                 
                m.WriteString("D", 100, "abcdefghij").Dump("д�ַ�����");
                m.ReadString("D", 100, 10).Dump("���ַ�����");


                Console.WriteLine("���ͷ�������");
                Console.WriteLine("��д����Ԫ��");
                
                m.WriteData<bool>("M", 100, (object)true).Dump("����д����ֵ");
                m.ReadData<bool>("M", 100).Dump("���Ͷ�����ֵ");
                 
                m.WriteData<Int16>("D", 100, (object)1234).Dump("����дINT16");
                m.ReadData<Int16>("D", 100).Dump("���Ͷ�INT16");
                 
                m.WriteData<UInt16>("D", 100, (object)1234).Dump("����дUINT16");
                m.ReadData<UInt16>("D", 100).Dump("���Ͷ�UINT16");
                 
                m.WriteData<Int32>("D", 100, (object)12345678).Dump("����дINT32");
                m.ReadData<Int32>("D", 100).Dump("���Ͷ�INT16");
                 
                m.WriteData<UInt32>("D", 100, (object)12345678).Dump("����дUINT32");
                m.ReadData<UInt32>("D", 100).Dump("���Ͷ�UINT16");
                 
                m.WriteData<Single>("D", 100, (object)1.2345678).Dump("����дINT32");
                m.ReadData<Single>("D", 100).Dump("���Ͷ�INT16");
                 
                m.WriteData<string>("D", 100, (object)"kkkkkkkkkkk").Dump("����дstring");
                m.ReadData<string>("D", 100, 10).Dump("���Ͷ�string");


                //��д���Ԫ��
                Console.WriteLine("��д���Ԫ��"); 
                m.WriteData<bool[]>("M", 100, (object)new bool[] { true, false, true, false, true }).Dump("����д5����ֵ");
                m.ReadData<bool[]>("M", 100, 5).Dump("���Ͷ�5����ֵ");
                 
                m.WriteData<Int16[]>("D", 100, (object)new Int16[] { 1, 2, 3, 4, 5 }).Dump("����д5INT16");
                m.ReadData<Int16[]>("D", 100, 5).Dump("���Ͷ�5INT16");
                 
                m.WriteData<UInt16[]>("D", 100, (object)new UInt16[] { 1, 2, 3, 4, 5 }).Dump("����д5UINT16");
                m.ReadData<UInt16[]>("D", 100, 5).Dump("���Ͷ�5UINT16");
                 
                m.WriteData<Int32[]>("D", 100, (object)new Int32[] { 11, 22, 33, 44, 55 }).Dump("����д5INT32");
                m.ReadData<Int32[]>("D", 100, 5).Dump("���Ͷ�5INT16");
                 
                m.WriteData<UInt32[]>("D", 100, (object)new UInt32[] { 12, 34, 56, 78, 90 }).Dump("����д5UINT32");
                m.ReadData<UInt32[]>("D", 100, 5).Dump("���Ͷ�5UINT16");
                 
                m.WriteData<Single[]>("D", 100, (object)new Single[] { 1.1f, 2.2f, 3.3f, 4.4f, 5.5f }).Dump("����д5INT32");
                m.ReadData<Single[]>("D", 100, 5).Dump("���Ͷ�5INT16");


                //�����첽��������-------------------------------------------------------------------------------------
                //��д����Ԫ��
                Console.WriteLine("�����첽��������--------------------------------------------------------------------");
                Console.WriteLine("��д����Ԫ��"); 
                m.WriteDataAsync<bool>("M", 100, (object)true).Result.Dump("����д����ֵ");
                m.ReadDataAsync<bool>("M", 100).Result.Dump("���Ͷ�����ֵ");
                 
                m.WriteDataAsync<Int16>("D", 100, (object)1234).Result.Dump("����дINT16");
                m.ReadDataAsync<Int16>("D", 100).Result.Dump("���Ͷ�INT16");
                 
                m.WriteDataAsync<UInt16>("D", 100, (object)1234).Result.Dump("����дUINT16");
                m.ReadDataAsync<UInt16>("D", 100).Result.Dump("���Ͷ�UINT16");
                 
                m.WriteDataAsync<Int32>("D", 100, (object)12345678).Result.Dump("����дINT32");
                m.ReadDataAsync<Int32>("D", 100).Result.Dump("���Ͷ�INT16");
                 
                m.WriteDataAsync<UInt32>("D", 100, (object)12345678).Result.Dump("����дUINT32");
                m.ReadDataAsync<UInt32>("D", 100).Result.Dump("���Ͷ�UINT16");
                 
                m.WriteDataAsync<Single>("D", 100, (object)1.2345678).Result.Dump("����дINT32");
                m.ReadDataAsync<Single>("D", 100).Result.Dump("���Ͷ�INT16");
                 
                m.WriteDataAsync<string>("D", 100, (object)"kkkkkkkkkkk").Result.Dump("����дstring");
                m.ReadDataAsync<string>("D", 100, 10).Result.Dump("���Ͷ�string");

                 
                Console.WriteLine("��д���Ԫ��"); 
                m.WriteDataAsync<bool[]>("M", 100, (object)new bool[] { true, false, true, false, true }).Result.Dump("����д5����ֵ");
                m.ReadDataAsync<bool[]>("M", 100, 5).Result.Dump("���Ͷ�5����ֵ");
                 
                m.WriteDataAsync<Int16[]>("D", 100, (object)new Int16[] { 1, 2, 3, 4, 5 }).Result.Dump("����д5INT16");
                m.ReadDataAsync<Int16[]>("D", 100, 5).Result.Dump("���Ͷ�5INT16");
                 
                m.WriteDataAsync<UInt16[]>("D", 100, (object)new UInt16[] { 1, 2, 3, 4, 5 }).Result.Dump("����д5UINT16");
                m.ReadDataAsync<UInt16[]>("D", 100, 5).Result.Dump("���Ͷ�5UINT16");
                 
                m.WriteDataAsync<Int32[]>("D", 100, (object)new Int32[] { 11, 22, 33, 44, 55 }).Result.Dump("����д5INT32");
                m.ReadDataAsync<Int32[]>("D", 100, 5).Result.Dump("���Ͷ�5INT16");
                 
                m.WriteDataAsync<UInt32[]>("D", 100, (object)new UInt32[] { 12, 34, 56, 78, 90 }).Result.Dump("����д5UINT32");
                m.ReadDataAsync<UInt32[]>("D", 100, 5).Result.Dump("���Ͷ�5UINT16");
                 
                m.WriteDataAsync<Single[]>("D", 100, (object)new Single[] { 1.1f, 2.2f, 3.3f, 4.4f, 5.5f }).Result.Dump("����д5INT32");
                m.ReadDataAsync<Single[]>("D", 100, 5).Result.Dump("���Ͷ�5INT16");

                Console.WriteLine("Done.");

                Console.Read();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        static void TestMewtocol(ProtocolBase m)
        {
            //��������
            try
            {
                //MC_3E mc = new MC_3E("COM1",9600,8,Parity.None,StopBits.One);

                //����ƴ�ӷ�ʽ-������
                //MC_3E2 mc = new MC_3E2("127.0.0.1", 6000);
                //MC_3E mc = new MC_3E("COM1", 9600, 8, Parity.None, StopBits.One);

                Console.WriteLine("�����������ԣ�");

                Console.WriteLine("��д����Ԫ��"); 
                m.WriteBool("R", 0x100, true).Dump("д����ֵ��");
                m.ReadBool("R", 0x100).Dump("������ֵ��");
                 
                m.WriteInt16("D", 100, 1234).Dump("д�з����֣�");
                m.ReadInt16("D", 100).Dump("�������֣�");
                 
                m.WriteUInt16("D", 100, 1234).Dump("д�޷����֣�");
                m.ReadUInt16("D", 100).Dump("���޷����֣�");
                 
                m.WriteInt32("D", 100, 1234567).Dump("д�з���˫�֣�");
                m.ReadInt32("D", 100).Dump("���з���˫�֣�");
                 
                m.WriteUInt32("D", 100, 1234567).Dump("д�޷���˫�֣�");
                m.ReadUInt32("D", 100).Dump("���޷���˫�֣�");
                 
                m.WriteSingle("D", 100, 3.141592653f).Dump("д��������");
                m.ReadSingle("D", 100).Dump("����������");


                Console.WriteLine("��д���Ԫ��"); 
                m.WriteBool("R", 0x100, new bool[] { true, false, true, false, true }).Dump("д5������ֵ��");
                m.ReadBool("R", 0x100, 5).Dump("��5������ֵ��");
                 
                m.WriteInt16("D", 100, new Int16[] { 1, 2, 3, 4, 5 }).Dump("д5���з����֣�");
                m.ReadInt16("D", 100, 5).Dump("��5�������֣�");
                 
                m.WriteUInt16("D", 100, new UInt16[] { 1, 2, 3, 4, 5 }).Dump("д5���޷����֣�");
                m.ReadUInt16("D", 100, 5).Dump("��5���޷����֣�");
                 
                m.WriteInt32("D", 100, new Int32[] { 12, 23, 34, 45, 56 }).Dump("д5���з���˫�֣�");
                m.ReadInt32("D", 100, 5).Dump("��5���з���˫�֣�");
                 
                m.WriteUInt32("D", 100, new UInt32[] { 11, 22, 33, 44, 55 }).Dump("д5���޷���˫�֣�");
                m.ReadUInt32("D", 100, 5).Dump("��5���޷���˫�֣�");
                 
                m.WriteSingle("D", 100, new Single[] { 1.1f, 2.2f, 3.3f, 4.4f, 5.5f }).Dump("д5����������");
                m.ReadSingle("D", 100, 5).Dump("��5����������");
                 
                m.WriteString("D", 100, "abcdefghij").Dump("д�ַ�����");
                m.ReadString("D", 100, 10).Dump("���ַ�����");


                //���ͷ�������
                //��д����Ԫ��
                Console.WriteLine("��д����Ԫ��"); 
                m.WriteData<bool>("R", 0x100, (object)true).Dump("����д����ֵ");
                m.ReadData<bool>("R", 0x100).Dump("���Ͷ�����ֵ");
                 
                m.WriteData<Int16>("D", 100, (object)1234).Dump("����дINT16");
                m.ReadData<Int16>("D", 100).Dump("���Ͷ�INT16");
                 
                m.WriteData<UInt16>("D", 100, (object)1234).Dump("����дUINT16");
                m.ReadData<UInt16>("D", 100).Dump("���Ͷ�UINT16");
                 
                m.WriteData<Int32>("D", 100, (object)12345678).Dump("����дINT32");
                m.ReadData<Int32>("D", 100).Dump("���Ͷ�INT16");
                 
                m.WriteData<UInt32>("D", 100, (object)12345678).Dump("����дUINT32");
                m.ReadData<UInt32>("D", 100).Dump("���Ͷ�UINT16");
                 
                m.WriteData<Single>("D", 100, (object)1.2345678).Dump("����дINT32");
                m.ReadData<Single>("D", 100).Dump("���Ͷ�INT16");
                 
                m.WriteData<string>("D", 100, (object)"kkkkkkkkkkk").Dump("����дstring");
                m.ReadData<string>("D", 100, 10).Dump("���Ͷ�string");


                //��д���Ԫ��
                Console.WriteLine("��д���Ԫ��"); 
                m.WriteData<bool[]>("R", 0x100, (object)new bool[] { true, false, true, false, true }).Dump("����д5����ֵ");
                m.ReadData<bool[]>("R", 0x100, 5).Dump("���Ͷ�5����ֵ");
                 
                m.WriteData<Int16[]>("D", 100, (object)new Int16[] { 1, 2, 3, 4, 5 }).Dump("����д5INT16");
                m.ReadData<Int16[]>("D", 100, 5).Dump("���Ͷ�5INT16");
                 
                m.WriteData<UInt16[]>("D", 100, (object)new UInt16[] { 1, 2, 3, 4, 5 }).Dump("����д5UINT16");
                m.ReadData<UInt16[]>("D", 100, 5).Dump("���Ͷ�5UINT16");
                 
                m.WriteData<Int32[]>("D", 100, (object)new Int32[] { 11, 22, 33, 44, 55 }).Dump("����д5INT32");
                m.ReadData<Int32[]>("D", 100, 5).Dump("���Ͷ�5INT16");
                 
                m.WriteData<UInt32[]>("D", 100, (object)new UInt32[] { 12, 34, 56, 78, 90 }).Dump("����д5UINT32");
                m.ReadData<UInt32[]>("D", 100, 5).Dump("���Ͷ�5UINT16");
                 
                m.WriteData<Single[]>("D", 100, (object)new Single[] { 1.1f, 2.2f, 3.3f, 4.4f, 5.5f }).Dump("����д5INT32");
                m.ReadData<Single[]>("D", 100, 5).Dump("���Ͷ�5INT16");


                //�����첽��������-------------------------------------------------------------------------------------
                //��д����Ԫ��
                Console.WriteLine("�����첽��������--------------------------------------------------------------------");
                Console.WriteLine("��д����Ԫ��"); 
                m.WriteDataAsync<bool>("R", 0x100, (object)true).Result.Dump("����д����ֵ");
                m.ReadDataAsync<bool>("R", 0x100).Result.Dump("���Ͷ�����ֵ");
                 
                m.WriteDataAsync<Int16>("D", 100, (object)1234).Result.Dump("����дINT16");
                m.ReadDataAsync<Int16>("D", 100).Result.Dump("���Ͷ�INT16");
                 
                m.WriteDataAsync<UInt16>("D", 100, (object)1234).Result.Dump("����дUINT16");
                m.ReadDataAsync<UInt16>("D", 100).Result.Dump("���Ͷ�UINT16");
                 
                m.WriteDataAsync<Int32>("D", 100, (object)12345678).Result.Dump("����дINT32");
                m.ReadDataAsync<Int32>("D", 100).Result.Dump("���Ͷ�INT16");
                 
                m.WriteDataAsync<UInt32>("D", 100, (object)12345678).Result.Dump("����дUINT32");
                m.ReadDataAsync<UInt32>("D", 100).Result.Dump("���Ͷ�UINT16");
                 
                m.WriteDataAsync<Single>("D", 100, (object)1.2345678).Result.Dump("����дINT32");
                m.ReadDataAsync<Single>("D", 100).Result.Dump("���Ͷ�INT16");
                 
                m.WriteDataAsync<string>("D", 100, (object)"kkkkkkkkkkk").Result.Dump("����дstring");
                m.ReadDataAsync<string>("D", 100, 10).Result.Dump("���Ͷ�string");


                //��д���Ԫ��
                Console.WriteLine("��д���Ԫ��"); 
                m.WriteDataAsync<bool[]>("R", 100, (object)new bool[] { true, false, true, false, true }).Result.Dump("����д5����ֵ");
                m.ReadDataAsync<bool[]>("R", 100, 5).Result.Dump("���Ͷ�5����ֵ");
                 
                m.WriteDataAsync<Int16[]>("D", 100, (object)new Int16[] { 1, 2, 3, 4, 5 }).Result.Dump("����д5INT16");
                m.ReadDataAsync<Int16[]>("D", 100, 5).Result.Dump("���Ͷ�5INT16");
                 
                m.WriteDataAsync<UInt16[]>("D", 100, (object)new UInt16[] { 1, 2, 3, 4, 5 }).Result.Dump("����д5UINT16");
                m.ReadDataAsync<UInt16[]>("D", 100, 5).Result.Dump("���Ͷ�5UINT16");
                 
                m.WriteDataAsync<Int32[]>("D", 100, (object)new Int32[] { 11, 22, 33, 44, 55 }).Result.Dump("����д5INT32");
                m.ReadDataAsync<Int32[]>("D", 100, 5).Result.Dump("���Ͷ�5INT16");
                 
                m.WriteDataAsync<UInt32[]>("D", 100, (object)new UInt32[] { 12, 34, 56, 78, 90 }).Result.Dump("����д5UINT32");
                m.ReadDataAsync<UInt32[]>("D", 100, 5).Result.Dump("���Ͷ�5UINT16");
                 
                m.WriteDataAsync<Single[]>("D", 100, (object)new Single[] { 1.1f, 2.2f, 3.3f, 4.4f, 5.5f }).Result.Dump("����д5INT32");
                m.ReadDataAsync<Single[]>("D", 100, 5).Result.Dump("���Ͷ�5INT16");

                Console.WriteLine("Done.");

                Console.Read();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
```