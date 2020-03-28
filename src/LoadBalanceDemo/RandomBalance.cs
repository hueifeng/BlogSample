using System;
using System.Collections;

namespace LoadBalanceDemo
{
    /// <summary>
    ///     随机类
    /// </summary>
    public  class RandomBalance
    {
        public static string GetServer()
        {
            var serverMap = ServerManager.ServerDictionary;
            ArrayList serverList = new ArrayList(serverMap);

            var random = new Random();
            //使用Random生成随机数,获取一个随机的服务器
            var server = serverList.GetRange(random.Next(serverList.Count),1);
            return server[0].ToString();
        }

    }
}
