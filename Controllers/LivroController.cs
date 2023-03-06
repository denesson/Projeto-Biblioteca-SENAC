using System;
using System.Collections.Generic;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Biblioteca.Controllers
{
    public class LivroController : Controller
    {
        public IActionResult Cadastro()
        {
            Autenticacao.verificaSeUsuarioAdmin(this);
            Autenticacao.CheckLogin(this);
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Livro l)
        {
            LivroService livroService = new LivroService();

            if(l.Id == 0)
            {
                livroService.Inserir(l);
            }
            else
            {
                livroService.Atualizar(l);
            }

            return RedirectToAction("NeedAdmin");
        }

        public IActionResult Edicao(int id)
        {
            Autenticacao.verificaSeUsuarioAdmin(this);
            Autenticacao.CheckLogin(this);
            LivroService ls = new LivroService();
            Livro l = ls.ObterPorId(id);
            return View(l);
        }
        public IActionResult Listagem(string tipoFiltro, string filtro, string itensPorPagina, int NumDaPagina, int PaginaAtual)
        {
            Autenticacao.CheckLogin(this);
            FiltrosLivros objFiltro = null;
            if (!string.IsNullOrEmpty(filtro))
            {
                objFiltro = new FiltrosLivros();
                objFiltro.Filtro = filtro;
                objFiltro.TipoFiltro = tipoFiltro;
            }

            ViewData["livrosPorPagina"] = (string.IsNullOrEmpty(itensPorPagina) ? 10 : Int32.Parse(itensPorPagina));
            ViewData["PaginaAtual"] = (PaginaAtual != 0 ? PaginaAtual : 1);

            LivroService livroService = new LivroService();
            return View(livroService.ListarTodos(objFiltro));
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View ("Login", "Usuario");
        }
    }
}