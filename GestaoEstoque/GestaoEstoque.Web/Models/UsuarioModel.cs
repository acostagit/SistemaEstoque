using GestaoEstoque.Web.Models.Repositorio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GestaoEstoque.Web.Models
{
    public class UsuarioModel : Base
    {

        public static bool Validar(string login, string senha)
        {
            var validado = false;
            using (var conexao = new SqlConnection())
            {
                //conexao.ConnectionString = @"Data Source=DESKTOP-99S90JI\SQLEXPRESS; Initial Catalog=ControleEstoque; User Id=admin;Password=123";
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["EstoqueConn"].ConnectionString;
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText =string.Format("SELECT  count(*) FROM Usuario WHERE login='{0}' AND senha='{1}'", login, senha);
                    validado = (int)comando.ExecuteScalar() > 0;
                }
            }

            return validado;
        }

        //A forma abaixo é REFATORACAO DO CODIGO...EVOLUÇÃO
        ////public static bool Validar(string login, string senha)
        ////{
        ////    return ConsultarUsuario();
        ////}

        ////public bool ConsultarUsuario()
        ////{
        ////    var validado = false;
        ////    using (SqlConnection conexao = new SqlConnection(ConsultarStringConexao()))
        ////    {
        ////        conexao.Connection = conexao;
        ////        conexao.Open();
        ////        SqlDataReader dr = sqlComm.ExecuteReader();

        ////        using (SqlCommand sqlComm = new SqlCommand())
        ////        {
        ////            sqlComm.CommandType = CommandType.StoredProcedure;
        ////            sqlComm.CommandText = "SPR_CONSULTAR_USUARIO";
        ////        }

        ////        validado = true;
        ////        while (dr.Read())
        ////        {
        ////            //Temperatura temperatura = new Temperatura();
        ////            //temperatura = ConverterDataReaderParaObjetoCurto(dr);
        ////            //lista.Add(temperatura);
        ////        }
        ////    }
        ////    return validado;
        ////}
    }
}