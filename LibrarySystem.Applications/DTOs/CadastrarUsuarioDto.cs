using LibrarySystem.Domain.Modelos;

namespace LibrarySystem.Applications.DTOs;

public record CadastrarUsuarioDto(string Nome, long CPF, TipoUsuario TipoUsuario);
