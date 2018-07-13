using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Senai.Chamados.Web.Helpers
{
    public class MeuHelper
    {
        public static string DireitosReservados()
        {
            return "®" + DateTime.Now.Year + " Todos os direitos reservados";
        }

        public static string BoasVindas()
        {
            return "Seja Bem vindo,";
        }


    }
}