/**
 * Autogenerated by Thrift Compiler (0.12.0)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Thrift;
using Thrift.Collections;
using System.Runtime.Serialization;
using Thrift.Protocol;
using Thrift.Transport;

namespace CSharpDemo.Thrift
{
    public partial class ThriftService
    {
        public interface ISync
        {
            void Connect(Dictionary<string, string> machine);
            Dictionary<string, int> GetValue();
            Dictionary<string, TestClass> GetMap();
        }

        public interface Iface : ISync
        {
#if SILVERLIGHT
      IAsyncResult Begin_Connect(AsyncCallback callback, object state, Dictionary<string, string> machine);
      void End_Connect(IAsyncResult asyncResult);
#endif
#if SILVERLIGHT
      IAsyncResult Begin_GetValue(AsyncCallback callback, object state);
      Dictionary<string, int> End_GetValue(IAsyncResult asyncResult);
#endif
#if SILVERLIGHT
      IAsyncResult Begin_GetMap(AsyncCallback callback, object state);
      Dictionary<string, TestClass> End_GetMap(IAsyncResult asyncResult);
#endif
        }

        public class Client : IDisposable, Iface
        {
            public Client(TProtocol prot) : this(prot, prot)
            {
            }

            public Client(TProtocol iprot, TProtocol oprot)
            {
                iprot_ = iprot;
                oprot_ = oprot;
            }

            protected TProtocol iprot_;
            protected TProtocol oprot_;
            protected int seqid_;

            public TProtocol InputProtocol
            {
                get { return iprot_; }
            }
            public TProtocol OutputProtocol
            {
                get { return oprot_; }
            }


            #region " IDisposable Support "
            private bool _IsDisposed;

            // IDisposable
            public void Dispose()
            {
                Dispose(true);
            }


            protected virtual void Dispose(bool disposing)
            {
                if (!_IsDisposed)
                {
                    if (disposing)
                    {
                        if (iprot_ != null)
                        {
                            ((IDisposable)iprot_).Dispose();
                        }
                        if (oprot_ != null)
                        {
                            ((IDisposable)oprot_).Dispose();
                        }
                    }
                }
                _IsDisposed = true;
            }
            #endregion



#if SILVERLIGHT
      
      public IAsyncResult Begin_Connect(AsyncCallback callback, object state, Dictionary<string, string> machine)
      {
        return send_Connect(callback, state, machine);
      }

      public void End_Connect(IAsyncResult asyncResult)
      {
        oprot_.Transport.EndFlush(asyncResult);
        recv_Connect();
      }

#endif

            public void Connect(Dictionary<string, string> machine)
            {
#if SILVERLIGHT
        var asyncResult = Begin_Connect(null, null, machine);
        End_Connect(asyncResult);

#else
                send_Connect(machine);
                recv_Connect();

#endif
            }
#if SILVERLIGHT
      public IAsyncResult send_Connect(AsyncCallback callback, object state, Dictionary<string, string> machine)
      {
        oprot_.WriteMessageBegin(new TMessage("Connect", TMessageType.Call, seqid_));
        Connect_args args = new Connect_args();
        args.Machine = machine;
        args.Write(oprot_);
        oprot_.WriteMessageEnd();
        return oprot_.Transport.BeginFlush(callback, state);
      }

#else

            public void send_Connect(Dictionary<string, string> machine)
            {
                oprot_.WriteMessageBegin(new TMessage("Connect", TMessageType.Call, seqid_));
                Connect_args args = new Connect_args();
                args.Machine = machine;
                args.Write(oprot_);
                oprot_.WriteMessageEnd();
                oprot_.Transport.Flush();
            }
#endif

            public void recv_Connect()
            {
                TMessage msg = iprot_.ReadMessageBegin();
                if (msg.Type == TMessageType.Exception)
                {
                    TApplicationException x = TApplicationException.Read(iprot_);
                    iprot_.ReadMessageEnd();
                    throw x;
                }
                Connect_result result = new Connect_result();
                result.Read(iprot_);
                iprot_.ReadMessageEnd();
                return;
            }


#if SILVERLIGHT
      
      public IAsyncResult Begin_GetValue(AsyncCallback callback, object state)
      {
        return send_GetValue(callback, state);
      }

      public Dictionary<string, int> End_GetValue(IAsyncResult asyncResult)
      {
        oprot_.Transport.EndFlush(asyncResult);
        return recv_GetValue();
      }

#endif

            public Dictionary<string, int> GetValue()
            {
#if SILVERLIGHT
        var asyncResult = Begin_GetValue(null, null);
        return End_GetValue(asyncResult);

#else
                send_GetValue();
                return recv_GetValue();

#endif
            }
#if SILVERLIGHT
      public IAsyncResult send_GetValue(AsyncCallback callback, object state)
      {
        oprot_.WriteMessageBegin(new TMessage("GetValue", TMessageType.Call, seqid_));
        GetValue_args args = new GetValue_args();
        args.Write(oprot_);
        oprot_.WriteMessageEnd();
        return oprot_.Transport.BeginFlush(callback, state);
      }

#else

            public void send_GetValue()
            {
                oprot_.WriteMessageBegin(new TMessage("GetValue", TMessageType.Call, seqid_));
                GetValue_args args = new GetValue_args();
                args.Write(oprot_);
                oprot_.WriteMessageEnd();
                oprot_.Transport.Flush();
            }
#endif

            public Dictionary<string, int> recv_GetValue()
            {
                TMessage msg = iprot_.ReadMessageBegin();
                if (msg.Type == TMessageType.Exception)
                {
                    TApplicationException x = TApplicationException.Read(iprot_);
                    iprot_.ReadMessageEnd();
                    throw x;
                }
                GetValue_result result = new GetValue_result();
                result.Read(iprot_);
                iprot_.ReadMessageEnd();
                if (result.__isset.success)
                {
                    return result.Success;
                }
                throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "GetValue failed: unknown result");
            }


#if SILVERLIGHT
      
      public IAsyncResult Begin_GetMap(AsyncCallback callback, object state)
      {
        return send_GetMap(callback, state);
      }

      public Dictionary<string, TestClass> End_GetMap(IAsyncResult asyncResult)
      {
        oprot_.Transport.EndFlush(asyncResult);
        return recv_GetMap();
      }

#endif

            public Dictionary<string, TestClass> GetMap()
            {
#if SILVERLIGHT
        var asyncResult = Begin_GetMap(null, null);
        return End_GetMap(asyncResult);

#else
                send_GetMap();
                return recv_GetMap();

#endif
            }
#if SILVERLIGHT
      public IAsyncResult send_GetMap(AsyncCallback callback, object state)
      {
        oprot_.WriteMessageBegin(new TMessage("GetMap", TMessageType.Call, seqid_));
        GetMap_args args = new GetMap_args();
        args.Write(oprot_);
        oprot_.WriteMessageEnd();
        return oprot_.Transport.BeginFlush(callback, state);
      }

#else

            public void send_GetMap()
            {
                oprot_.WriteMessageBegin(new TMessage("GetMap", TMessageType.Call, seqid_));
                GetMap_args args = new GetMap_args();
                args.Write(oprot_);
                oprot_.WriteMessageEnd();
                oprot_.Transport.Flush();
            }
#endif

            public Dictionary<string, TestClass> recv_GetMap()
            {
                TMessage msg = iprot_.ReadMessageBegin();
                if (msg.Type == TMessageType.Exception)
                {
                    TApplicationException x = TApplicationException.Read(iprot_);
                    iprot_.ReadMessageEnd();
                    throw x;
                }
                GetMap_result result = new GetMap_result();
                result.Read(iprot_);
                iprot_.ReadMessageEnd();
                if (result.__isset.success)
                {
                    return result.Success;
                }
                throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "GetMap failed: unknown result");
            }

        }
        public class Processor : TProcessor
        {
            public Processor(ISync iface)
            {
                iface_ = iface;
                processMap_["Connect"] = Connect_Process;
                processMap_["GetValue"] = GetValue_Process;
                processMap_["GetMap"] = GetMap_Process;
            }

            protected delegate void ProcessFunction(int seqid, TProtocol iprot, TProtocol oprot);
            private ISync iface_;
            protected Dictionary<string, ProcessFunction> processMap_ = new Dictionary<string, ProcessFunction>();

            public bool Process(TProtocol iprot, TProtocol oprot)
            {
                try
                {
                    TMessage msg = iprot.ReadMessageBegin();
                    ProcessFunction fn;
                    processMap_.TryGetValue(msg.Name, out fn);
                    if (fn == null)
                    {
                        TProtocolUtil.Skip(iprot, TType.Struct);
                        iprot.ReadMessageEnd();
                        TApplicationException x = new TApplicationException(TApplicationException.ExceptionType.UnknownMethod, "Invalid method name: '" + msg.Name + "'");
                        oprot.WriteMessageBegin(new TMessage(msg.Name, TMessageType.Exception, msg.SeqID));
                        x.Write(oprot);
                        oprot.WriteMessageEnd();
                        oprot.Transport.Flush();
                        return true;
                    }
                    fn(msg.SeqID, iprot, oprot);
                }
                catch (IOException)
                {
                    return false;
                }
                return true;
            }

            public void Connect_Process(int seqid, TProtocol iprot, TProtocol oprot)
            {
                Connect_args args = new Connect_args();
                args.Read(iprot);
                iprot.ReadMessageEnd();
                Connect_result result = new Connect_result();
                try
                {
                    iface_.Connect(args.Machine);
                    oprot.WriteMessageBegin(new TMessage("Connect", TMessageType.Reply, seqid));
                    result.Write(oprot);
                }
                catch (TTransportException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("Error occurred in processor:");
                    Console.Error.WriteLine(ex.ToString());
                    TApplicationException x = new TApplicationException(TApplicationException.ExceptionType.InternalError, " Internal error.");
                    oprot.WriteMessageBegin(new TMessage("Connect", TMessageType.Exception, seqid));
                    x.Write(oprot);
                }
                oprot.WriteMessageEnd();
                oprot.Transport.Flush();
            }

            public void GetValue_Process(int seqid, TProtocol iprot, TProtocol oprot)
            {
                GetValue_args args = new GetValue_args();
                args.Read(iprot);
                iprot.ReadMessageEnd();
                GetValue_result result = new GetValue_result();
                try
                {
                    result.Success = iface_.GetValue();
                    oprot.WriteMessageBegin(new TMessage("GetValue", TMessageType.Reply, seqid));
                    result.Write(oprot);
                }
                catch (TTransportException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("Error occurred in processor:");
                    Console.Error.WriteLine(ex.ToString());
                    TApplicationException x = new TApplicationException(TApplicationException.ExceptionType.InternalError, " Internal error.");
                    oprot.WriteMessageBegin(new TMessage("GetValue", TMessageType.Exception, seqid));
                    x.Write(oprot);
                }
                oprot.WriteMessageEnd();
                oprot.Transport.Flush();
            }

            public void GetMap_Process(int seqid, TProtocol iprot, TProtocol oprot)
            {
                GetMap_args args = new GetMap_args();
                args.Read(iprot);
                iprot.ReadMessageEnd();
                GetMap_result result = new GetMap_result();
                try
                {
                    result.Success = iface_.GetMap();
                    oprot.WriteMessageBegin(new TMessage("GetMap", TMessageType.Reply, seqid));
                    result.Write(oprot);
                }
                catch (TTransportException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("Error occurred in processor:");
                    Console.Error.WriteLine(ex.ToString());
                    TApplicationException x = new TApplicationException(TApplicationException.ExceptionType.InternalError, " Internal error.");
                    oprot.WriteMessageBegin(new TMessage("GetMap", TMessageType.Exception, seqid));
                    x.Write(oprot);
                }
                oprot.WriteMessageEnd();
                oprot.Transport.Flush();
            }

        }


#if !SILVERLIGHT
        [Serializable]
#endif
        public partial class Connect_args : TBase
        {
            private Dictionary<string, string> _machine;

            public Dictionary<string, string> Machine
            {
                get
                {
                    return _machine;
                }
                set
                {
                    __isset.machine = true;
                    this._machine = value;
                }
            }


            public Isset __isset;
#if !SILVERLIGHT
            [Serializable]
#endif
            public struct Isset
            {
                public bool machine;
            }

            public Connect_args()
            {
            }

            public void Read(TProtocol iprot)
            {
                iprot.IncrementRecursionDepth();
                try
                {
                    TField field;
                    iprot.ReadStructBegin();
                    while (true)
                    {
                        field = iprot.ReadFieldBegin();
                        if (field.Type == TType.Stop)
                        {
                            break;
                        }
                        switch (field.ID)
                        {
                            case 1:
                                if (field.Type == TType.Map)
                                {
                                    {
                                        Machine = new Dictionary<string, string>();
                                        TMap _map0 = iprot.ReadMapBegin();
                                        for (int _i1 = 0; _i1 < _map0.Count; ++_i1)
                                        {
                                            string _key2;
                                            string _val3;
                                            _key2 = iprot.ReadString();
                                            _val3 = iprot.ReadString();
                                            Machine[_key2] = _val3;
                                        }
                                        iprot.ReadMapEnd();
                                    }
                                }
                                else
                                {
                                    TProtocolUtil.Skip(iprot, field.Type);
                                }
                                break;
                            default:
                                TProtocolUtil.Skip(iprot, field.Type);
                                break;
                        }
                        iprot.ReadFieldEnd();
                    }
                    iprot.ReadStructEnd();
                }
                finally
                {
                    iprot.DecrementRecursionDepth();
                }
            }

            public void Write(TProtocol oprot)
            {
                oprot.IncrementRecursionDepth();
                try
                {
                    TStruct struc = new TStruct("Connect_args");
                    oprot.WriteStructBegin(struc);
                    TField field = new TField();
                    if (Machine != null && __isset.machine)
                    {
                        field.Name = "machine";
                        field.Type = TType.Map;
                        field.ID = 1;
                        oprot.WriteFieldBegin(field);
                        {
                            oprot.WriteMapBegin(new TMap(TType.String, TType.String, Machine.Count));
                            foreach (string _iter4 in Machine.Keys)
                            {
                                oprot.WriteString(_iter4);
                                oprot.WriteString(Machine[_iter4]);
                            }
                            oprot.WriteMapEnd();
                        }
                        oprot.WriteFieldEnd();
                    }
                    oprot.WriteFieldStop();
                    oprot.WriteStructEnd();
                }
                finally
                {
                    oprot.DecrementRecursionDepth();
                }
            }

            public override string ToString()
            {
                StringBuilder __sb = new StringBuilder("Connect_args(");
                bool __first = true;
                if (Machine != null && __isset.machine)
                {
                    if (!__first) { __sb.Append(", "); }
                    __first = false;
                    __sb.Append("Machine: ");
                    __sb.Append(Machine);
                }
                __sb.Append(")");
                return __sb.ToString();
            }

        }


#if !SILVERLIGHT
        [Serializable]
#endif
        public partial class Connect_result : TBase
        {

            public Connect_result()
            {
            }

            public void Read(TProtocol iprot)
            {
                iprot.IncrementRecursionDepth();
                try
                {
                    TField field;
                    iprot.ReadStructBegin();
                    while (true)
                    {
                        field = iprot.ReadFieldBegin();
                        if (field.Type == TType.Stop)
                        {
                            break;
                        }
                        switch (field.ID)
                        {
                            default:
                                TProtocolUtil.Skip(iprot, field.Type);
                                break;
                        }
                        iprot.ReadFieldEnd();
                    }
                    iprot.ReadStructEnd();
                }
                finally
                {
                    iprot.DecrementRecursionDepth();
                }
            }

            public void Write(TProtocol oprot)
            {
                oprot.IncrementRecursionDepth();
                try
                {
                    TStruct struc = new TStruct("Connect_result");
                    oprot.WriteStructBegin(struc);

                    oprot.WriteFieldStop();
                    oprot.WriteStructEnd();
                }
                finally
                {
                    oprot.DecrementRecursionDepth();
                }
            }

            public override string ToString()
            {
                StringBuilder __sb = new StringBuilder("Connect_result(");
                __sb.Append(")");
                return __sb.ToString();
            }

        }


#if !SILVERLIGHT
        [Serializable]
#endif
        public partial class GetValue_args : TBase
        {

            public GetValue_args()
            {
            }

            public void Read(TProtocol iprot)
            {
                iprot.IncrementRecursionDepth();
                try
                {
                    TField field;
                    iprot.ReadStructBegin();
                    while (true)
                    {
                        field = iprot.ReadFieldBegin();
                        if (field.Type == TType.Stop)
                        {
                            break;
                        }
                        switch (field.ID)
                        {
                            default:
                                TProtocolUtil.Skip(iprot, field.Type);
                                break;
                        }
                        iprot.ReadFieldEnd();
                    }
                    iprot.ReadStructEnd();
                }
                finally
                {
                    iprot.DecrementRecursionDepth();
                }
            }

            public void Write(TProtocol oprot)
            {
                oprot.IncrementRecursionDepth();
                try
                {
                    TStruct struc = new TStruct("GetValue_args");
                    oprot.WriteStructBegin(struc);
                    oprot.WriteFieldStop();
                    oprot.WriteStructEnd();
                }
                finally
                {
                    oprot.DecrementRecursionDepth();
                }
            }

            public override string ToString()
            {
                StringBuilder __sb = new StringBuilder("GetValue_args(");
                __sb.Append(")");
                return __sb.ToString();
            }

        }


#if !SILVERLIGHT
        [Serializable]
#endif
        public partial class GetValue_result : TBase
        {
            private Dictionary<string, int> _success;

            public Dictionary<string, int> Success
            {
                get
                {
                    return _success;
                }
                set
                {
                    __isset.success = true;
                    this._success = value;
                }
            }


            public Isset __isset;
#if !SILVERLIGHT
            [Serializable]
#endif
            public struct Isset
            {
                public bool success;
            }

            public GetValue_result()
            {
            }

            public void Read(TProtocol iprot)
            {
                iprot.IncrementRecursionDepth();
                try
                {
                    TField field;
                    iprot.ReadStructBegin();
                    while (true)
                    {
                        field = iprot.ReadFieldBegin();
                        if (field.Type == TType.Stop)
                        {
                            break;
                        }
                        switch (field.ID)
                        {
                            case 0:
                                if (field.Type == TType.Map)
                                {
                                    {
                                        Success = new Dictionary<string, int>();
                                        TMap _map5 = iprot.ReadMapBegin();
                                        for (int _i6 = 0; _i6 < _map5.Count; ++_i6)
                                        {
                                            string _key7;
                                            int _val8;
                                            _key7 = iprot.ReadString();
                                            _val8 = iprot.ReadI32();
                                            Success[_key7] = _val8;
                                        }
                                        iprot.ReadMapEnd();
                                    }
                                }
                                else
                                {
                                    TProtocolUtil.Skip(iprot, field.Type);
                                }
                                break;
                            default:
                                TProtocolUtil.Skip(iprot, field.Type);
                                break;
                        }
                        iprot.ReadFieldEnd();
                    }
                    iprot.ReadStructEnd();
                }
                finally
                {
                    iprot.DecrementRecursionDepth();
                }
            }

            public void Write(TProtocol oprot)
            {
                oprot.IncrementRecursionDepth();
                try
                {
                    TStruct struc = new TStruct("GetValue_result");
                    oprot.WriteStructBegin(struc);
                    TField field = new TField();

                    if (this.__isset.success)
                    {
                        if (Success != null)
                        {
                            field.Name = "Success";
                            field.Type = TType.Map;
                            field.ID = 0;
                            oprot.WriteFieldBegin(field);
                            {
                                oprot.WriteMapBegin(new TMap(TType.String, TType.I32, Success.Count));
                                foreach (string _iter9 in Success.Keys)
                                {
                                    oprot.WriteString(_iter9);
                                    oprot.WriteI32(Success[_iter9]);
                                }
                                oprot.WriteMapEnd();
                            }
                            oprot.WriteFieldEnd();
                        }
                    }
                    oprot.WriteFieldStop();
                    oprot.WriteStructEnd();
                }
                finally
                {
                    oprot.DecrementRecursionDepth();
                }
            }

            public override string ToString()
            {
                StringBuilder __sb = new StringBuilder("GetValue_result(");
                bool __first = true;
                if (Success != null && __isset.success)
                {
                    if (!__first) { __sb.Append(", "); }
                    __first = false;
                    __sb.Append("Success: ");
                    __sb.Append(Success);
                }
                __sb.Append(")");
                return __sb.ToString();
            }

        }


#if !SILVERLIGHT
        [Serializable]
#endif
        public partial class GetMap_args : TBase
        {

            public GetMap_args()
            {
            }

            public void Read(TProtocol iprot)
            {
                iprot.IncrementRecursionDepth();
                try
                {
                    TField field;
                    iprot.ReadStructBegin();
                    while (true)
                    {
                        field = iprot.ReadFieldBegin();
                        if (field.Type == TType.Stop)
                        {
                            break;
                        }
                        switch (field.ID)
                        {
                            default:
                                TProtocolUtil.Skip(iprot, field.Type);
                                break;
                        }
                        iprot.ReadFieldEnd();
                    }
                    iprot.ReadStructEnd();
                }
                finally
                {
                    iprot.DecrementRecursionDepth();
                }
            }

            public void Write(TProtocol oprot)
            {
                oprot.IncrementRecursionDepth();
                try
                {
                    TStruct struc = new TStruct("GetMap_args");
                    oprot.WriteStructBegin(struc);
                    oprot.WriteFieldStop();
                    oprot.WriteStructEnd();
                }
                finally
                {
                    oprot.DecrementRecursionDepth();
                }
            }

            public override string ToString()
            {
                StringBuilder __sb = new StringBuilder("GetMap_args(");
                __sb.Append(")");
                return __sb.ToString();
            }

        }


#if !SILVERLIGHT
        [Serializable]
#endif
        public partial class GetMap_result : TBase
        {
            private Dictionary<string, TestClass> _success;

            public Dictionary<string, TestClass> Success
            {
                get
                {
                    return _success;
                }
                set
                {
                    __isset.success = true;
                    this._success = value;
                }
            }


            public Isset __isset;
#if !SILVERLIGHT
            [Serializable]
#endif
            public struct Isset
            {
                public bool success;
            }

            public GetMap_result()
            {
            }

            public void Read(TProtocol iprot)
            {
                iprot.IncrementRecursionDepth();
                try
                {
                    TField field;
                    iprot.ReadStructBegin();
                    while (true)
                    {
                        field = iprot.ReadFieldBegin();
                        if (field.Type == TType.Stop)
                        {
                            break;
                        }
                        switch (field.ID)
                        {
                            case 0:
                                if (field.Type == TType.Map)
                                {
                                    {
                                        Success = new Dictionary<string, TestClass>();
                                        TMap _map10 = iprot.ReadMapBegin();
                                        for (int _i11 = 0; _i11 < _map10.Count; ++_i11)
                                        {
                                            string _key12;
                                            TestClass _val13;
                                            _key12 = iprot.ReadString();
                                            _val13 = new TestClass();
                                            _val13.Read(iprot);
                                            Success[_key12] = _val13;
                                        }
                                        iprot.ReadMapEnd();
                                    }
                                }
                                else
                                {
                                    TProtocolUtil.Skip(iprot, field.Type);
                                }
                                break;
                            default:
                                TProtocolUtil.Skip(iprot, field.Type);
                                break;
                        }
                        iprot.ReadFieldEnd();
                    }
                    iprot.ReadStructEnd();
                }
                finally
                {
                    iprot.DecrementRecursionDepth();
                }
            }

            public void Write(TProtocol oprot)
            {
                oprot.IncrementRecursionDepth();
                try
                {
                    TStruct struc = new TStruct("GetMap_result");
                    oprot.WriteStructBegin(struc);
                    TField field = new TField();

                    if (this.__isset.success)
                    {
                        if (Success != null)
                        {
                            field.Name = "Success";
                            field.Type = TType.Map;
                            field.ID = 0;
                            oprot.WriteFieldBegin(field);
                            {
                                oprot.WriteMapBegin(new TMap(TType.String, TType.Struct, Success.Count));
                                foreach (string _iter14 in Success.Keys)
                                {
                                    oprot.WriteString(_iter14);
                                    Success[_iter14].Write(oprot);
                                }
                                oprot.WriteMapEnd();
                            }
                            oprot.WriteFieldEnd();
                        }
                    }
                    oprot.WriteFieldStop();
                    oprot.WriteStructEnd();
                }
                finally
                {
                    oprot.DecrementRecursionDepth();
                }
            }

            public override string ToString()
            {
                StringBuilder __sb = new StringBuilder("GetMap_result(");
                bool __first = true;
                if (Success != null && __isset.success)
                {
                    if (!__first) { __sb.Append(", "); }
                    __first = false;
                    __sb.Append("Success: ");
                    __sb.Append(Success);
                }
                __sb.Append(")");
                return __sb.ToString();
            }

        }

    }
}
