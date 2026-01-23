using SistemaBiblioteca.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaBiblioteca.Serviços
{
    internal class Biblioteca
    {
        private Dictionary<Guid,Livro> Livros = new Dictionary<Guid, Livro>();
		private Dictionary<Guid,Usuario> Usuarios = new Dictionary<Guid, Usuario>();
        private List<Emprestimo> emprestimosAtivos = new List<Emprestimo>();
        private Stack<Emprestimo> HistoriDeEmprestimos = new Stack<Emprestimo>();

        public Usuario GetUsuarioPeloCpf(long cpf)
        {
            return Usuarios.Values.FirstOrDefault(u => u.CPF == cpf);       
        }

        public IEnumerable<Livro> GetLivroTitulo (string titulo)
        {
            var livros = Livros
                 .Where(l => l.Value.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase))
                 .Select(l => l.Value);
            return livros;
		}
        public Livro GetLivro(Guid id)
        {
            return Livros[id];
        }

        public void AdicionarLivro(Livro livro)
        {
			Livros.Add(livro.Id,livro);

		}
        public void CadatrarUsuario(Usuario usuario)
        {
            if (GetUsuarioPeloCpf(usuario.CPF) is not null)
            {
                throw new InvalidOperationException($"O CPF {usuario.CPF} ja Esta Cadastrado");
            }
			Usuarios.Add(usuario.IdUsuario, usuario);
		}
        public void CriarEmprestimo(Usuario usuario, Livro livro)
        {
            if (usuario is null)
            {
                throw new InvalidOperationException($"Usuario Não Cadastrado, Realize o cadastro e tente Novamente");
            }
            if (livro is null)
            {
                throw new InvalidOperationException("Livro não existe na biblioteca");
            }
            else
            {
                var novoEmprestimo = new Emprestimo(livro, usuario);
                livro.Emprestar();
                usuario.AdicionarEmprestimoAoUsuario(novoEmprestimo);
                emprestimosAtivos.Add(novoEmprestimo);
            }
		}

        public void DevolverLivro(Usuario usuario,Emprestimo emprestimo, Livro livro)
        {
            usuario.DevolverLivro(emprestimo);
            livro.Devolver();
            emprestimo.FinalizarEmprestimo();
            HistoriDeEmprestimos.Push(emprestimo);  
        }

        public void ReservarLivro(Usuario usuario, Livro livro)
        {
			if (usuario is null)
			{
				throw new InvalidOperationException($"Usuario Não Cadastrado, Realize o cadastro e tente Novamente");
			}
			if (livro is null)
			{
				throw new InvalidOperationException("Livro não existe na biblioteca");
			}
			livro.Reservar();
		}
        public IEnumerable<Emprestimo> MostrarHistorico ()
        {
            foreach (var emprestimo in HistoriDeEmprestimos)
            {
                yield return emprestimo;
            }
        }
    }
}
