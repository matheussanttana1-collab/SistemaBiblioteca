using LibrarySystem.Domain.Exceptions;


namespace LibrarySystem.Domain.Modelos;

public class Usuario
{
	public Usuario(string name, long cpf, TipoUsuario tipoUsuario)
	{
		if (string.IsNullOrWhiteSpace(name))
			throw new ArgumentException("Nome não pode estar vazio.", nameof(name));
		if (cpf <= 0)
			throw new ArgumentException("CPF inválido.", nameof(cpf));

		Id = Guid.NewGuid();
		Name = name;
		CPF = cpf;
		TipoDoUsuario = tipoUsuario;
		EmprestimosAtivos = new List<Emprestimo>();
		AtividadeUsuario = StatusAtividade.Ativo;
	}

	public Guid Id { get; }
	public string Name { get; }
	public long CPF { get; }
	public TipoUsuario TipoDoUsuario { get; }
	public StatusAtividade AtividadeUsuario { get; private set; }
	private List<Emprestimo> EmprestimosAtivos = new List<Emprestimo>();
	private List<Livro> Reservas = new List<Livro>();

	public IReadOnlyCollection<Emprestimo> ObterEmprestimosAtivos() => EmprestimosAtivos.AsReadOnly();
	public IReadOnlyCollection<Livro> ObterReservasAtivos() => Reservas.AsReadOnly();

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
	/// Adiciona um empréstimo ao usuário.
	/// Altera apenas o estado, sem validação. 
	/// Necessario para adicionar dinamicamente apos busca no banco de dados
	/// </summary>
	public void AdicionarEmprestimoAoUsuario(Emprestimo emprestimo)
	{
		EmprestimosAtivos.Add(emprestimo);
	}

	/// <summary>
	/// Reserva um livro para o usuário.
	/// Altera apenas o estado, sem validação.
	/// Necessario para adicionar dinamicamente apos busca no banco de dados
	/// </summary>
	public void AdicionarReservaAoUsuario(Livro livro)
	{
		Reservas.Add(livro);
	}

	/// <summary>
	/// Valida se usuário pode realizar empréstimos, lançando exceção se não puder
	/// Usado apenas em Biblioteca Service por isso é Internal
	/// </summary>
	internal void ValidarEmprestimo()
	{
		if (AtividadeUsuario == StatusAtividade.Inativo)
			throw new DomainException($"Usuário '{Name}' está inativo e não pode realizar empréstimos.");

		if (EmprestimosAtivos.Count >= LimiteDeEmprestimos)
			throw new DomainException($"Limite de empréstimos ({LimiteDeEmprestimos}) excedido para o usuário '{Name}'.");
	}

	/// <summary>
	/// Valida se usuário pode realizar reservas, lançando exceção se não puder
	/// Usado apenas em Biblioteca Service por isso é Internal
	/// </summary>
	internal void ValidarReserva()
	{
		if (AtividadeUsuario == StatusAtividade.Inativo)
			throw new DomainException($"Usuário '{Name}' está inativo e não pode realizar reservas.");

		if (Reservas.Count >= LimiteDeReservas)
			throw new DomainException($"Limite de reservas ({LimiteDeReservas}) excedido para o usuário '{Name}'.");
	}
}
