# WpfTool

## Tcp模块
其中默认读取的数据包格式为[lenght]+[data]其中lenght为接收到数据长度，data为实际的数据
因此发送的数据也是[lenght]+[data]格式。具体的粘包细节参考SocketHelper.cs文件
### TcpServer
为tcp的服务端默认支持2中模式并行不阻塞，并行阻塞;
### TcpClient
使用了多线程进行服务




