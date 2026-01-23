using SistemaBiblioteca.Modelos;
using SistemaBiblioteca.Serviços;
using System;
using System.Threading.Channels;
using System.Xml;

public class Program
{
	static void Main(string[] args)
	{
		//var biblioteca = new Biblioteca();

		//biblioteca.AdicionarLivro(new Livro("LIVRO1", "MATHEUS", 1984));
		//biblioteca.AdicionarLivro(new Livro("LIVRO1", "MATHEUS", 1984));
		//biblioteca.AdicionarLivro(new Livro("LIVRO1", "PUDIM", 1984));

		//biblioteca.CadatrarUsuario(new Usuario("Matheus", 44098108801, TipoUsuario.Aluno));


	}

	//------------------------------- Funções -------------------------------------------------
	static void ExibirTitulo()
	{
		Console.Clear();
		Console.WriteLine(@"
██████╗░██╗██████╗░██╗░░░░░██╗░█████╗░████████╗███████╗░█████╗░░█████╗░
██╔══██╗██║██╔══██╗██║░░░░░██║██╔══██╗╚══██╔══╝██╔════╝██╔══██╗██╔══██╗
██████╦╝██║██████╦╝██║░░░░░██║██║░░██║░░░██║░░░█████╗░░██║░░╚═╝███████║
██╔══██╗██║██╔══██╗██║░░░░░██║██║░░██║░░░██║░░░██╔══╝░░██║░░██╗██╔══██║
██████╦╝██║██████╦╝███████╗██║╚█████╔╝░░░██║░░░███████╗╚█████╔╝██║░░██║
╚═════╝░╚═╝╚═════╝░╚══════╝╚═╝░╚════╝░░░░╚═╝░░░╚══════╝░╚════╝░╚═╝░░╚═╝");
		Console.WriteLine("Seja Bem-Vindo");
	}
	static void RealizarEmprestimos(Biblioteca biblioteca)
	{
		ExibirTitulo();
		Console.WriteLine("\nRealizar Emprestimo: ");
		Console.Write("Qual Livro Deseja Emprestar: ");
		string? livroEntrada = Console.ReadLine();
		if (string.IsNullOrWhiteSpace(livroEntrada))
		{
			Console.WriteLine("Entrada Invalida Tente Novamente");
			Thread.Sleep(1000);
			return;
		}
		var LivrosEncontrados = biblioteca.GetLivroTitulo(livroEntrada).ToList();
		if (LivrosEncontrados.Count() == 0)
		{
			Thread.Sleep(1000);
			Console.WriteLine($"Nenhum Livro Com este Titulo Foi Encontrado");
			return;
		}
		var LivroSelecionado = SelecionarLivroComEsteTitulo(LivrosEncontrados);
		ExibirTitulo();
		var livro = biblioteca.GetLivro(LivroSelecionado);
        Console.WriteLine($"Livro: {livro.Titulo} De {livro.Autor}");
		Console.Write("\nCPF: ");
		string? usuarioEntrada = Console.ReadLine();
		if (string.IsNullOrWhiteSpace(usuarioEntrada))
		{
			Console.WriteLine("Entrada Invalida Tente Novamente");
			Thread.Sleep(1000);
			return;
		}
		if (!long.TryParse(usuarioEntrada, out long cpf))
		{
			Thread.Sleep(1000);
			Console.WriteLine("CPF Deve ser um numero");
			return;
		}
		var usuario = biblioteca.GetUsuarioPeloCpf(cpf);
		try
		{
			biblioteca.CriarEmprestimo(usuario, livro);
			Console.WriteLine($"Emprestimo Realizado {usuario.Name} - {usuario.TipoDoUsuario} - {livro.Titulo}");
			Thread.Sleep(1000);
		}
		catch (InvalidOperationException ex)
		{
			Console.WriteLine(ex.Message);
			Thread.Sleep(1000);
		}
	}
	static Guid SelecionarLivroComEsteTitulo(List<Livro> livros)
	{
		Console.WriteLine($"Livros Encontrados: ");
		int count = 1;
		foreach (var livro in livros)
		{
			Console.WriteLine($"{count}\t - {livro.Titulo} (Autor: {livro.Autor})  - Status: {livro.StatusDoLivro}");
			count++;
		}
		while (true)
		{
			Console.Write("Selecione: ");
			var selecionado = Console.ReadLine();
			if (int.TryParse(selecionado, out int seleInt) && seleInt < count)
			{
				return livros[seleInt - 1].Id;
			}
			else
			{
				Console.WriteLine("Indice Invalido Tente Novamente");
			}
		}

	}
	static void CadastrarUsuario ()
	{

	}
}


