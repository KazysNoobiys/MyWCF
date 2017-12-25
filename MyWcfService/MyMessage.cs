using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;


namespace MyWcfService
{   
    public class MyMessage
    {
        public int SIG { get; private set; }
        public ushort MID { get; private set; }
        public ushort RAL { get; private set; }        
        public ushort MCS { get; private set; }
        public Byte[] RAD { get; private set; }

        MyRecord myRecord;

        public MyMessage()
        {
            SIG = 0;
            MID = 0;
            RAL = 0;
            MCS = 0;
        }
        public MyMessage(string str)
        {
            SIG = 5523794;
            MID = (ushort)this.GetHashCode();            
            myRecord = new MyRecord(str);
            RAD = myRecord.MyRecordToBytes();
            RAL = (ushort)RAD.Length;            

            List<byte> combList = new List<byte>();
            combList.AddRange(BitConverter.GetBytes(SIG));
            combList.AddRange(BitConverter.GetBytes(MID));
            combList.AddRange(BitConverter.GetBytes(RAL));
            combList.AddRange(RAD);

            byte[] byteArr = combList.ToArray();
            MCS = crc16(byteArr, byteArr.Length);            
        }
        public byte[] MyMessageToBytes()
        {
            List<byte> combList = new List<byte>();
            combList.AddRange(BitConverter.GetBytes(SIG));
            combList.AddRange(BitConverter.GetBytes(MID));
            combList.AddRange(BitConverter.GetBytes(RAL));
            combList.AddRange(BitConverter.GetBytes(MCS));
            combList.AddRange(RAD);
            

            return combList.ToArray();

        }
        public static MyMessage BytesToMyMessage(byte[] bytes)
        {                       
            MyMessage myMessage = new MyMessage();           

            int offset = 0;            
            myMessage.SIG = BitConverter.ToInt32(bytes, 0);

            offset += Marshal.SizeOf(myMessage.SIG);            
            myMessage.MID = BitConverter.ToUInt16(bytes, offset);

            offset += Marshal.SizeOf(myMessage.MID);            
            myMessage.RAL = BitConverter.ToUInt16(bytes, offset);

            offset += Marshal.SizeOf(myMessage.RAL);
            myMessage.MCS = BitConverter.ToUInt16(bytes, offset);

            offset += Marshal.SizeOf(myMessage.MCS);
            myMessage.RAD = new byte[bytes.Length - offset];
            Array.Copy(bytes, offset, myMessage.RAD, 0, myMessage.RAD.Length);                       

            return myMessage;
        }



        public class MyRecord
        {
            public int UID { get; private set; }
            public uint TM { get; private set; }
            public ushort RL { get; private set; }
            public byte[] RD { get; private set; }

            public MyRecord()
            {
                UID = 0;
                TM = 0;
                RL = 0;
            }
            public MyRecord(string str)
            {
                RD = Encoding.Unicode.GetBytes(str);
                RL = (ushort)RD.Length;               
                var dateCreateMassage = DateTime.Now;
                TimeSpan ts = new DateTime(2010, 01, 01, 00, 00, 00) - dateCreateMassage;
                TM = (ushort)ts.Seconds;                
                UID = GetHashCode();                
            }
            public byte[] MyRecordToBytes()
            {                
                List<byte> combList = new List<byte>();
                combList.AddRange(BitConverter.GetBytes(UID));
                combList.AddRange(BitConverter.GetBytes(TM));
                combList.AddRange(BitConverter.GetBytes(RL));
                combList.AddRange(RD);                

                return combList.ToArray();
            }
            public static MyRecord BytesToMyRecord(byte[] bytes)
            {               
                MyRecord myRecord = new MyRecord();         
                
                int offset = 0;               
                myRecord.UID = BitConverter.ToInt32(bytes, offset);

                offset += Marshal.SizeOf(myRecord.UID);               
                myRecord.TM = BitConverter.ToUInt32(bytes, offset);

                offset += Marshal.SizeOf(myRecord.TM);                
                myRecord.RL = BitConverter.ToUInt16(bytes, offset);

                offset += Marshal.SizeOf(myRecord.RL);
                myRecord.RD = new byte[bytes.Length - offset];
                Array.Copy(bytes, offset, myRecord.RD, 0, myRecord.RD.Length);

                return myRecord;
            }

        }
        public static ushort crc16(byte[] buf, int len)
        {
            ushort crc = 0xFFFF;
            for (int pos = 0; pos < len; pos++)
            {
                crc ^= (ushort)buf[pos];
                for (int i = 8; i != 0; i--)
                {
                    if ((crc & 0x0001) != 0)
                    {
                        crc >>= 1;
                        crc ^= 0xA001;
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
            }
            return crc;
        }
    }
}
