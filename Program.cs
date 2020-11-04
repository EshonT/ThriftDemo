using CSharpDemo.Thrift;
using CSharpDemo.Thrift.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Thrift;
using Thrift.Protocol;
using Thrift.Server;
using Thrift.Transport;

namespace CSharpDemo
{
    class Program
    {
        private static TServer server;
        static void Main(string[] args)
        {
            Console.WriteLine("启动服务");
            try
            {
                TServerSocket serverSocket = new TServerSocket(58622);
                TBinaryProtocol.Factory factory = new TBinaryProtocol.Factory();

                TProcessor processor = new ThriftService.Processor(new ThriftServiceImpl());
                server = new TSimpleServer(processor, serverSocket);
                server.Serve();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return;
            }
            Console.ReadKey();
        }
    }
}
