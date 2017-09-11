using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GestaoEstoque.Web.Models
{
    public class GrupoProdutoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o campo Nome")]
        public string Nome { get; set; }

        public bool Ativo { get; set; }

        public static List<GrupoProdutoModel> RecuperarLista()
        {
            var lista = new List<GrupoProdutoModel>();

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["EstoqueConn"].ConnectionString;
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = "SELECT  * FROM GrupoProduto";
                    var reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        lista.Add(new Models.GrupoProdutoModel
                        {
                            Id = (int)reader["Id"],
                            Nome = (string)reader["Nome"],
                            Ativo = (bool)reader["Ativo"]
                        });
                    }
                }
            }

            return lista;
        }

        public static GrupoProdutoModel RecuperarGrupoProdutoPorId(int id)
        {
            GrupoProdutoModel model = null;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["EstoqueConn"].ConnectionString;
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = string.Format("SELECT  * FROM GrupoProduto WHERE ID= {0}", id);
                    var reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        model = new GrupoProdutoModel();
                        model.Id = (int)reader["Id"];
                        model.Nome = (string)reader["Nome"];
                        model.Ativo = (bool)reader["Ativo"];
                    }
                }
            }

            return model;
        }

        public static bool ExcluirPorId(int id)
        {
            //GrupoProdutoModel model = null;
            var ret = false;
            if (RecuperarGrupoProdutoPorId(id) != null)
            {

                using (var conexao = new SqlConnection())
                {
                    conexao.ConnectionString = ConfigurationManager.ConnectionStrings["EstoqueConn"].ConnectionString;
                    conexao.Open();

                    using (var comando = new SqlCommand())
                    {
                        comando.Connection = conexao;
                        comando.CommandText = string.Format("DELETE FROM GrupoProduto WHERE ID= {0}", id);
                        ret = (comando.ExecuteNonQuery() > 0);
                    }
                }
            }

            return ret;
        }

        public int Salvar()
        {
            var ret = 0;
            var model = RecuperarGrupoProdutoPorId(this.Id);


            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = ConfigurationManager.ConnectionStrings["EstoqueConn"].ConnectionString;
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    if (model == null)
                    {
                        comando.Connection = conexao;
                        comando.CommandText = string.Format("INSERT INTO GrupoProduto (Nome, Ativo) VALUES('{0}', {1});  SELECT CONVERT(int,scope_identity())", this.Nome, this.Ativo ? 1 : 0);
                        ret = (int)comando.ExecuteScalar();
                    }
                    else
                    {
                        comando.Connection = conexao;
                        comando.CommandText = string.Format("UPDATE GrupoProduto SET Nome='{1}', Ativo={2} WHERE Id={0}", this.Id, this.Nome, this.Ativo ? 1 : 0);
                        if (comando.ExecuteNonQuery() > 0)
                        {
                            ret = this.Id;
                        }
                    }


                    return ret;
                }

                ////public int Alterar()
                ////{
                ////    var ret = 0;

                ////    var model = RecuperarGrupoProdutoPorId(this.Id);

                ////    using (var conexao = new SqlConnection())
                ////    {
                ////        conexao.ConnectionString = @"Data Source=DESKTOP-99S90JI\SQLEXPRESS; Initial Catalog=ControleEstoque; User Id=admin;Password=123";
                ////        conexao.Open();

                ////        using (var comando = new SqlCommand())
                ////        {
                ////            comando.Connection = conexao;
                ////            if (model != null)
                ////            {
                ////                comando.CommandText = string.Format("UPDATE INTO GrupoProduto Nome, Ativo '{0}', {1}  SELECT CONVERT(int,scope_identity())", this.Nome, this.Ativo ? 1 : 0);
                ////                ret = (int)comando.ExecuteScalar();
                ////            }
                ////            else
                ////            {
                ////                comando.CommandText = string.Format("UPDATE INTO GrupoProduto Nome, Ativo '{0}', {1}  SELECT CONVERT(int,scope_identity())", this.Nome, this.Ativo ? 1 : 0);
                ////            }
                ////        }

                ////    }

                ////    return ret;
                ////}


            }
        }
    }
}