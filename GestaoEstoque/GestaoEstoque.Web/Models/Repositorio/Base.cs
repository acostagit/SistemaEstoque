using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace GestaoEstoque.Web.Models.Repositorio
{
    public class Base
    {
        private String _stringConexao = null;
        public string ConsultarStringConexao()
        {
            if (this._stringConexao == null) _stringConexao = ConfigurationManager.ConnectionStrings["EstoqueConn"].ConnectionString;
            return _stringConexao;
        }

    }
}