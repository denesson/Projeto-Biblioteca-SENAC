using Biblioteca.Controllers;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Projeto_Biblioteca_SENAC.Models;
using System.Collections.Generic;

namespace Projeto_Biblioteca_SENAC.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Edicao(int id)
        {
            Usuario u = new UsuarioService().ObterPorId(id);
            return View(u);
        }

        [HttpPost]

        public IActionResult Edicao(Usuario userEditado)
        {
            UsuarioService us = new UsuarioService();
            us.UpdateUsuario(userEditado);

            return RedirectToAction ("Lista");
        }

        public IActionResult Lista()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioAdmin(this);
            UsuarioService service = new UsuarioService();
            return View (service.GetUsuarios());
        }
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Usuario novoUser)
        {
            UsuarioService service = new UsuarioService();

            novoUser.senha = Criptografo.TextoCriptografado(novoUser.senha);

            UsuarioService us = new UsuarioService();
            us.incluirUsuario(novoUser);

            return RedirectToAction("cadastroRealizado");
        }

        public IActionResult cadastroRealizado()
        {
            return View();
        }

        public IActionResult EdicaoRealizada()
        {
            return View();
        }

        public IActionResult ExclusaoRealizada()
        {
            return View();
        }

        public IActionResult Excluir(int id)
        {
            Autenticacao.verificaSeUsuarioAdmin(this);
            UsuarioService service = new UsuarioService();
            Usuario usuario = service.GetUsuarioDetail(id);

            return View(usuario);
        }

        [HttpPost]
        public IActionResult Excluir(int Id, string decisao)
        {
            if(decisao == "s")
            {
                UsuarioService service = new UsuarioService();
                service.DeleteUsuario(Id);
            }else {
                return RedirectToAction("Lista");
            }

            return RedirectToAction("ExclusaoRealizada");
        }

        public IActionResult NeedAdmin()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View ("Login", "Usuario");
        }
    }
}