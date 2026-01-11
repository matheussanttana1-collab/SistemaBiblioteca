using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;


namespace SistemaBiblioteca.Modelos
{
    internal class Usuario
    {
        public Usuario(string name, long cpf, TipoUsuario tipoUsuario)
        {
            IdUsuario = Guid.NewGuid();
            Name = name;
            CPF = cpf;
            TipoDoUsuario = tipoUsuario;
            Emprestimos = new List<Emprestimo>();
            AtividadeUsuario = StatusAtividade.Ativo;
        }

        public Guid IdUsuario { get; }
        public string Name { get; }
        public long CPF { get; }
        public TipoUsuario TipoDoUsuario { get; }
        public StatusAtividade AtividadeUsuario { get; private set; }
		private Random rdn = new Random();
		private List<Emprestimo> Emprestimos = new List<Emprestimo>();
        public IReadOnlyCollection<Emprestimo> _Emprestimos => Emprestimos;

		public int LimiteDeEmprestimos
        {
            get
            {
                return TipoDoUsuario switch
                {
                    TipoUsuario.Aluno => 2,
                    TipoUsuario.Professor => 5,
                    TipoUsuario.Funcionario => 3,
                    _ => 0,
                };
            }
		}
        public void DesativarUsuario()
        {
            this.AtividadeUsuario = StatusAtividade.Inativo;
        }

        public void AdicionarEmprestimoAoUsuario(Emprestimo emprestimo)
        {	
            if (AtividadeUsuario != StatusAtividade.Ativo)
			{
				throw new InvalidOperationException("O usuário não está ativo para realizar empréstimos.");
			}
			if (Emprestimos.Count >= LimiteDeEmprestimos)
			{
                throw new InvalidOperationException("O usuário atingiu o limite máximo de empréstimos.");
			}	
			Emprestimos.Add(emprestimo);
		}

        public void DevolverLivro(Emprestimo emprestimo)
        {
            if(!Emprestimos.Contains(emprestimo))
            {
                throw new InvalidOperationException("Emprestimo Não Existe");
            }
            Emprestimos.Remove(emprestimo);
        }
	}
}
