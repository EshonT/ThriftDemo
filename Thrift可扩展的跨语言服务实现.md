

# Thrift: 可扩展的跨语言服务实现-- [阅读笔记](http://thrift.apache.org/static/files/thrift-20070401.pdf)

[TOC]



Thrift是Facebook开发出的一个软件库和一组代码生成工具，以加快高效率、可扩展的后端服务的开发与实现的速度。它通过对各语言最常用的部分加以抽象，把它们放进一个通用库里，再用各个语言实现，来实现跨编程语言的高效而可靠的通信。亦即，Thrift允许开发者在一个单独的语言无关的文件里，定义数据类型和服务接口，然后生成用来构建RPC客户和服务器所需的全部代码。

# 1 Introduction（简介）

Thrift：

- 在多种编程语言之间建立一个透明的、高效的桥梁；

- 一个在众多编程语言之中实现的语言中立的软件栈，以及相关的代码生成引擎，该引擎将一种简单的接口和数据定义语言，转换为客户及服务器远程过程调用库；

- 设计为对开发者尽可能简单，对一个复杂的服务，在单个短文件里即可定义全部必需的数据结构和接口。

  *网络环境下，跨语言交互的一些关键组件*：

  1. **类型**（Types）——需要一种通用的类型系统。类型的转换上，编程者不需编写任何应用层以下的代码。
  2. **传输**（Transport）——各个语言必须有一种双向传输原始数据的通用接口。一个给定的传输是如何实现的，应该与服务开发者无关。
  3. **协议**（Protocol）——数据类型必须有某种方法，来使用传输层对它们自身编码和解码。同样地，应用开发者不需要关心该层。重要的是，数据能够以一种一致的、确定的方式被读写。
  4. **版本化**（Versioning）——健壮的服务相关的数据类型必须提供一种自身版本化的机制。具体来说，它应当能在一个对象中添加或移除域，或改变一个函数的参数列表，而不干扰服务。
  5. **处理器**（Processors）——最后，我们生成能够处理数据流以实现远程过程调用的代码。

# 2 Types（类型）

​	Thrift类型系统的目标是使编程者能使用完全在Thrift中定义的类型，而不论他们使用的是哪种编程语言。

​	Thrift类型系统没有引入任何特殊的动态类型或包装器对象，也不要求开发者编写任何对象序列化或传输的代码。

​	Thrift IDL文件在逻辑上，是开发者对他们的数据结构进行注解的一种方法，该方法告诉代码生成器怎样在语言之间安全传输对象，所需的额外信息量最小。

## 2.1 Base Types（基本类型）

所有编程语言中都可用的关键类型。

|     bool      |    byte    |      i16       |      i32       |      i64       |   double   |             string             |
| :-----------: | :--------: | :------------: | :------------: | :------------: | :--------: | :----------------------------: |
| 布尔值 真或假 | 有符号字节 | 16位有符号整数 | 32位有符号整数 | 64位有符号整数 | 64位浮点数 | 与编码无关的文本或二进制字符串 |


许多语言中都没有无符号整数类型，且无法防止某些语言（如Python）的开发者把一个负值赋给一个整型变量，这会导致程序无法预料的行为。

从设计角度讲，无符号整型鲜少用于数学目的，实际中更长用作关键词或标识符。这种情况下，符号是无关紧要的，可用有符号整型代替。

## 2.2 Structs（结构体）

Thrift结构体定义了一个用在多种语言之间的通用对象。定义一个Thrift结构体的基本语法与C结构体定义非常相似。域可由一个整型域标识符（在该结构体的作用域内是唯一的），以及可选的默认值来标注。

```thrift
struct Example {
	1:i32 number=10,
	2:i64 bigNumber,
	3:double decimals,
	4:string name="thrifty"
}
```

## 2.3 Containers（容器）

Thrift的容器是强类型容器，映射为通用编程语言中最常使用的容器。使用C++模板类来标注。

有三种可用类型：

- **list<type>**  元素的有序列表。直接翻译为STL vector，Java ArrayList，或脚本语言中的native array。可包含重复元素。
	
- **set<type>** 不重复元素的无序集合。翻译为STL set，Java HashSet，Python中的set，或PHP/Ruby中的native dictionary。
	
- **Map<type1,type2>** 严格唯一的键（keys）到值（values）的映射。翻译为STL map，Java HashMap，PHP associative array，或Python/Ruby dictionary。


在目标语言中，定义将产生有read和write两种方法的类型，使用Thrift TProtocol对象对对象进行序列化和传输。

## 2.4 Exceptions（异常）

​	异常在语法和功能上都与结构体相同，唯一的区别是它们使用exception关键词，而非struct关键词进行声明。生成的对象继承自各目标编程语言中适当的异常基类，以便与任何给定语言中的本地异常处理无缝地整合。

## 2.5 Services（服务）

​	使用Thrift类型定义服务。对一个服务的定义在语法上等同于在面向对象编程中定义一个接口（或一个纯虚抽象类）。Thrift编译器生成实现该接口的客户与服务器存根。服务的定义如下：

```thrift
service <name> {
	<returntype> <name>(<arguments>)
		[throws (<exceptions>)]
	...
}
```

*一个例子：*

```thrift
service StringCache {
	void set(1:i32 key, 2:string value),
	string get(1:i32 key) throws (1:KeyNotFound knf),
	void delete(1:i32 key)
}
```

*注意：*

​	除其他所有定义的Thrift类型外，**void**也是一个有效的函数返回类型。**Void**函数可添加一个**async**修饰符，产生的代码不等待服务器的应答。一个纯void函数会向客户端返回一个应答，保证服务器一侧操作已完成。应用开发者应**小心**，仅当方法调用失败是可以接受的，或传输层已知可靠的情况下，才使用async优化。

# 3 Transport（传输）

生成的代码使用传输层来促进数据传递。

## 3.1 Interface（接口）

Thrift实现中，一个关键的设计选择就是将传输层从代码生成层解耦。从根本上，生成的Thrift代码只需要知道如何读和写数据。数据的源和目的地无关紧要，可以使一个socket，一段共享内存，或本地磁盘上的一个文件。TTransport（Thrift transport）接口支持以下方法：

- **open**   Opens the transport
- **close**  Closes the transport
- **isOpen** Indicates whether the transport is open
- **read**   Reads from the transport
- **write**  Writes to the transport
- **flush**  Forces any pending writes

除以上的TTransport接口外，还有一个TServerTransport接口，用来接受或创建原始传输对象。它的接口如下：

- **open**  Opens the transport
- **listen**  Begins listening for connections
- **accept** Returns a new client transport
- **close**  Closes the transport

## 3.2 Implementation（实现）

### 3.2.1 TSocket

​	TSocket类在所有目标语言中实现，提供了一个到TCP/IP流套接字（stream socket）的通用、简单的接口。

### 3.2.2 TFileTransport

​	TFileTransport是一个磁盘上的文件到一个数据流的抽象，可用来将一组到来的Thrift请求写到磁盘上的一个文件中。

### 3.2.3 Utilities

​	传输接口支持使用通用OOP技术的扩展，例如组合。简单的utilities包括TBufferedTransport，TFramedTransport和TMemoryBuffer。

# 4 Protocol（协议）

​	Thrift中第二个主要的抽象，是将数据结构与传输显示分离。传输数据时，Thrift强制一个特定的消息结构，但所使用的协议编码对它来说是不可知的。换句话说，只要数据支持一组固定的操作，使它能够被生成的代码确定地读写，那么无论数据编码使用的是XML，ASCII或者二进制格式，都无关紧要。

## 4.1 Interface（接口）

​	Thrift协议接口非常直接，它根本上支持两件事：

​		1)  双向有序的消息传递；
​		2)  基本类型、容器及结构体的编码。
​    		*writeMessageBegin(name, type, seq)*
​    		*writeMessageEnd()*
​    		*writeStructBegin(name)*
​    		*writeStructEnd()*
​    		*writeFieldBegin(name, type, id)*
​    		*writeFieldEnd()*
​    		*writeFieldStop()*
​    		*writeMapBegin(ktype, vtype, size)*
​    		*writeMapEnd()*
​    		*writeListBegin(etype, size)*
​    		*writeListEnd()*
​    		*writeSetBegin(etype, size)*
​    		*writeSetEnd()*
​    		*writeBool(bool)*
​    		*writeByte(byte)*
​    		*writeI16(i16)*
​    		*writeI32(i32)*
​    		*writeI64(i64)*
​    		*writeDouble(double)*
​    		*writeString(string)*
​    		*name, type, seq = readMessageBegin()*
​    		*readMessageEnd()*
​    		*name = readStructBegin()*
​    		*readStructEnd()*
​    		*name, type, id = readFieldBegin()*
​    		*readFieldEnd()*
​    		*k, v, size = readMapBegin()*
​    		*readMapEnd()*
​    		*etype, size = readListBegin()*
​    		*readListEnd()*
​    		*etype, size = readSetBegin()*
​    		*readSetEnd()*
​    		*bool = readBool()*
​    		*byte = readByte()*
​    		*i16 = readI16()*
​    		*i32 = readI32()*
​    		*i64 = readI64()*
​    		*double = readDouble()*
​    		*string = readString()*


​	注意到每个write函数有且仅有一个相应的read方法。*WriteFieldStop()*异常是一个特殊的方法，标志一个结构的结束。读一个结构的过程是*readFieldBegin()*直到遇到stop域，然后*readStructEnd()*。生成的代码依靠这个调用顺序，来确保一个协议编码器所写的每一件事，都可被一个相应的协议解码器读取。

​	这组功能在设计上更加注重**健壮性**，而非必要性。例如，*writeStructEnd()*不是严格必需的，因为一个结构体的结束可用stop域表示。

## 4.2 Structure（结构）

​	Thrift结构支持编码进一个流协议里，编码前无需将一个结构成帧或计算全部数据的长度。类似地，结构体也并不事先编码它们的数据长度。作为代替，它们被编码为一序列的域，每个域有一个类型说明符和一个唯一的域标识符。一个特殊的STOP类型的域标志着结构体的结束。因为所有基本类型可被确定地读取，所有结构体（甚至是包含其他结构的结构体）也能被确定地读取。Thrift协议是自定界的，没有任何成帧，且不论编码格式。

​	在没必要流化或成帧有优势的场合，使用TFramedTransport抽象可以非常简单地将成帧的功能添加进传输层。

## 4.3 Implementation（实现）

​	Facebook已经实现并部署了一个节省空间的二进制协议，该协议为大多数后端服务所使用。从本质上讲，它将所有数据按一种扁平的（flat）二进制格式来写。整数类型将转换为网络字节顺序(字节序)，字符串以其字节长度为前缀，并且所有消息和字段标头都使用原始整数序列化结构编写。使用生成的代码时字段的字符串名称被省略，字段标识符就足够了。

​	为了简化代码，我们决定不进行某些极端的存储优化（即将小整数打包为ASCII或使用7位连续格式）。 如果遇到性能要求严格的用例，可以轻松进行更改。

# 5 Versioning（版本化）

​	Thrift面对版本化和数据定义的改变是健壮的。将阶段性的改变推出到已部署的服务中的能力至关重要。系统必须能够支持从日志文件中读取旧数据，以及过时的客户（服务器）向新的服务器（客户）发送的请求。

## 5.1 Field Identifiers（域标识符）

​	Thrift的版本化通过域标识符实现。Thrift中，一个结构体的每一个成员的域头都用一个唯一的域标识符编码。域标识符和类型说明符结合起来，唯一地标志该域。Thrift定义语言支持域标识符的自动分配，但最好始终显式地指定域标识符。标识符的指定如下所示：

```thrift
struct Example {
	1:i32 number=10,
	2:i64 bigNumber,
	3:double decimals,
	4:string name="thrifty"
}
```

​	为避免手动和自动分配的标识符之间的冲突，省略了标识符的域所赋的标识符从-1开始递减，本语言对正的标识符仅支持手动赋值。

​	函数参数列表里能够、并且应当指定域标识符。事实上，参数列表不仅在后端表现为结构，实际上在编译器前端也表现为与结构体同样的代码。这允许我们对方法参数进行版本安全的修改。

```thrift
service StringCache {
	void set(1:i32 key, 2:string value),
	string get(1:i32 key) throws (1:KeyNotFound knf),
	void delete(1:i32 key)
}
```

​	可认为结构体是一个字典，标识符是关键字，而值是强类型的有名字的域。

域标识符在内部使用**i16**的Thrift类型。然而要注意，TProtocol抽象能以任何格式编码标识符。

## 5.2 Isset

​	如果遇到了一个预料之外的域，它可被安全地忽视并丢弃。当一个预期的域找不到时，必须有某些方法告诉开发者该域不在。这是通过定义的对象内部的一个isset结构实现的。（Isset功能在PHP里默认为null，Python里为None，Ruby里为nil）。

​	各个Thrift结构内部的isset对象为各个域包含一个布尔值，表示该域在结构中是否存在。接收一个结构时，应当在直接对其进行操作之前，先检查一个域是否已设置（being set）。

```c++
class Example {
	public:
		Example() :
			number(10),
			bigNumber(0),
			decimals(0),
			name("thrifty") {}
		int32_t number;
		int64_t bigNumber;
		double decimals;
		std::string name;
		
		struct __isset {
			__isset() :
				number(false),
				bigNumber(false),
				decimals(false),
				name(false) {}
			bool number;
			bool bigNumber;
			bool decimals;
			bool name;
		} __isset;
	...
}
```

## 5.3 Case Analysis（案例分析）

有四种可能发生版本不匹配的情况：

1. *新加的域，旧客户端，新服务器。*这种情况下，旧客户端不发送新的域，新服务器认出该域未设置，并对过时的请求执行默认行为。
2. *移除的域，旧客户端，新服务器。*这种情况下，旧客户端发送已被移除的域，而新服务器简单地无视它。
3. *新加的域，新客户端，旧服务器。*新客户端发送一个旧服务器不识别的域。旧服务器简单地无视该域，像平时一样进行处理。
4. *移除的域，新客户端，旧服务器。*这是最危险的情况，因为旧服务器不太可能对丢失的域执行适当的默认行为。这种情形下，建议在新客户端之前，先推出新服务器。

## 5.4 Protocol/Transport Versioning（协议/传输版本化）

TProtocol抽象同样给与协议的实现以自由，让协议的实现能以任何它们认为合适的方式，对它们自身进行版本化。在协议层，完全由实现者来决定如何处理版本化。关键点是协议编码的改变，要与接口定义版本的变化安全地隔离开来。

# 6 RPC Implementation（远程过程调用实现）

## 6.1 TProcessor

Thrift设计中，最后一个核心的接口是TProcessor。该接口如下所示：

```thrift
interface TProcessor {
	bool process(TProtocol in, TProtocol out) throws TException
}
```

这里的关键设计理念是：我们构建的复杂系统，能够从根本上分解为对输入输出进行操作的代理或服务。大多数情况下，实际上只有一个输入和一个输出需要处理。

## 6.2 Generated Code（生成的代码）

定义了一个服务时，我们生成一个TProcessor实例，使用一些helpers，该实例有能力处理对那个服务的RPC请求。用伪C++来说明，基本结构如下所示：

```
Service.thrift
	=> Service.cpp
		interface ServiceIf
		class ServiceClient : virtual ServiceIf
			TProtocol in
			TProtocol out
		class ServiceProcessor : TProcessor
			ServiceIf handler

ServiceHandler.cpp
	class ServiceHandler : virtual ServiceIf
	
TServer.cpp
	TServer(TProcessor processor,
			TServerTransport transport,
			TTransportFactory tfactory,
			TProtocolFactory pfactory)
	serve()
```

​	我们从Thrift定义文件，生成虚服务接口。生成了一个实现该接口的客户类，该类使用两个TProtocol实例来执行I/O操作。生成的processor实现了TProcessor接口。通过调用process()，并将应用开发者实现的服务接口的一个实例作为参数，生成的代码具有处理RPC调用的所有逻辑。

​	使用者在一个分离的、非生成的源码中，提供应用接口的实现。

## 6.3 TServer

最后，Thrift核心库提供一个TServer抽象。TServer对象通常如下工作。

- 使用TServerTransport获得一个TTransport
- 使用TTransportFactory，可选地将原始传输转换为一个适合的应用传输（典型的是使用TBufferedTransportFactory）
- 使用TProtocolFactory，为TTransport创建一个输入和输出
- 调用TProcessor对象的process()方法

恰当地分离各个层次，这样服务器代码无需了解任何正在使用的传输、编码或者应用。服务器在连接处理、线程等方面封装逻辑，而processor处理RPC。唯一由应用开发者编写的代码存在于Thrift定义文件和接口实现里。

Facebook已部署了多种TServer实现，包括单线程的TSimpleServer，每个连接一个线程的TThreadedServer，以及线程池的TThreadPoolServer。

TProcessor接口在设计上具有非常高的普遍性。不要求一个TServer使用一个生成的TProcessor对象。应用开发者可以很容易地编写在TProtocol对象上操作的任何类型的服务器（例如，一个服务器可以简单地将一个特定的对象类型流化，而没有任何实际的RPC方法调用）。

# 7 Implementation Details（实现的细节）

## 7.1 Target Languages（目标语言）

​	Thrift当前支持五种目标语言：C++，Java，Python，Ruby和PHP。在Facebook，用C++部署的服务器占主导地位。用PHP实现的Thrift服务也已被嵌入Apache web服务器，从后端透明地接入到许多使用THttpClient实现TTransport接口的前端结构。

​	固然，公共web服务与Thrift的核心用例和设计无关，但一旦有这个需求，Thrift促进了快速迭代，让我们能够将整个基于XML的web服务，快速迁移到一个更高性能的系统上。

## 7.2 Generated Structs（生成的结构体）

​	我们作了一个合理的决定，让我们生成的结构体尽可能透明。所有域都是公共可达的；没有set()和get()方法。类似地，isset对象的使用并不是强制的。我们没有包括任何FieldNotSetException结构。开发者可使用这些域来编写更加健壮的代码，但即使完全忽略isset结构，系统对开发者也是健壮的，并将在所有情况下提供适当的默认行为。

​	这一决定的动机是减轻应用开发难度的渴望，让开发者们使用在各语言中最熟悉的结构，生成可以工作的代码，而非学习他们所选择的语言的一个丰富的新库。

​	生成的对象的read()和write()方法是公共的，这样对象可在RPC客户机和服务器的上下文之外使用。Thrift是一个有用的工具，它单纯是为生成可轻易跨语言序列化的对象。

## 7.3 RPC Method Identification（RPC方法识别）

​	RPC的方法调用通过以一个字符串的形式发送方法名来实现。越长的方法名要求越多的带宽。

​	方法调用时，为避免过多不必要的字符串比较，我们生成从字符串到函数指针的映射，这样在通常的情况下，借由一个固定时间的哈希查找，调用能够被有效地完成。

因为Java没有函数指针，process函数都是实现一个通用接口的私有成员类。

```java
private class ping implements ProcessFunction {
	public void process(int seqid,
						TProtocol iprot,
						TProtocol oprot)
		throws TException
	{ ...}
}

HashMap<String,ProcessFunction> processMap_ =
	new HashMap<String,ProcessFunction>();
```

C++中，我们使用成员函数指针。

```c++
std::map<std::string,
		void (ExampleServiceProcessor::*)(int32_t,
		facebook::thrift::protocol::TProtocol*,
		facebook::thrift::protocol::TProtocol*)>
	processMap_;
```

使用这些技术，我们最小化了字符串处理的开销，通过检查已知的字符串方法名，我们能够容易地进行调试。

## 7.4 Servers and Multithreading（服务器和多线程）

​	为处理来自多个客户机的同时的请求，Thrift服务要求基本的多线程。对Thrift服务器逻辑的Python和Java实现来说，随语言发布的标准线程库提供了足够的支持。对C++实现来说，不存在标准的多线程运行时库。具体说来，不存在健壮的、轻量的和可移植的线程管理器及定时器类。为此，Thrift实现了自己的库，如下所述。

## 7.5 Thread Primitives（线程原语）

名字空间facebook:thrift:concurrency中实现了Thrift线程库，有三种组件：

- primitives

- thread pool manager

- timer manager


如上所述，我们不愿意在Thrift中引入任何额外的依赖性。我们决定使用boost::shared_ptr，因为它对多线程应用来说非常有用，它不要求链接时或运行时库，并且即将成为C++0x标准的一部分。

我们实现了标准的Mutex和Conditioin类，以及一个Monitor类。后者仅仅是一个mutex与condition变量的组合，与Java Object类提供的Monitor实现类似。Thrift提供一个Synchronized守卫类，允许类似Java的同步块。与Java不同的是，我们仍然拥有在编程时lock，unlock，block和signal monitors的能力。

```java
void run() {
    {
        Synchronized s(manager - > monitor);
        if (manager - > state == TimerManager::STARTING) {
            manager - > state = TimerManager::STARTED;
            manager - > monitor.notifyAll();
        }
    }
}
```

Thrift借用了Java的一个thread与一个runnable类的区别。一个thread是一个实际可调度的对象。Runnable类是在线程内执行的逻辑。Thread实现处理所有与平台相关的线程创建和销毁问题，而Runnable类的实现处理每个线程与应用相关的逻辑。这一方法的好处是开发者可以轻易地从Runnable类继承子类，而不会牵扯到平台相关的超类。

## 7.6 Thread，Runnable and shared_ptr

​	贯穿ThreadManager和TimerManager的实现，我们使用boost::shared_ptr来保证可被多个线程接入的已死对象的清理。

​	线程创建要求调用一个C库。典型地，操作系统几乎不能保证C线程的进入点函数ThreadMain何时会被调用。因此，我们的线程创建调用——ThreadFactory::newThread()可能在操作系统调用ThreadMain之前就返回给调用者。如果调用者在ThreadMain调用之前放弃了对线程的引用，为确保返回的线程对象不会被过早地清理，线程对象在它的start方法中制造了一个指向它自身的弱引用。

​	有了这个弱引用，ThreadMain函数能够在进入绑定到该线程的Runnable对象的Runnable::run方法之前，尝试获得一个强引用。倘若在离开Thread::start和进入ThreadMain之间，没能获得对该线程的强引用，那么弱引用返回null，函数将立刻退出。

​	线程制造一个对它自身的弱引用的需求，对API有着重大影响。因为引用是通过boost::shared_ptr模板管理的，线程对象必须有一个返回给调用者的、同样以boost::shared_ptr封装的指向它自身的引用。这需要使用工厂模式。ThreadFactory创建一个原始的线程对象，以及一个boost::shared_ptr包装器，并调用实现了Thread接口的类的一个私有helper方法，以使它能够通过boost::shared_ptr封装制造添加对它自身的弱引用。

​	Thread和Runnable对象彼此互相引用。一个Runnable对象可能需要知道它在其中执行的线程的信息，而一个Thread显然也需要知道它正在主持什么Runnable对象。这一相关性非常复杂，因为各对象的生命周期与其它对象是不相关的。一个应用可能创建一组Runnable对象，重用在不同线程里，或者它可能创建一个Runnable对象，一旦有一个线程为之创建并启动，它可能会忘记这个Runnable对象。

​	Thread类在它的构造函数中，使用一个boost::shared_ptr引用它所主持的Runnable对象，而Runnable类有显式的thread方法（ThreadFactory::newThread），允许地显式绑定主持的线程。

## 7.7 ThreadManager

​	ThreadManager创建一池工作者线程，一旦有空闲的工作者线程，应用就可以调度任务来执行。ThreadManager并未实现动态线程池大小的调整，但提供了原语，以便应用能基于负载添加和移除线程。Thrift把复杂的API抽象留给特定应用，提供原语以制定所期望的政策，并对当前状态进行采样。

## 7.8 TimerManager

​	TimerManager允许应用在未来某个时间点调度Runnable对象以执行。它具体的工作是允许应用定期对ThreadManager的负载进行抽样，并根据应用的方针使线程池大小发生改变。TimerManager也能用于生成任意数量的定时器或告警事件。

​	TimerManager的默认实现，使用了单个线程来处理过期的Runnable对象。因此，如果一个定时器操作需要做大量工作，尤其是如果它需要阻塞I/O，则应当在一个单独的线程中完成。

## 7.9 Nonblocking Operation（非阻塞操作）

​	尽管Thrift传输接口更直接地映射到一个阻塞I/O模型，然而Thrift基于libevent和TFramedTransport，用C++实现了一个高性能的TNonBlockingServer。这是通过使用状态机，把所有I/O移动到一个严密的事件循环中来实现的。实质上，事件循环将成帧的请求读入TMemoryBuffer对象。一旦全部请求ready，它们会被分发给TProcessor对象，该对象能直接读取内存中的数据。

## 7.10 Compiler（编译器）

​	Thrift编译器是使用C++实现的。尽管若用另一种语言来实现，代码行数可能会少，但使用C++能够强制语言结构的显示定义，使代码对新的开发者来说更容易接近。

​	代码生成使用两遍pass完成。第一遍只看include文件和类型定义。这一阶段，并不检查类型定义，因为它们可能依赖于include文件。第一次pass，所有包含的文件按顺序被扫描一遍。一旦解析了include树，第二遍pass过所有文件，将类型定义插入语法树，如果有任何未定义的类型，则引发一个error。然后，根据语法树生成程序。

​	由于固有的复杂性以及潜在的循环依赖性，Thrift显式地禁止前向声明。两个Thrift结构不能各自包含对方的一个实例。

## 7.11 TFileTransport

​	TFileTransport通过将来的数据及数据长度成帧，并将它写到磁盘上，来对Thrift的请求/结构作日志。使用一个成帧的磁盘上格式，允许了更好的错误检查，并有助于处理有限数目的离散事件。TFileWriterTransport使用一个交换内存中缓冲区的系统，来确保作大量数据的日志时的高性能。一个Thrift日志文件被分裂成某一特定大小的块，被记入日志的信息不允许跨越块的边界。如果有一个可能跨越块边界的消息，则添加填塞直到块的结束，并且消息的第一个字节与下一个块的开始对齐。将文件划分成块，使从文件的一个特定点读取及解释数据成为可能。

# 8 Facebook Thrift Services（Facebook的Thrift服务）

​	Facebook中已经大量使用了Thrift，包括搜索、日志、手机、广告和开发者平台。下面讨论两种具体的使用。

## 8.1 Search（搜索）

​	Facebook搜索服务使用Thrift作为底层协议和传输层。多语言的代码生成很适合搜索，因为可以用高效的服务器端语言（C++）进行应用开发，并且Facebook基于PHP的web应用可以使用Thrift PHP库调用搜索服务。Thrift使搜索团队能够利用各个语言的长处，快速地开发代码。

## 8.2 Logging（日志）

​	使用Thrift TFileTransport功能进行结构化的日志。可认为各服务函数定义以及它的参数是一个结构化的日志入口，由函数名识别。

# 9 Conclusions（总结）

​	通过让工程师能够“分而治之”，Thrift使得Facebook能够高效地建立可扩展的后端服务。应用开发者能专注于应用代码，而不用担心socket层。通过在一个地方编写缓冲及I/O逻辑，而不是把它散布在各个应用中，我们避免了重复的工作。

​	Thrift已被使用在Facebook的搜索、日志、手机、广告和开发者平台等一系列广泛的应用上。一个额外的软件抽象层引起的微小的工作开销，远远比不上开发者效率和系统可靠性方面的收益。