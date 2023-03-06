using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Models
{
    public class EmprestimoService 
    {
        public void Inserir(Emprestimo e)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Emprestimos.Add(e);
                bc.SaveChanges();
            }
        }

        public void Atualizar(Emprestimo e)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                Emprestimo emprestimo = bc.Emprestimos.Find(e.Id);
                emprestimo.NomeUsuario = e.NomeUsuario;
                emprestimo.Telefone = e.Telefone;
                emprestimo.LivroId = e.LivroId;
                emprestimo.DataEmprestimo = e.DataEmprestimo;
                emprestimo.DataDevolucao = e.DataDevolucao;

                bc.SaveChanges();
            }
        }

        public ICollection<Emprestimo> ListarTodos(FiltrosEmprestimos filtro)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Emprestimos.Include(e => e.Livro).ToList();
            }
        }

        public Emprestimo ObterPorId(int id)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Emprestimos.Find(id);
            }
        }

        public ICollection<Emprestimo> GetEmprestimosFull()
        {
            using (var context = new BibliotecaContext())
            {
                IQueryable<Emprestimo> consulta =
                    context.Emprestimos.Include(p => p.Livro).OrderByDescending(p => p.LivroId);

                return consulta.ToList();
            }
        }

        public ICollection<Emprestimo> GetEmprestimosFull(int page, int size)
        {
            using(var context = new BibliotecaContext())
            {
                int pular = (page - 1) * size;

                IQueryable<Emprestimo> consulta =
                context.Emprestimos.Include(p => p.Livro).OrderByDescending(p => p.LivroId);

                return consulta.Skip(pular).Take(size).ToList();
            }
        }

        public int CountEmprestimos()
        {
            using (var context = new BibliotecaContext())
            {
                return context.Emprestimos.Count();
            }
        }
    }
}