# WpfTool

## Tcp模块
其中默认读取的数据包格式为[lenght]+[data]其中lenght为接收到数据长度，data为实际的数据
因此发送的数据也是[lenght]+[data]格式。具体的粘包细节参考SocketHelper.cs文件(自行修改成自己想要的数据格式)
![image](https://github.com/user-attachments/assets/504bae5e-726d-4da8-aef5-30a8586e6c36)

### TcpServer
为tcp的服务端默认支持2中模式并行不阻塞，并行阻塞;
内部还提供了心跳机制用于检测掉线
### TcpClient
使用了多线程进行服务
内部提供心跳机制
### SocketBase
为TcpServer、TcpClient的基类
### 提供IToken基类
如数据数据的接收重写*ExecuteReciveCommand*方法
数据的发送重写*ExecuteSendCommand*方法
数据包的消费重写*ExecuteCommand*方法
## EventModule 事件模块
具体使用方式参照内部Dome，模块默认使用的是微软的DI包Microsoft.Extensions.DependencyInjection
其中事件模块的执行为并行处理该模块考虑到线程安全的情况执行时添加了执行锁，确保每一次执行为不同消费者



