using LibrarySystem.DomainExcpetion.Exceptions;

namespace LibrarySystem.DomainExcpetion.Modelos;

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
	public void FinalizarEmprestimo()
	{ 
		DataDevolucao = DateTime.Today;
		StatusEmprestimo = StatusAtividade.Inativo;
	}

	/// <summary>
	/// Verifica se o empréstimo está ativo (não finalizado)
	/// </summary>
	public bool EstaAtivo() => StatusEmprestimo == StatusAtividade.Ativo;
}
