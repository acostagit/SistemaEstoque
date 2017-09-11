﻿using GestaoEstoque.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GestaoEstoque.Web.Controllers
{
    public class ContaController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel login, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            //var usuario = UsuarioModel.ValidarUsuario(login.Usuario, login.Senha);
            var achou = (UsuarioModel.Validar(login.Usuario, login.Senha));
            if (achou)
            {
                FormsAuthentication.SetAuthCookie(login.Usuario, login.LembrarMe);
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("","Login inválido!");
            }


            //if (usuario != null)
            //{
            //    var tiket = FormsAuthentication.Encrypt(new FormsAuthenticationTicket(
            //        1, usuario.Nome, DateTime.Now, DateTime.Now.AddHours(12), login.LembrarMe, PerfilModel.RecuperarPeloId(usuario.IdPerfil).Nome));
            //    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, tiket);
            //    Response.Cookies.Add(cookie);

            //    if (Url.IsLocalUrl(returnUrl))
            //    {
            //        return Redirect(returnUrl);
            //    }
            //    else
            //    {
            //        return RedirectToAction("Index", "Home");
            //    }
            //}
            //else
            //{
            //    ModelState.AddModelError("", "Login inválido.");
            //}

            return View(login);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}