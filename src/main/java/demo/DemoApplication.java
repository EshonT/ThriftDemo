package demo;

import com.alibaba.fastjson.JSON;
import demo.thrift.TestClass;
import demo.thrift.ThriftService;
import org.apache.thrift.TException;
import org.apache.thrift.protocol.TBinaryProtocol;
import org.apache.thrift.protocol.TProtocol;
import org.apache.thrift.transport.TSocket;
import org.apache.thrift.transport.TTransport;

import java.util.Arrays;
import java.util.HashMap;
import java.util.Map;


/**
 * @author TangYiXiong
 * @since 2020/10/29 18:34
 */
public class DemoApplication {

    /**
     * 服务端IP
     */
    private static final String SERVER_IP = "127.0.0.1";

    /**
     * 服务端设置的端口  58622
     */
    private static final int SERVER_PORT = 58622;

    /**
     * socket连接超时时间
     */
    private static final int TIMEOUT = 3000;

    public static void main(String[] args) {
        try {
            long t1 = System.currentTimeMillis();
            TTransport transport = new TSocket(SERVER_IP, SERVER_PORT, TIMEOUT);
            TProtocol protocol = new TBinaryProtocol(transport);        //协议要和服务端一致   使用二进制协议
            //创建Client
            ThriftService.Client client = new ThriftService.Client(protocol);

            transport.open();

            long t2 = System.currentTimeMillis();

            Map<String, String> machine = new HashMap<String, String>() {{
                put("416", "192.168.56.1.1.1");
                put("511/611", "192.168.56.1.1.1");
            }};

            client.Connect(machine);

            long t3 = System.currentTimeMillis();

            Map<String, Integer> result = client.GetValue();
            System.out.println(JSON.toJSONString(result));

            Map<String, TestClass> mapTest = client.GetMap();

            long t4 = System.currentTimeMillis();

            //关闭资源
            transport.close();
            long t5 = System.currentTimeMillis();

            System.out.println("connectThrift:\t" + (t2 - t1) + "\nconnectPLC:\t\t" + (t3 - t2) + "\ngetAndReturn:\t" + (t4 - t3) + "\nclose:\t\t\t" + (t5 - t4));

        } catch (TException e) {
            System.out.println("e.getMessage() = " + e.getMessage() + Arrays.toString(e.getStackTrace()));
            //e.getStackTrace();
        }
    }
}
