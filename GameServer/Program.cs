using System;

namespace GameServer
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.Title = "Game Server";

			Server server = new Server(50, 5000);

			server.Start();

			Console.ReadKey();
		}
	}
}
