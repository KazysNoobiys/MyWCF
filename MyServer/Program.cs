using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MyServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Server";
            ServiceHost host = new ServiceHost(typeof(MyWcfService.MyService));
            host.Open();
            Console.WriteLine("Сервер начал работу");
            Console.ReadKey();
            host.Close();
        }
    }
}
