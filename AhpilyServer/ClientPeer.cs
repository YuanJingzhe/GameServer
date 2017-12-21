/*
*When writing this code, only Pig Piggy and I know what it is doing.
*Now, only Pig Piggy knows.
*Time：2017/12/20 15:32:04
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
namespace AhpilyServer
{
    class ClientPeer
    {
        private Socket clientSocket;

        public void SetSocket(Socket clientSocket) {
            this.clientSocket = clientSocket;
        }

        #region 接收数据
        /// <summary>
        /// 接收消息的缓冲区
        /// </summary>
        private List<byte> dataCache = new List<byte>();
        #endregion
    }
}
