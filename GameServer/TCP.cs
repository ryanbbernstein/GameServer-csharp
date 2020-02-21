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

			public void ConnectSocket(TcpClient socket)
			{
				mSocket = socket;
				mSocket.ReceiveBufferSize = Constants.BUFFER_SIZE;
				mSocket.SendBufferSize = Constants.BUFFER_SIZE;

				_stream = mSocket.GetStream();

				_dataBuffer = new byte[Constants.BUFFER_SIZE];

				_stream.BeginRead(_dataBuffer, 0, Constants.BUFFER_SIZE, ReceiveData, null);
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
				}
				catch (Exception ex)
				{
					Console.WriteLine("TCP - Error receiving data: " + ex);
				}
			}
		}
	}
}
