using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace GameServer
{
	namespace ConnectionType
	{
		class TCP
		{
			private TcpClient _socket;

			private readonly int _id;
			private NetworkStream _stream;
			private byte[] _dataBuffer;

			public TCP(int id)
			{
				_id = id;
			}

			public bool IsConnected()
			{
				return (_socket != null && _socket.Connected);
			}

			public bool ConnectSocket(TcpClient socket)
			{
				_socket = socket;
				_socket.ReceiveBufferSize = Constants.BUFFER_SIZE;
				_socket.SendBufferSize = Constants.BUFFER_SIZE;

				_stream = _socket.GetStream();

				_dataBuffer = new byte[Constants.BUFFER_SIZE];

				_stream.BeginRead(_dataBuffer, 0, Constants.BUFFER_SIZE, ReceiveData, null);

				return true;
			}

			private void ReceiveData(IAsyncResult result)
			{
				try
				{
					int dataLength = _stream.EndRead(result);
					if (dataLength <= 0)
					{
						return;
					}

					byte[] data = new byte[dataLength];
					Array.Copy(_dataBuffer, data, dataLength);

					_stream.BeginRead(_dataBuffer, 0, dataLength, ReceiveData, null);
				}
				catch (Exception ex)
				{
					Console.WriteLine("TCP - Error receiving data: " + ex);
				}
			}
		}
	}
}
