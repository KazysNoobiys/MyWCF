using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MyWcfService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде и файле конфигурации.
    public class MyService : IMyServiceContract
    {
        public byte[] Say(byte[] bytes)
        {
            MyMessage myMassage = MyMessage.BytesToMyMessage(bytes);
            MyMessage.MyRecord myRec = MyMessage.MyRecord.BytesToMyRecord(myMassage.RAD);
            string str = Encoding.Unicode.GetString(myRec.RD);
            string str1 = str + ":Ответ";
            MyMessage massage = new MyMessage(str1);
            return massage.MyMessageToBytes();
        }
    }
}
