using LibrarySystem.Applications.Ports.Out;
using LibrarySystem.Domain.Modelos;

namespace LibrarySystem.Applications.UseCases.EmprestimoCases;

public class BuscarHistoricoEmprestimosUseCase
{
	private readonly IEmprestimoRepository emprestimoRepository;

	public BuscarHistoricoEmprestimosUseCase(IEmprestimoRepository emprestimoRepository)
	{
		this.emprestimoRepository = emprestimoRepository;
	}

	public async Task<IEnumerable<Emprestimo>> Execute
	(DateTime? dataInicio = null,DateTime? dataFim = null,StatusAtividade? status = null)
	{
		var emprestimos = await emprestimoRepository.BuscarHistoricoEmprestimosAsync
		(dataInicio, dataFim, status);

		return emprestimos;
	}
}
