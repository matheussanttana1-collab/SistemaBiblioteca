namespace LibrarySystem.DomainExcpetion.Exceptions;

/// <summary>
/// Exceção para violações de regras de negócio no domínio
/// </summary>
public class DomainException : Exception
{
	public DomainException(string? message) : base(message) { }
}
