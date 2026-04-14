namespace Biblioteca.Api.Exceptions;

/// <summary>
/// Exceção base para violações de regras de negócio no domínio
/// </summary>
public class DomainException : Exception
{
	public DomainException(string? message) : base(message) { }
}

/// <summary>
/// Livro está inativo e não pode ser emprestado
/// </summary>
public class LivroInativoException : DomainException
{
	public LivroInativoException(string titulo) 
		: base($"O livro '{titulo}' está inativo e não pode ser emprestado ou reservado.") { }
}

/// <summary>
/// Livro não está disponível para empréstimo
/// </summary>
public class LivroNaoDisponibilizadoException : DomainException
{
	public LivroNaoDisponibilizadoException(string titulo, string statusAtual)
		: base($"O livro '{titulo}' não está disponível. Status atual: {statusAtual}.") { }
}

/// <summary>
/// Livro reservado só pode ser emprestado ao reservante
/// </summary>
public class LivroReservadoParaOutroUsuarioException : DomainException
{
	public LivroReservadoParaOutroUsuarioException(string titulo)
		: base($"O livro '{titulo}' está reservado para outro usuário.") { }
}

/// <summary>
/// Livro já possui uma reserva ativa
/// </summary>
public class LivroJaReservadoException : DomainException
{
	public LivroJaReservadoException(string titulo)
		: base($"O livro '{titulo}' já está reservado.") { }
}

/// <summary>
/// Tentativa de inativar um livro que não está disponível
/// </summary>
public class NaoPodeInativarLivroEmprestamoException : DomainException
{
	public NaoPodeInativarLivroEmprestamoException(string titulo, string statusAtual)
		: base($"Não é possível inativar o livro '{titulo}' pois está {statusAtual}.") { }
}

/// <summary>
/// Usuário inativo não pode realizar operações
/// </summary>
public class UsuarioInativoException : DomainException
{
	public UsuarioInativoException(string nome, string operacao)
		: base($"O usuário '{nome}' está inativo e não pode {operacao}.") { }
}

/// <summary>
/// Usuário excedeu o limite de empréstimos
/// </summary>
public class LimiteDeEmprestimosExcedidoException : DomainException
{
	public LimiteDeEmprestimosExcedidoException(string nomeUsuario, int limite)
		: base($"O usuário '{nomeUsuario}' excedeu o limite de {limite} empréstimos simultâneos.") { }
}

/// <summary>
/// Usuário excedeu o limite de reservas
/// </summary>
public class LimiteDeReservasExcedidoException : DomainException
{
	public LimiteDeReservasExcedidoException(string nomeUsuario, int limite)
		: base($"O usuário '{nomeUsuario}' excedeu o limite de {limite} reservas simultâneas.") { }
}

/// <summary>
/// Empréstimo não pode ser finalizado porque não está ativo
/// </summary>
public class EmprestimoJaFinalizadoException : DomainException
{
	public EmprestimoJaFinalizadoException()
		: base("Este empréstimo já foi finalizado e não pode ser reativado.") { }
}

/// <summary>
/// Tentativa de devolver um empréstimo que não existe para o usuário
/// </summary>
public class EmprestimoNaoExisteException : DomainException
{
	public EmprestimoNaoExisteException()
		: base("Este empréstimo não existe para o usuário.") { }
}
