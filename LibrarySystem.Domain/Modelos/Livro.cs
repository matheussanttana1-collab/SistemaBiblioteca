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



	/// <summary>
	/// Emprestar livro para um usuário.
	/// Altera apenas o estado, sem validação.
	/// </summary>
	internal void Emprestar(Guid usuarioId)
	{
		StatusDoLivro = StatusDoLivro.Emprestado;
		UsuarioQueReservouId = null;
	}

	internal void FinalizarEmprestimo()
	{
		if (StatusDoLivro == StatusDoLivro.Disponivel)
			throw new DomainException("Livro ja Marcado como Disponivel");
		if (UsuarioQueReservouId != null)
			StatusDoLivro = StatusDoLivro.Reservado;
		else
			StatusDoLivro = StatusDoLivro.Disponivel;
	}
	public void InativarLivro()
	{
		if (StatusDoLivro == StatusDoLivro.Disponivel)
			throw new DomainException("Livro Ja Esta Inativo");
		if (StatusDoLivro == StatusDoLivro.Emprestado)
			throw new DomainException("Não é possivel Inativar um livro Emprestado, Finalize o emprestimo e tente " +
			"novamente");
		StatusDoLivro = StatusDoLivro.Inativo;
	}

	public void ReativarLivro() 
	{
		if(StatusDoLivro == StatusDoLivro.Disponivel)
			throw new DomainException("Livro ja Marcado como Disponivel");
		StatusDoLivro = StatusDoLivro.Disponivel;
	}

	/// <summary>
	/// Reservar livro para um usuário.
	/// Altera apenas o estado, sem validação.
	/// </summary>
	internal void Reservar(Guid usuarioId)
	{
		UsuarioQueReservouId = usuarioId;
		if (StatusDoLivro !=  StatusDoLivro.Emprestado)
			StatusDoLivro = StatusDoLivro.Reservado;
	}

	public void RetirarReserva() 
	{
		if (UsuarioQueReservouId == null)
			throw new DomainException("Não existe Reserva para este livro");

		UsuarioQueReservouId = null;
		if (StatusDoLivro != StatusDoLivro.Emprestado)
			StatusDoLivro = StatusDoLivro.Disponivel;
	}

	/// <summary>
	/// Valida se livro pode ser emprestado, lançando exceção se não puder
	/// </summary>
	internal void ValidarEmprestimo(Guid usuarioId)
	{
		if (StatusDoLivro == StatusDoLivro.Inativo)
			throw new DomainException($"O livro '{Titulo}' está inativo.");

		if (StatusDoLivro == StatusDoLivro.Emprestado)
			throw new DomainException($"O livro '{Titulo}' não está disponível. Status: {StatusDoLivro}");

		if (StatusDoLivro == StatusDoLivro.Reservado && UsuarioQueReservouId != usuarioId)
			throw new DomainException($"O livro '{Titulo}' está reservado para outro usuário.");
	}

	/// <summary>
	/// Valida se livro pode ser reservado, lançando exceção se não puder
	/// </summary>
	internal void ValidarReserva()
	{
		if (StatusDoLivro == StatusDoLivro.Inativo)
			throw new DomainException($"O livro '{Titulo}' está inativo.");

		if (StatusDoLivro == StatusDoLivro.Reservado)
			throw new DomainException($"O livro '{Titulo}' já possui uma reserva.");
	}
}
