using GameServer.ConnectionType;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace GameServer
{
	class ClientConnection
	{
		private int _id;
		private TCP _tcpConnection;

		public ClientConnection(int id)
		{
			_id = id;
			_tcpConnection = new TCP(_id);
		}
		
		public bool IsConnected()
		{
			return _tcpConnection.IsConnected();
		}

		public bool Connect(TcpClient client)
		{
			return _tcpConnection.ConnectSocket(client);
		}
	}
}
