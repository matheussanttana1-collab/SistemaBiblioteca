using SistemaBiblioteca.Modelos;
using SistemaBiblioteca.Serviços;

Biblioteca biblioteca = new Biblioteca();

Usuario usario1 = new Usuario("Matheus", 44098108801, TipoUsuario.Aluno);
Livro livro1 = new Livro("Game Of Thrones","George Martin", 1980);
Livro livro2 = new Livro("Game Of Thrones", "George Martin", 1980);

try
{
	biblioteca.AdicionarUsuario(usario1);
	biblioteca.AdicionarLivro(livro1);
	biblioteca.AdicionarLivro(livro2);
	biblioteca.CriarEmprestimo(usario1, livro1);
	biblioteca.CriarEmprestimo(usario1 , livro2);
}
catch(InvalidOperationException ex) 
{
	Console.WriteLine(ex.Message);
}


foreach (var emprestimo in biblioteca.emprestimos)
{
	Console.WriteLine(emprestimo.IdEmprestimo);
    Console.WriteLine($"{emprestimo.UsuarioQueEmprestou.Name}");
    Console.WriteLine($"{emprestimo.LivroEmprestado.Titulo}");
}


