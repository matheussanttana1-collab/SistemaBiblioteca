using SistemaBiblioteca.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaBiblioteca.Serviços
{
    internal class Biblioteca
    {
        public Dictionary<Guid,Livro> livros { get; private set; } = new Dictionary<Guid, Livro>();
		public Dictionary<Guid,Usuario> usuarios { get; private set; } = new Dictionary<Guid, Usuario>();
        public List<Emprestimo> emprestimos { get; } = new List<Emprestimo>();

		public void AdicionarLivro(Livro livro)
        {
            if (livros.ContainsKey(livro.Id))
            {
                throw new InvalidOperationException($"O Livro {livro.Titulo} Ja esta Cadstrado");
            }
			livros.Add(livro.Id,livro);

		}
        public void AdicionarUsuario(Usuario usuario)
        {
            if (usuarios.ContainsKey(usuario.IdUsuario))
            {
                throw new InvalidOperationException($"O usuario {usuario.Name} ja Esta Cadastrado");
            }
			usuarios.Add(usuario.IdUsuario, usuario);

		}
        public void CriarEmprestimo(Usuario usuario, Livro livro)
        {
            if (!usuarios.ContainsKey(usuario.IdUsuario))
            {
                throw new InvalidOperationException($"Usuario Não Cadastrado, Realize o cadastro e tente Novamente");
            }
            if (!livros.ContainsKey(livro.Id))
            {
                throw new InvalidOperationException("Livro não existe na biblioteca");
            }
			var novoEmprestimo = new Emprestimo(livro, usuario);
            livro.Emprestar();
            usuario.AdicionarEmprestimoAoUsuario(novoEmprestimo);
			emprestimos.Add(novoEmprestimo);
		}

        public void DevolverLivro(Usuario usuario,Emprestimo emprestimo, Livro livro)
        {
            usuario.DevolverLivro(emprestimo);
            livro.Devolver();
            emprestimo.FinalizarEmprestimo();
            
        }

        public void ReservarLivro(Usuario usuario, Livro livro)
        {
			if (!usuarios.ContainsKey(usuario.IdUsuario))
			{
				throw new InvalidOperationException($"Usuario Não Cadastrado, Realize o cadastro e tente Novamente");
			}
			if (!livros.ContainsKey(livro.Id))
			{
				throw new InvalidOperationException("Livro não existe na biblioteca");
			}
			livro.Reservar();
		}
    }
}
