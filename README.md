##ʾ��ΪTCPͨ�ţ���ʱ֧��TCP��SerialPort,��.NetFramework4.8+hlsCommunication�в���ͨ��
```C#
		MC_3E mc = new MC_3E("127.0.0.1", 6000);
		Console.WriteLine("�����������ԣ�");

		Console.WriteLine("��д����Ԫ��");
		Console.WriteLine("λԪ����");
		mc.WriteBool("M", 100, true).Dump("д����ֵ��");
		mc.ReadBool("M", 100).Dump("������ֵ��");

		Console.WriteLine("�з���16λ��Ԫ����");
		mc.WriteInt16("D", 100, 1234).Dump("д�з����֣�");
		mc.ReadInt16("D", 100).Dump("�������֣�");

		Console.WriteLine("�޷���16λ��Ԫ����");
		mc.WriteUInt16("D", 100, 1234).Dump("д�޷����֣�");
		mc.ReadUInt16("D", 100).Dump("���޷����֣�");

		Console.WriteLine("�з���32λ��Ԫ����");
		mc.WriteInt32("D", 100, 1234567).Dump("д�з���˫�֣�");
		mc.ReadInt32("D", 100).Dump("���з���˫�֣�");

		Console.WriteLine("�޷���32λ��Ԫ����");
		mc.WriteUInt32("D", 100,1234567).Dump("д�޷���˫�֣�");
		mc.ReadUInt32("D", 100).Dump("���޷���˫�֣�");

		Console.WriteLine("���Ÿ�������");
		mc.WriteSingle("D", 100, 3.141592653f).Dump("д��������");
		mc.ReadSingle("D", 100).Dump("����������");


		Console.WriteLine("��д���Ԫ��");
		Console.WriteLine("λԪ����");
		mc.WriteBool("M", 100, new bool[] { true, false, true, false, true }).Dump("д5������ֵ��");
		mc.ReadBool("M", 100, 5).Dump("��5������ֵ��");

		Console.WriteLine("�з���16λ��Ԫ����");
		mc.WriteInt16("D", 100, new Int16[] { 1, 2, 3, 4, 5 }).Dump("д5���з����֣�");
		mc.ReadInt16("D", 100, 5).Dump("��5�������֣�");

		Console.WriteLine("�޷���16λ��Ԫ����");
		mc.WriteUInt16("D", 100, new UInt16[] { 1, 2, 3, 4, 5 }).Dump("д5���޷����֣�");
		mc.ReadUInt16("D", 100, 5).Dump("��5���޷����֣�");

		Console.WriteLine("�з���32λ��Ԫ����");
		mc.WriteInt32("D", 100, new Int32[] { 12, 23, 34, 45, 56 }).Dump("д5���з���˫�֣�");
		mc.ReadInt32("D", 100, 5).Dump("��5���з���˫�֣�");
		
		Console.WriteLine("�޷���32λ��Ԫ����");
		mc.WriteUInt32("D", 100, new UInt32[] { 11, 22, 33, 44, 55 }).Dump("д5���޷���˫�֣�");
		mc.ReadUInt32("D", 100, 5).Dump("��5���޷���˫�֣�");

		Console.WriteLine("���Ÿ�������");
		mc.WriteSingle("D", 100, new Single[] { 1.1f, 2.2f, 3.3f, 4.4f, 5.5f }).Dump("д5����������");
		mc.ReadSingle("D", 100, 5).Dump("��5����������");

		Console.WriteLine("��д�ַ�����");
		mc.WriteString("D", 100, "abcdefghij").Dump("д�ַ�����");
		mc.ReadString("D", 100, 10).Dump("���ַ�����");
		
		
		//���ͷ�������
		//��д����Ԫ��
		Console.WriteLine("���Ͳ���ֵ����");
		mc.WriteData<bool>("M",100,(object)true).Dump("����д����ֵ");
		mc.ReadData<bool>("M",100).Dump("���Ͷ�����ֵ");
		
		Console.WriteLine("����INT16����");
		mc.WriteData<Int16>("D", 100, (object)1234).Dump("����дINT16");
		mc.ReadData<Int16>("D", 100).Dump("���Ͷ�INT16");
		
		Console.WriteLine("����UINT16����");
		mc.WriteData<UInt16>("D", 100, (object)1234).Dump("����дUINT16");
		mc.ReadData<UInt16>("D", 100).Dump("���Ͷ�UINT16");

		Console.WriteLine("����INT32����");
		mc.WriteData<Int32>("D", 100, (object)12345678).Dump("����дINT32");
		mc.ReadData<Int32>("D", 100).Dump("���Ͷ�INT16");

		Console.WriteLine("����UINT32����");
		mc.WriteData<UInt32>("D", 100, (object)12345678).Dump("����дUINT32");
		mc.ReadData<UInt32>("D", 100).Dump("���Ͷ�UINT16");

		Console.WriteLine("����FLOAT����");
		mc.WriteData<Single>("D", 100, (object)1.2345678).Dump("����дINT32");
		mc.ReadData<Single>("D", 100).Dump("���Ͷ�INT16");

		Console.WriteLine("����String����");
		mc.WriteData<string>("D", 100, (object)"kkkkkkkkkkk").Dump("����дstring");
		mc.ReadData<string>("D", 100,10).Dump("���Ͷ�string");


		//��д���Ԫ��
		Console.WriteLine("���Ͳ���ֵ����");
		mc.WriteData<bool[]>("M", 100, (object)new bool[] {true,false,true,false,true}).Dump("����д5����ֵ");
		mc.ReadData<bool[]>("M", 100,5).Dump("���Ͷ�5����ֵ");

		Console.WriteLine("����INT16����");
		mc.WriteData<Int16[]>("D", 100, (object)new Int16[]{1,2,3,4,5}).Dump("����д5INT16");
		mc.ReadData<Int16[]>("D", 100,5).Dump("���Ͷ�5INT16");

		Console.WriteLine("����UINT16����");
		mc.WriteData<UInt16[]>("D", 100, (object)new UInt16[] {1,2,3,4,5}).Dump("����д5UINT16");
		mc.ReadData<UInt16[]>("D", 100,5).Dump("���Ͷ�5UINT16");

		Console.WriteLine("����INT32����");
		mc.WriteData<Int32[]>("D", 100, (object)new Int32[] {11,22,33,44,55}).Dump("����д5INT32");
		mc.ReadData<Int32[]>("D", 100,5).Dump("���Ͷ�5INT16");

		Console.WriteLine("����UINT32����");
		mc.WriteData<UInt32[]>("D", 100, (object)new UInt32[] {12,34,56,78,90}).Dump("����д5UINT32");
		mc.ReadData<UInt32[]>("D", 100,5).Dump("���Ͷ�5UINT16");

		Console.WriteLine("����FLOAT����");
		mc.WriteData<Single[]>("D", 100, (object)new Single[] {1.1f,2.2f,3.3f,4.4f,5.5f}).Dump("����д5INT32");
		mc.ReadData<Single[]>("D", 100,5).Dump("���Ͷ�5INT16");
```
