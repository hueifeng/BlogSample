using System.Collections.Generic;

namespace LoadBalanceDemo
{
    public class ServerManager
    {
        //创建四台服务器,key=IP,Value=权重
        public static volatile Dictionary<string, int> ServerDictionary = new Dictionary<string, int>()
        {
            { "192.168.1.1",1},
            { "192.168.1.2",2},
            { "192.168.1.3",3},
            { "192.168.1.4",4}
        };
    } 
}
