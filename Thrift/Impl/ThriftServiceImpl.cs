using System;
using System.Collections.Generic;

namespace CSharpDemo.Thrift.Impl
{
    internal class ThriftServiceImpl : ThriftService.Iface
    {
        private Dictionary<string, string> testDic;

        public ThriftServiceImpl()
        {
            testDic = new Dictionary<string, string>();
        }

        public void Connect(Dictionary<string, string> machine)
        {
            testDic = machine;

        }

        public Dictionary<string, TestClass> GetMap()
        {
            var valuePairs = new Dictionary<string, TestClass>();

            if (testDic.Count > 0)
            {
                foreach (var n in testDic)
                {
                    valuePairs.Add(n.Key, new TestClass() { Name = n.Value });
                }
            }
            return valuePairs;
        }

        public Dictionary<string, int> GetValue()
        {
            var valuePairs = new Dictionary<string, int>();

            if (testDic.Count > 0)
            {
                foreach (var n in testDic)
                {
                    valuePairs.Add(n.Key, DateTime.Now.Second);
                }
            }
            return valuePairs;
        }
    }
}
