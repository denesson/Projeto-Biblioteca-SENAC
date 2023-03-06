using System.Linq;
using System.Collections.Generic;
using System.Collections;
using Biblioteca.Models;

namespace Projeto_Biblioteca_SENAC.Models
{
    public class UsuarioService
    {

        public List<Usuario> Listar(int id)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuarios.ToList();
            }
        }
        public void incluirUsuario(Usuario novoUser)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Add(novoUser);
                bc.SaveChanges();
            }
        }

        public ICollection<Usuario> GetUsuarios()
        {
            using (var context = new BibliotecaContext())
            {
                ICollection<Usuario> resultado = 
                context.Usuarios.ToList();
                
                return resultado;
            }
        }

        public void UpdateUsuario(Usuario userEditado)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                Usuario u = bc.Usuarios.Find(userEditado.Id);
                u.login = userEditado.login;
                u.Nome = userEditado.Nome;
                u.senha = userEditado.senha;
                u.tipo = userEditado.tipo;

                bc.SaveChanges();
            }
        }

        public Usuario ObterPorId(int id)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuarios.Find(id);
            }
        }

        public void DeleteUsuario(int Id)
        {
            using (var BibliotecaContext = new BibliotecaContext())
            {
                Usuario Registro = BibliotecaContext.Usuarios.Find(Id);
                BibliotecaContext.Usuarios.Remove(Registro);
                BibliotecaContext.SaveChanges();
            }
        }

        public Usuario GetUsuarioDetail(int id)
        {
            using (var context = new BibliotecaContext())
            {
                Usuario registro = context.Usuarios.Where(p => p.Id == id).SingleOrDefault();
                return registro;
            }
        }
    }
}