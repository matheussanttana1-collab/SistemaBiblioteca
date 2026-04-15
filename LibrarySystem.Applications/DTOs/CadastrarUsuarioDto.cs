using LibrarySystem.DomainExcpetion.Modelos;

namespace LibrarySystem.Applications.DTOs;

public record CadastrarUsuarioDto(string Nome, long CPF, TipoUsuario TipoUsuario);
