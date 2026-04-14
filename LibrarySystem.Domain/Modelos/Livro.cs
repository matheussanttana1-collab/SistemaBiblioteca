using LibrarySystem.Domain.Exceptions;

namespace LibrarySystem.Domain.Modelos;

public class Livro
{
	public Livro(string titulo, string autor, string isbn, int anoPublicação)
	{
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
	public Guid? UsuarioQueReservouId { get; private set; }
	public Guid? UsuarioQueEmprestouId { get; private set; }

	/// <summary>
	/// Emprestar livro para um usuário.
	/// Valida se: livro está disponível ou reservado para este usuário, e se não está inativo
	/// </summary>
	public void Emprestar(Guid usuarioId)
	{
		if (StatusDoLivro == StatusDoLivro.Inativo)
			throw new DomainException($"O livro '{Titulo}' está inativo.");

		if (StatusDoLivro == StatusDoLivro.Emprestado)
			throw new DomainException($"O livro '{Titulo}' não está disponível. Status: {StatusDoLivro}");

		if (StatusDoLivro == StatusDoLivro.Reservado && UsuarioQueReservouId != usuarioId)
			throw new DomainException($"O livro '{Titulo}' está reservado para outro usuário.");

		StatusDoLivro = StatusDoLivro.Emprestado;
		UsuarioQueEmprestouId = usuarioId;
		UsuarioQueReservouId = null;
	}

	/// <summary>
	/// Reservar livro para um usuário.
	/// Valida se: livro não está inativo e ainda não possui reserva
	/// </summary>
	public void Reservar(Guid usuarioId)
	{
		if (StatusDoLivro == StatusDoLivro.Inativo)
			throw new DomainException($"O livro '{Titulo}' está inativo.");

		if (StatusDoLivro == StatusDoLivro.Reservado)
			throw new DomainException($"O livro '{Titulo}' já possui uma reserva.");

		UsuarioQueReservouId = usuarioId;
		StatusDoLivro = StatusDoLivro.Reservado;
	}

	/// <summary>
	/// Marca livro como disponível novamente após devolução
	/// </summary>
	internal void MarcarComoDisponivel()
	{
		if (StatusDoLivro == StatusDoLivro.Disponivel)
			throw new DomainException("Livro já está disponível.");

		StatusDoLivro = StatusDoLivro.Disponivel;
		UsuarioQueEmprestouId = null;
	}

	/// <summary>
	/// Inativa um livro para que não possa mais ser emprestado ou reservado.
	/// Só pode inativar se estiver disponível (sem empréstimos ativos)
	/// </summary>
	public void Inativar()
	{
		if (StatusDoLivro != StatusDoLivro.Disponivel)
			throw new DomainException("Livro não pode ser inativado, verifique se ele ja esta emprestado, reservado" +
			"ou se ja esta inativo");

		StatusDoLivro = StatusDoLivro.Inativo;
	}
}
