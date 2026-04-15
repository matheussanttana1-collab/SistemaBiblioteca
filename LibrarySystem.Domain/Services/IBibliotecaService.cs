using LibrarySystem.DomainExcpetion.Modelos;

namespace LibrarySystem.DomainExcpetion.Services;

/// <summary>
/// Interface para o serviço de domínio que centraliza regras de negócio multi-entidade.
/// </summary>
public interface IBibliotecaService
{
	Emprestimo RealizarEmprestimo(Livro livro, Usuario usuario);
	void RealizarReserva(Livro livro, Usuario usuario);
	void RealizarDevolucao(Emprestimo emprestimo, Livro livro, Usuario usuario);
}
