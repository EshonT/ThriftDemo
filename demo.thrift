namespace java com.secdt.mro.vibration.thrift
namespace csharp CSharpDemo.Thrift

service ThriftService{
    void Connect(1: map<string,string> machine)
    map<string,i32> GetValue()
    map<string,TestClass> GetMap()
}

struct TestClass{
    1: string name,
    2: i32 input,
    3: i32 output,
    4: i32 reject,
}