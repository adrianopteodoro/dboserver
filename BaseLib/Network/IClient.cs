/*
 * Criado por SharpDevelop.
 * Usuário: Adriano
 * Data: 30/11/2011
 * Hora: 21:46
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

namespace BaseLib.Network
{
	/// <summary>
	/// Description of IClient.
	/// </summary>
	public interface IClient
	{
		bool IsConnected { get; }
        IPEndPoint RemoteEndPoint { get; }
        IPEndPoint LocalEndPoint { get; }
        IUser User { get; set; }
        Socket _Socket { get; }
        
        int Send(byte[] buffer);
        int Send(byte[] buffer, SocketFlags flags);
        int Send(byte[] buffer, int start, int count);
        int Send(byte[] buffer, int start, int count, SocketFlags flags);

        void Disconnect();
	}
}
