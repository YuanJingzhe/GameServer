/*
*When writing this code, only Pig Piggy and I know what it is doing.
*Now, only Pig Piggy knows.
*Time：2017/12/20 16:05:15
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhpilyServer
{
    /// <summary>
    /// 客户端连接池
    /// </summary>
    class ClientPeerPool
    {
        private Queue<ClientPeer> clientPeerPool;

        public ClientPeerPool(int count) {
            clientPeerPool = new Queue<ClientPeer>(count);
        }

        public void Enqueue(ClientPeer client) {
            clientPeerPool.Enqueue(client);
        }
        public ClientPeer Dequeue() {
            return clientPeerPool.Dequeue();
        }
    }
}
