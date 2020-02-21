using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer
{
	class ClientConnection
	{
		private int _ID;
		private TCP _Tcp;

		public ClientConnection(int id)
		{
			_ID = id;
			_Tcp = new TCP(_ID);
		}

	}
}
