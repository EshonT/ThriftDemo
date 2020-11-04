using System.Collections.Generic;

namespace CSharpDemo.Thrift.Impl
{
    internal class ThriftServiceImpl : ThriftService.Iface
    {
        public void Connect(Dictionary<string, string> machine)
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<string, TestClass> GetMap()
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<string, int> GetValue()
        {
            throw new System.NotImplementedException();
        }
    }
}
