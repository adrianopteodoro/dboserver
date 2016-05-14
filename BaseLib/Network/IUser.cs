/*
 * Criado por SharpDevelop.
 * Usuário: Adriano
 * Data: 1/12/2011
 * Hora: 18:35
 * 
 * Para alterar este modelo use Ferramentas | Opções | Codificação | Editar Cabeçalhos Padrão.
 */

using System;

namespace BaseLib.Network
{
	/// <summary>
	/// Description of IUser.
	/// </summary>
	public interface IUser
    {
        IClient Client { get; set; }
    }
}
