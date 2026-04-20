namespace LibrarySystem.Applications.DTOs;

public record BuscarLivrosFilterDto(
	string? Autor = null,
	string? Genero = null,
	string? Titulo = null
);
