using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Applications.DTOs;

public record CadastrarLivroDto (string Titulo, string Autor, int AnoPublicacao, string Isbn)
{
}
