using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace GameServer
{
	class Server
	{
		public static int MaxConnections { get; private set; }

		public static int TcpPort { get; private set; }
		public static TcpListener _TcpListener;

		public void StartServer(int maxConnections, int port)
		{
			MaxConnections = maxConnections;
			TcpPort = port;

			_TcpListener = new TcpListener(IPAddress.Any, TcpPort);
			_TcpListener.Start();
			_TcpListener.BeginAcceptTcpClient(new AsyncCallback(NewTcpConnection), null);
		}

		private static void NewTcpConnection(IAsyncResult _result)
		{
			TcpClient client = _TcpListener.EndAcceptTcpClient(_result);
			_TcpListener.BeginAcceptTcpClient(new AsyncCallback(NewTcpConnection), null);
		}
	}
}
