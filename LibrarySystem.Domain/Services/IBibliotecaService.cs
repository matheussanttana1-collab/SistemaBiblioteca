using LibrarySystem.Domain.Modelos;

namespace LibrarySystem.Domain.Services;

/// <summary>
/// Interface para o serviço de domínio que centraliza regras de negócio multi-entidade.
/// </summary>
public interface IBibliotecaService
{
	Emprestimo RealizarEmprestimo(Livro livro, Usuario usuario);
	void RealizarReserva(Livro livro, Usuario usuario);
	void RealizarDevolucao(Emprestimo emprestimo, Livro livro, Usuario usuario);
}
