/*
 * Criado por SharpDevelop.
 * Usuário: Adriano
 * Data: 1/12/2011
 * Hora: 0:17
 * 
 * Para alterar este modelo use Ferramentas | Opções | Codificação | Editar Cabeçalhos Padrão.
 */

using System;

namespace BaseLib.Network
{
	/// <summary>
	/// Description of ConnectionEventArgs.
	/// </summary>
	public class ClientEventArgs : EventArgs
    {
		private IClient _Client = null;
        public IClient Client {
			get {return _Client;}
			private set {_Client = value;}
		}

        public ClientEventArgs(IClient client)
        {
            if (client == null) throw new ArgumentNullException("client");
            this.Client = client;
        }

        public override string ToString()
        {
            return Client.RemoteEndPoint != null
                ? Client.RemoteEndPoint.ToString()
                : "Not Connected";
        }
    }
}
