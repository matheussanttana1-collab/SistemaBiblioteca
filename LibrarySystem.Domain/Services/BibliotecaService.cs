using LibrarySystem.Domain.Modelos;
using LibrarySystem.Domain.Exceptions;

namespace LibrarySystem.Domain.Services;

/// <summary>
/// Serviço de domínio que centraliza regras de negócio que envolvem múltiplas entidades.
/// </summary>
public class BibliotecaService : IBibliotecaService
{
	/// <summary>
	/// Realiza o empréstimo de um livro para um usuário, validando todas as regras de negócio.
	/// 
	/// Regras validadas:
	/// - Livro deve estar Disponível ou Reservado para este usuário
	/// - Usuário deve estar Ativo
	/// - Limite de empréstimos do usuário respeitado
	/// - Livro não pode estar Inativo
	/// </summary>
	public Emprestimo RealizarEmprestimo(Livro livro, Usuario usuario)
	{
		livro.ValidarEmprestimo(usuario.Id);
		usuario.ValidarEmprestimo();

		var emprestimo = new Emprestimo(livro.Id, usuario.Id);
		livro.Emprestar(usuario.Id);

		return emprestimo;
	}

	/// <summary>
	/// Realiza a reserva de um livro para um usuário, validando todas as regras de negócio.
	/// 
	/// Regras validadas:
	/// - Livro pode ser reservado (não está inativo ou já reservado)
	/// - Usuário deve estar Ativo
	/// - Limite de reservas do usuário respeitado
	/// - Apenas uma reserva por livro
	/// </summary>
	public void RealizarReserva(Livro livro, Usuario usuario)
	{
		livro.ValidarReserva();
		usuario.ValidarReserva();

		livro.Reservar(usuario.Id);
	}

	/// <summary>
	/// Realiza a devolução de um livro, finalizando o empréstimo.
	/// 
	/// Regras validadas:
	/// - Empréstimo deve estar ativo
	/// - Livro foi emprestado
	/// - Status do livro volta para Disponível
	/// - Empréstimo é finalizado
	/// - Histórico é preservado
	/// </summary>
	public void RealizarDevolucao(Emprestimo emprestimo, Livro livro, Usuario usuario)
	{
		emprestimo.ValidaDevolucao(usuario.Id);

		livro.FinalizarEmprestimo();
		emprestimo.FinalizarEmprestimo();
	}
}
