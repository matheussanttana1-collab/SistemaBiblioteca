using SistemaBiblioteca.Modelos;
using SistemaBiblioteca.Serviços;
using System;
using System.Threading.Channels;
using System.Xml;

public class Program
{
	static void Main (string[] args)
	{
		var biblioteca = new Biblioteca();		
	}

//------------------------------- Funções -------------------------------------------------
	static void ExibirTitulo ()
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
		Console.Write("\nCPF: ");
		string? usuarioEntrada = Console.ReadLine();
		Console.Write("Livro: ");
		string? livroEntrada = Console.ReadLine();
		if (string.IsNullOrWhiteSpace(usuarioEntrada) || string.IsNullOrWhiteSpace(livroEntrada))
		{
			Console.WriteLine("Entrada Invalida Tente Novamente");
			Thread.Sleep(1000);
			RealizarEmprestimos(biblioteca);
			return;
		}
		if (!long.TryParse(usuarioEntrada, out long cpf))
		{
			Thread.Sleep(1000);
			Console.WriteLine("CPF Deve ser um numero");
			return;
		}
		var usuario = biblioteca.GetUsuarioPeloCpf(cpf);
		var livro = biblioteca.GetLivroTitulo(livroEntrada);
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
}


