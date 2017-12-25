using MyWcfService;
using System.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "CLIENT";
            
            MyServiceReference.MyServiceContractClient proxy = new MyServiceReference.MyServiceContractClient();
            string str = "Запрос";           
            Console.WriteLine("For client: " + str);

            byte[] bytes = proxy.Say(new MyMessage(str).MyMessageToBytes());
            MyMessage mm = MyMessage.BytesToMyMessage(bytes);
            Console.WriteLine("SIG = " + mm.SIG);
            Console.WriteLine("MID = " + mm.MID);
            Console.WriteLine("RAL = " + mm.RAL);
            Console.WriteLine("MCS = " + mm.MCS);
            //Console.WriteLine(new string('-',20));

            MyMessage.MyRecord mr = MyMessage.MyRecord.BytesToMyRecord(mm.RAD);
            Console.WriteLine("UID = " + mr.UID);
            Console.WriteLine("TM = " + mr.TM);
            Console.WriteLine("RL = " + mr.RL);
            Console.WriteLine("For server: " + Encoding.Unicode.GetString(mr.RD));
            Console.ReadKey();
        }
    }
}
