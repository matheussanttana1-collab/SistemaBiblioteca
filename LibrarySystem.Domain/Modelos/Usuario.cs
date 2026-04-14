using LibrarySystem.Domain.Exceptions;
using LibrarySystem.Domain.Modelos;

namespace LibrarySystem.Domain.Modelos;

public class Usuario
{
	public Usuario(string name, long cpf, TipoUsuario tipoUsuario)
	{
		if (string.IsNullOrWhiteSpace(name))
			throw new ArgumentException("Nome não pode estar vazio.", nameof(name));
		if (cpf <= 0)
			throw new ArgumentException("CPF inválido.", nameof(cpf));

		IdUsuario = Guid.NewGuid();
		Name = name;
		CPF = cpf;
		TipoDoUsuario = tipoUsuario;
		Emprestimos = new List<Emprestimo>();
		AtividadeUsuario = StatusAtividade.Ativo;
	}

	public Guid IdUsuario { get; }
	public string Name { get; }
	public long CPF { get; }
	public TipoUsuario TipoDoUsuario { get; }
	public StatusAtividade AtividadeUsuario { get; private set; }
	private List<Emprestimo> Emprestimos = new List<Emprestimo>();
	private List<Livro> Reservas = new List<Livro>();

	public IReadOnlyCollection<Emprestimo> ObterEmprestimos() => Emprestimos.AsReadOnly();
	public IReadOnlyCollection<Livro> ObterReservas() => Reservas.AsReadOnly();

	private int LimiteDeEmprestimos
	{
		get
		{
			return TipoDoUsuario switch
			{
				TipoUsuario.Aluno => 2,
				TipoUsuario.Professor => 5,
				TipoUsuario.Funcionario => 3,
				_ => 0,
			};
		}
	}

	private int LimiteDeReservas
	{
		get
		{
			return TipoDoUsuario switch
			{
				TipoUsuario.Aluno => 2,
				TipoUsuario.Professor => 3,
				TipoUsuario.Funcionario => 2,
				_ => 0,
			};
		}
	}

	/// <summary>
	/// Desativa o usuário, impedindo novas operações
	/// </summary>
	public void DesativarUsuario()
	{
		if (AtividadeUsuario == StatusAtividade.Inativo)
			throw new DomainException("Usuário já está inativo.");

		AtividadeUsuario = StatusAtividade.Inativo;
	}

	/// <summary>
	/// Adiciona um empréstimo ao usuário após validar regras de negócio
	/// </summary>
	public void AdicionarEmprestimoAoUsuario(Emprestimo emprestimo)
	{
		if (AtividadeUsuario == StatusAtividade.Inativo)
			throw new DomainException($"Usuário '{Name}' está inativo e não pode realizar empréstimos.");

		if (Emprestimos.Count >= LimiteDeEmprestimos)
			throw new DomainException($"Limite de empréstimos ({LimiteDeEmprestimos}) excedido para o usuário '{Name}'.");

		Emprestimos.Add(emprestimo);
	}

	/// <summary>
	/// Remove um empréstimo do usuário após devolução
	/// </summary>
	public void DevolverLivro(Emprestimo emprestimo)
	{
		if (!Emprestimos.Contains(emprestimo))
			throw new DomainException("Empréstimo não existe para este usuário.");

		Emprestimos.Remove(emprestimo);
	}

	/// <summary>
	/// Reserva um livro para o usuário após validar regras de negócio
	/// </summary>
	public void ReservarLivro(Livro livro)
	{
		if (AtividadeUsuario == StatusAtividade.Inativo)
			throw new DomainException($"Usuário '{Name}' está inativo e não pode realizar reservas.");

		if (Reservas.Count >= LimiteDeReservas)
			throw new DomainException($"Limite de reservas ({LimiteDeReservas}) excedido para o usuário '{Name}'.");

		Reservas.Add(livro);
	}

	/// <summary>
	/// Remove uma reserva do usuário
	/// </summary>
	public void RemoverReserva(Livro livro)
	{
		Reservas.Remove(livro);
	}

	/// <summary>
	/// Verifica se usuário pode realizar empréstimos
	/// </summary>
	public bool PodeEmprestar()
		=> AtividadeUsuario == StatusAtividade.Ativo && Emprestimos.Count < LimiteDeEmprestimos;

	/// <summary>
	/// Verifica se usuário pode realizar reservas
	/// </summary>
	public bool PodeReservar()
		=> AtividadeUsuario == StatusAtividade.Ativo && Reservas.Count < LimiteDeReservas;
}
