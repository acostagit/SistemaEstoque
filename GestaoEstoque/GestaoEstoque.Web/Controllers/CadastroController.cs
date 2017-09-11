using GestaoEstoque.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestaoEstoque.Web.Controllers
{
    [AllowAnonymous]
    public class CadastroController : Controller
    {

        private static List<GrupoProdutoModel> lista = new List<GrupoProdutoModel>()
        {
            new GrupoProdutoModel() {Id=1, Nome="Galaxy S6", Ativo=true },
            new GrupoProdutoModel() {Id=2, Nome="Iphone 6", Ativo=true },
            new GrupoProdutoModel() {Id=3, Nome="Zenfone TT", Ativo=false},
        };

        [Authorize]
        public ActionResult GrupoProduto()
        {
            return View(GrupoProdutoModel.RecuperarLista());
        }

        [HttpPost]
        [Authorize]
        public ActionResult RecuperarGrupoProduto(int id)
        {
            //return Json(lista.Find(x => x.Id == id));
            return Json(GrupoProdutoModel.RecuperarGrupoProdutoPorId(id));
        }

        [HttpPost]
        [Authorize]
        public ActionResult ExcluirGrupoProduto(int id)
        {
            //Antes com BD em MemoriA
            ////var ret = false;

            ////var registro = lista.Find(x => x.Id == id);
            ////if (registro != null)
            ////{
            ////    lista.Remove(registro);
            ////    ret = true;
            ////}

            //////return Json(ret);
            return Json(GrupoProdutoModel.ExcluirPorId(id));
        }

        [HttpPost]
        [Authorize]
        public ActionResult SalvarGrupoProduto(GrupoProdutoModel model)
        {
            var resultado = "OK";
            var mensagens = new List<string>();
            var idSalvo = string.Empty;

            if (!ModelState.IsValid)
            {
                resultado = "AVISO";
                mensagens = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                try
                {
                    var id = model.Salvar();
                    if(id > 0)
                    {
                        idSalvo = id.ToString();
                    }
                    else
                    {
                        resultado = "ERRO";
                    }


                    //antes com Memoria
                    ////var registro = lista.Find(x => x.Id == model.Id);
                    ////if (registro == null)
                    ////{
                    ////    registro = model;
                    ////    registro.Id = lista.Max(x => x.Id) + 1;
                    ////    lista.Add(registro);
                    ////}
                    ////else
                    ////{
                    ////    registro.Nome = model.Nome;
                    ////    registro.Ativo = model.Ativo;
                    ////}

                    ////idSalvo = model.Id.ToString();
                }
                catch (Exception ex)
                {
                    resultado = "ERRO";
                }
            }

            return Json(new { Resultado = resultado, Mensagens = mensagens, IdSalvo = idSalvo });
        }

        //Metodo usado com BD em Memoria
        ////public ActionResult SalvarGrupoProduto(GrupoProdutoModel model)
        ////{
        ////    var resultado = "OK";
        ////    var mensagens = new List<string>();
        ////    var idSalvo = string.Empty;

        ////    if (!ModelState.IsValid)
        ////    {
        ////        resultado = "AVISO";
        ////        mensagens = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
        ////    } else
        ////    {
        ////        try
        ////        {
        ////            var registro = lista.Find(x => x.Id == model.Id);
        ////            if (registro == null)
        ////            {
        ////                registro = model;
        ////                registro.Id = lista.Max(x => x.Id) + 1;
        ////                lista.Add(registro);
        ////            }
        ////            else
        ////            {
        ////                registro.Nome = model.Nome;
        ////                registro.Ativo = model.Ativo;
        ////            }

        ////            idSalvo = model.Id.ToString();
        ////        }
        ////        catch (Exception ex)
        ////        {
        ////            resultado = "ERRO";
        ////        }
        ////    }

        ////    return Json(new { Resultado = resultado, Mensagens = mensagens, IdSalvo = idSalvo});
        ////}

        public ActionResult MarcaProduto()
        {
            return View();
        }

        public ActionResult LocalArmazenamento()
        {
            return View();
        }

        public ActionResult UnidadeMedida()
        {
            return View();
        }

        public ActionResult Produto()
        {
            return View();
        }

        public ActionResult Pais()
        {
            return View();
        }

        public ActionResult Estado()
        {
            return View();
        }

        public ActionResult Cidade()
        {
            return View();
        }

        public ActionResult Fornecedor()
        {
            return View();
        }

        public ActionResult PerfilUsuario()
        {
            return View();
        }

        public ActionResult Usuario()
        {
            return View();
        }
    }
}