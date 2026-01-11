using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaBiblioteca.Modelos
{
    internal class Emprestimo
    {
		public Emprestimo(Livro livroEmprestado, Usuario usuarioQueEmprestou)
		{
			IdEmprestimo = Guid.NewGuid();
			LivroEmprestado = livroEmprestado;
			UsuarioQueEmprestou = usuarioQueEmprestou;
			DataEmprestimo = DateTime.Today;
			DataDevolucao = DataEmprestimo.AddDays(PrazoParaDevolucao);
			StatusEmprestimo = StatusAtividade.Ativo;
		}
		public Guid IdEmprestimo { get; }
		public Livro LivroEmprestado { get; }
        public Usuario UsuarioQueEmprestou { get; }
        public DateTime DataEmprestimo { get; }
        public DateTime DataDevolucao { get; private set; }
		private int PrazoParaDevolucao = 15;

		public StatusAtividade StatusEmprestimo { get; private set; }

		public void FinalizarEmprestimo()
		{
			StatusEmprestimo = StatusAtividade.Inativo;
		}

	}
}
