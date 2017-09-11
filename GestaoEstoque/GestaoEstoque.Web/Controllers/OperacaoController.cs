using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestaoEstoque.Web.Controllers
{
    public class OperacaoController : Controller
    {
        public ActionResult EntradaEstoque()
        {
            return View();
        }
    }
}