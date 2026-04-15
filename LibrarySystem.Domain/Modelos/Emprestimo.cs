using LibrarySystem.Domain.Exceptions;

namespace LibrarySystem.Domain.Modelos;

public class Emprestimo
{
	private const int PrazoParaDevolucaoEmDias = 15;

	public Emprestimo(Guid UsuarioId, Guid LivroId)
	{
		Id = Guid.NewGuid();
		DataEmprestimo = DateTime.Today;
		DataPrevistaDevolucao = DataEmprestimo.AddDays(PrazoParaDevolucaoEmDias);
		StatusEmprestimo = StatusAtividade.Ativo;
	}

	public Guid Id { get; }
	public Guid LivroId {  get; }
	public Guid UsuarioId {  get; }
	public DateTime DataEmprestimo { get; }
	public DateTime DataPrevistaDevolucao { get; }
	public DateTime? DataDevolucao { get; private set; }
	public StatusAtividade StatusEmprestimo { get; private set; }

	/// <summary>
	/// Finaliza o empréstimo registrando a data de devolução.
	/// Só pode ser feito uma única vez.
	/// </summary>
	internal void FinalizarEmprestimo()
	{ 
		DataDevolucao = DateTime.Today;
		StatusEmprestimo = StatusAtividade.Inativo;
	}

	/// <summary>
	/// Verifica se o empréstimo está ativo (não finalizado)
	/// </summary>
	internal void ValidaDevolucao(Guid usuarioId) 
	{
		if (StatusEmprestimo == StatusAtividade.Ativo)
			throw new DomainException("Emprestimo ja foi Finalizado");
		if (usuarioId != UsuarioId)
			throw new DomainException("Usuario que emprestou deve se o mesmo que realizou emprestimo");
	}	
}
