/*
 * Criado por SharpDevelop.
 * Usuário: Adriano
 * Data: 30/11/2011
 * Hora: 22:02
 * 
 * Para alterar este modelo use Ferramentas | Opções | Codificação | Editar Cabeçalhos Padrão.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using BaseLib.Packets;

namespace BaseLib.Network
{
	/// <summary>
	/// Description of Client.
	/// </summary>
	public class Client : IClient
	{
		private readonly Server _server;
		private  Socket __Socket = null;
        public  Socket _Socket {
			get {return __Socket;}
			private set {__Socket = value;}
		}
        private readonly byte[] _recvBuffer = new byte[BufferSize];
        public static readonly int BufferSize = 16 * 1024; // 16 KB  
        private IUser _User = null;
        public IUser User {
        	get {return _User;}
        	set {_User = value;}
        }
        
		public Client(Server server, Socket socket)
		{
			if (server == null) throw new ArgumentNullException("server");
            if (socket == null) throw new ArgumentNullException("socket");

            this._server = server;
            this._Socket = socket;
		}

        public bool IsConnected
        {
            get { return _Socket.Connected; }
        }

        public IPEndPoint RemoteEndPoint
        {
            get { return _Socket.RemoteEndPoint as IPEndPoint; }
        }

        public IPEndPoint LocalEndPoint
        {
            get { return _Socket.LocalEndPoint as IPEndPoint; }
        }

        public byte[] RecvBuffer
        {
            get { return _recvBuffer; }
        }

        public Socket Socket
        {
            get { return _Socket; }
        }

        public IAsyncResult BeginReceive(AsyncCallback callback, object state)
        {
            return _Socket.BeginReceive(_recvBuffer, 0, BufferSize, SocketFlags.None, callback, state);
        }

        public int EndReceive(IAsyncResult result)
        {
            return _Socket.EndReceive(result);
        }

        public int Send(byte[] buffer)
        {
            if (buffer == null) throw new ArgumentNullException("buffer");
            return Send(buffer, 0, buffer.Length, SocketFlags.None);
        }

        public int Send(byte[] buffer, SocketFlags flags)
        {
            if (buffer == null) throw new ArgumentNullException("buffer");
            return Send(buffer, 0, buffer.Length, flags);
        }

        public int Send(byte[] buffer, int start, int count)
        {
            if (buffer == null) throw new ArgumentNullException("buffer");
            return Send(buffer, start, count, SocketFlags.None);
        }

        public int Send(byte[] buffer, int start, int count, SocketFlags flags)
        {
            if (buffer == null) throw new ArgumentNullException("buffer");
            return _server.Send(this, buffer, start, count, flags);
        }
        
        public void Disconnect()
        {
            if (this.IsConnected)
                _server.Disconnect(this);
        }

        public override string ToString()
        {
            if (_Socket.RemoteEndPoint != null)
                return _Socket.RemoteEndPoint.ToString();
            else
                return "Not Connected";
        }
	}
}
