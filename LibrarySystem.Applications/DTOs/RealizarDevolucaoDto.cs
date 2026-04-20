namespace LibrarySystem.Applications.DTOs;

public record RealizarDevolucaoDto(Guid EmprestimoId, Guid LivroId, Guid UsuarioId);
