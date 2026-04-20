using LibrarySystem.Applications.Ports.Out;
using LibrarySystem.Domain.Exceptions;
using LibrarySystem.Domain.Modelos;

namespace LibrarySystem.Applications.UseCases.EmprestimoCases;

public class BuscarEmprestimoPorIdUseCase
{
	private readonly IEmprestimoRepository emprestimoRepository;

	public BuscarEmprestimoPorIdUseCase(IEmprestimoRepository emprestimoRepository)
	{
		this.emprestimoRepository = emprestimoRepository;
	}

	public async Task<Emprestimo> Execute(Guid id)
	{
		var emprestimo = await emprestimoRepository.BuscarEmprestimoPeloIdAsync(id);

		if (emprestimo == null)
			throw new DomainException($"Empréstimo com ID '{id}' não encontrado.");

		return emprestimo;
	}
}
