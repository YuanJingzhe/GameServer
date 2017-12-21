/*
*When writing this code, only Pig Piggy and I know what it is doing.
*Now, only Pig Piggy knows.
*Time：2017/12/20 11:19:26
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace AhpilyServer
{
    /// <summary>
    /// 服务器端
    /// </summary>
    public class ServerPeer
    {
        /// <summary>
        /// 服务器端的socket对象
        /// </summary>
        private Socket serverSocket;

        /// <summary>
        /// 限制客户端连接数量的信号量
        /// </summary>
        private Semaphore acceptSemaphore;
        
        /// <summary>
        /// 客户端对象池
        /// </summary>
        private ClientPeerPool clientPeerPool;
        
  
        /// <summary>
        /// 开启服务器
        /// </summary>
        /// <param name="port">端口号</param>
        /// <param name="maxCount">最大连接数量</param>
        public void Start(int port, int maxCount)
        {
            try
            {
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                acceptSemaphore = new Semaphore(maxCount, maxCount);
                clientPeerPool = new ClientPeerPool(maxCount);
                ClientPeer tempClientPeer = null;
                for (int i = 0; i < maxCount; i++)
                {
                    tempClientPeer = new ClientPeer();
                    clientPeerPool.Enqueue(tempClientPeer);
                }

                serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));              
                serverSocket.Listen(maxCount);
                Console.WriteLine("服务器启动");
                startAccept(null);
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
            }
        }

        #region 接受连接
        /// <summary>
        /// 开始等待客户端
        /// </summary>
        private void startAccept(SocketAsyncEventArgs e)
        {
            if (e == null) {
                e = new SocketAsyncEventArgs();
                e.Completed += accept_Complete;
            }
            //限制线程的访问
            acceptSemaphore.WaitOne();
            bool result = serverSocket.AcceptAsync(e);
            //返回值判断事件是否执行完毕，如果为false，代表处理完成。如果为true，表示正在执行，执行完成后会触发某个事件

            if (!result)
            {
                processAccept(e);
            }
        }
        /// <summary>
        /// 接受连接请求异步事件完成时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accept_Complete(object sender,SocketAsyncEventArgs e) {
            processAccept(e);
        }
        /// <summary>
        /// 处理连接请求
        /// </summary>
        /// <param name="e"></param>
        private void processAccept(SocketAsyncEventArgs e) {
            ClientPeer client = clientPeerPool.Dequeue();
            client.SetSocket(e.AcceptSocket);
            //对得到的客户端对象进行保存，然后处理
            //TODO

            e.AcceptSocket = null;
            startAccept(e);
        }
        #endregion

        #region 断开连接

        #endregion

        #region 接受数据

        #endregion

        #region 发送数据

        #endregion
    }
}
