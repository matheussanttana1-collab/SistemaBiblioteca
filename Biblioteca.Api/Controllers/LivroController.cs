using Microsoft.AspNetCore.Mvc;
namespace Biblioteca.Api.Controllers;

[ApiController]
[Route("/livro")]
public class LivroController : ControllerBase
{
	// TODO: Implementar métodos com a BibliotecaService depois

	[HttpPost]
	public IActionResult AdicionarLivro()
	{
		// _biblioteca.AdicionarLivro(livro);
		// Console.WriteLine(livro.Id);
		// return CreatedAtAction(nameof(PegarLivroPeloId), new { id = livro.Id }, livro);
		return Ok();
	}

	[HttpGet]
	public IEnumerable<T> RecuperarLivros<T>()
	{
		// return _biblioteca.Livros.Values.Skip(skip).Take(take);
		return Enumerable.Empty<T>();
	}

	[HttpGet("{id}")]
	public IActionResult PegarLivroPeloId(Guid id)
	{
		// Livro livro = _biblioteca.GetLivro(id);
		// if (livro is null) return BadRequest();
		// return Ok(livro);
		return Ok();
	}
}
