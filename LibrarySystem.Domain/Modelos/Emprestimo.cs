using Biblioteca.Api.Exceptions;

namespace Biblioteca.Api.Modelos;

public class Emprestimo
{
	private const int PrazoParaDevolucaoEmDias = 15;

	public Emprestimo(Livro livroEmprestado, Usuario usuarioQueEmprestou)
	{
		if (livroEmprestado == null)
			throw new ArgumentNullException(nameof(livroEmprestado));
		if (usuarioQueEmprestou == null)
			throw new ArgumentNullException(nameof(usuarioQueEmprestou));

		// Validar invariantes de negócio
		if (livroEmprestado.StatusDoLivro == StatusDoLivro.Inativo)
			throw new LivroInativoException(livroEmprestado.Titulo);

		if (usuarioQueEmprestou.AtividadeUsuario == StatusAtividade.Inativo)
			throw new UsuarioInativoException(usuarioQueEmprestou.Name, "pegar livros em empréstimo");

		IdEmprestimo = Guid.NewGuid();
		LivroEmprestado = livroEmprestado;
		UsuarioQueEmprestou = usuarioQueEmprestou;
		DataEmprestimo = DateTime.Today;
		DataPrevistaDevolucao = DataEmprestimo.AddDays(PrazoParaDevolucaoEmDias);
		DataDevolucao = null;
		StatusEmprestimo = StatusAtividade.Ativo;
	}

	public Guid IdEmprestimo { get; }
	public Guid LivroId => LivroEmprestado.Id;
	public Livro LivroEmprestado { get; }
	public Guid UsuarioId => UsuarioQueEmprestou.IdUsuario;
	public Usuario UsuarioQueEmprestou { get; }
	public DateTime DataEmprestimo { get; }
	public DateTime DataPrevistaDevolucao { get; }
	public DateTime? DataDevolucao { get; private set; }
	public StatusAtividade StatusEmprestimo { get; private set; }

	/// <summary>
	/// Finaliza o empréstimo registrando a data de devolução.
	/// Só pode ser feito uma única vez.
	/// </summary>
	public void FinalizarEmprestimo()
	{
		if (StatusEmprestimo == StatusAtividade.Inativo)
			throw new EmprestimoJaFinalizadoException();

		if (DataDevolucao.HasValue)
			throw new InvalidOperationException("Empréstimo já foi finalizado com data de devolução registrada.");

		DataDevolucao = DateTime.Today;
		StatusEmprestimo = StatusAtividade.Inativo;
	}

	/// <summary>
	/// Verifica se o empréstimo está ativo (não finalizado)
	/// </summary>
	public bool EstaAtivo() => StatusEmprestimo == StatusAtividade.Ativo;
}
