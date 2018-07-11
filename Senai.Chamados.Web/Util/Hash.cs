using System.Security.Cryptography;
using System.Text;

namespace Senai.Chamados.Web.Util
{
    public class Hash
    {
        /// <summary>
        /// Retorna um texto encriptografado
        /// </summary>
        /// <param name="Texto">Texto que irá ser encriptografado</param>
        /// <returns>Retorna o texto encriptografado</returns>
        public static string GerarHash(string Texto)
        {
            //Declaro uma variavel do tipo String Builder
            StringBuilder result = new StringBuilder();

            //Declaro uma variavel do Tipo SHA256 para encriptografia
            SHA256 sha256 = SHA256Managed.Create();
            //Converto o texto recebido como parametro em bytes
            byte[] bytes = Encoding.UTF8.GetBytes(Texto);
            //Gera um hash de acordo com a variavel bytes
            byte[] hash = sha256.ComputeHash(bytes);
            //Percorre o hash e vai concatenando o texto
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X"));
            }

            //Retorna o texto encriptografado
            return result.ToString();
        }
    }
}