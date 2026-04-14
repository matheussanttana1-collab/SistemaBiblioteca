using Biblioteca.Api.Exceptions;

namespace Biblioteca.Api.Modelos;

public class Livro
{
	public Livro(string titulo, string autor, string isbn, int anoPublicação)
	{
		if (string.IsNullOrWhiteSpace(titulo))
			throw new ArgumentException("Título não pode estar vazio.", nameof(titulo));
		if (string.IsNullOrWhiteSpace(autor))
			throw new ArgumentException("Autor não pode estar vazio.", nameof(autor));
		if (string.IsNullOrWhiteSpace(isbn))
			throw new ArgumentException("ISBN não pode estar vazio.", nameof(isbn));
		if (anoPublicação < 0)
			throw new ArgumentException("Ano de publicação não pode ser negativo.", nameof(anoPublicação));

		Titulo = titulo;
		Id = Guid.NewGuid();
		Autor = autor;
		ISBN = isbn;
		AnoPublicação = anoPublicação;
		StatusDoLivro = StatusDoLivro.Disponivel;
	}

	public string Titulo { get; }
	public Guid Id { get; }
	public string Autor { get; }
	public string ISBN { get; }
	public int AnoPublicação { get; }
	public StatusDoLivro StatusDoLivro { get; private set; }
	public Usuario? UsuarioQueReservou { get; private set; }
	public Usuario? UsuarioQueEmprestou { get; private set; }

	/// <summary>
	/// Emprestar livro para um usuário.
	/// Valida se: livro está disponível ou reservado para este usuário, e se não está inativo
	/// </summary>
	public void Emprestar(Usuario usuario)
	{
		if (StatusDoLivro == StatusDoLivro.Inativo)
			throw new LivroInativoException(Titulo);

		if (StatusDoLivro == StatusDoLivro.Emprestado)
			throw new LivroNaoDisponibilizadoException(Titulo, StatusDoLivro.ToString());

		if (StatusDoLivro == StatusDoLivro.Reservado && UsuarioQueReservou != usuario)
			throw new LivroReservadoParaOutroUsuarioException(Titulo);

		StatusDoLivro = StatusDoLivro.Emprestado;
		UsuarioQueEmprestou = usuario;
		UsuarioQueReservou = null;
	}

	/// <summary>
	/// Reservar livro para um usuário.
	/// Valida se: livro não está inativo e ainda não possui reserva
	/// </summary>
	public void Reservar(Usuario usuario)
	{
		if (StatusDoLivro == StatusDoLivro.Inativo)
			throw new LivroInativoException(Titulo);

		if (StatusDoLivro == StatusDoLivro.Reservado)
			throw new LivroJaReservadoException(Titulo);

		UsuarioQueReservou = usuario;
		StatusDoLivro = StatusDoLivro.Reservado;
	}

	/// <summary>
	/// Marca livro como disponível novamente após devolução
	/// </summary>
	internal void MarcarComoDisponivel()
	{
		if (StatusDoLivro == StatusDoLivro.Disponivel)
			throw new InvalidOperationException("Livro já está disponível.");

		StatusDoLivro = StatusDoLivro.Disponivel;
		UsuarioQueEmprestou = null;
	}

	/// <summary>
	/// Inativa um livro para que não possa mais ser emprestado ou reservado.
	/// Só pode inativar se estiver disponível (sem empréstimos ativos)
	/// </summary>
	public void Inativar()
	{
		if (StatusDoLivro != StatusDoLivro.Disponivel)
			throw new NaoPodeInativarLivroEmprestamoException(Titulo, StatusDoLivro.ToString());

		StatusDoLivro = StatusDoLivro.Inativo;
	}
}
