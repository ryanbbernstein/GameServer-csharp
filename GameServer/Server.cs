using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace GameServer
{
	class Server
	{
		public int MaxConnections { get; private set; }
		public int TcpPort { get; private set; }

		public Dictionary<int, ClientConnection> conntections = new Dictionary<int, ClientConnection>();

		private TcpListener _tcpListener;

		public Server(int maxConnections, int port)
		{
			MaxConnections = maxConnections;
			TcpPort = port;

			for (int i = 1; i <= MaxConnections; i++)
			{
				conntections.Add(i, new ClientConnection(i));
			}
		}

		public void Start()
		{
			Console.WriteLine("Server - Starting...");

			_tcpListener = new TcpListener(IPAddress.Any, TcpPort);
			_tcpListener.Start();
			_tcpListener.BeginAcceptTcpClient(new AsyncCallback(NewTcpConnection), null);

			Console.WriteLine("Server - Started on port " + TcpPort + ".");
		}

		private void NewTcpConnection(IAsyncResult _result)
		{
			TcpClient client = _tcpListener.EndAcceptTcpClient(_result);
			_tcpListener.BeginAcceptTcpClient(new AsyncCallback(NewTcpConnection), null);
			Console.WriteLine("Server - Connection request from " + client.Client.RemoteEndPoint + "...");

			for (int i = 1; i <= MaxConnections; i++)
			{
				if (!conntections[i].IsConnected())
				{
					if (conntections[i].Connect(client))
					{
						Console.WriteLine("Server - Connected to client " + client.Client.RemoteEndPoint + ".");
						return;
					} else
					{
						continue;
					}
				}
			}

			Console.WriteLine("Server - " + client.Client.RemoteEndPoint + " failed to connect! Server Full!");
		}
	}
}
