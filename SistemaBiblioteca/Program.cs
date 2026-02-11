using SistemaBiblioteca.Modelos;
using SistemaBiblioteca.Serviços;
using System;
using System.Threading.Channels;
using System.Xml;

public class Program
{
	static void Main(string[] args)
	{
		var biblioteca = new Biblioteca();

		biblioteca.AdicionarLivro(new Livro("LIVRO1", "MATHEUS", 1984));
		biblioteca.AdicionarLivro(new Livro("LIVRO1", "MATHEUS", 1984));
		biblioteca.AdicionarLivro(new Livro("LIVRO1", "PUDIM", 1984));
		var usuario1 = new Usuario("Matheus", 44098108801, TipoUsuario.Aluno);
		biblioteca.CadatrarUsuario(usuario1);
		var count = 0;

		ReservarLivro(biblioteca);
		while(count < 4)
		{
			RealizarEmprestimos(biblioteca);
			count++;

		}

	
    }

	//------------------------------- Metodos -------------------------------------------------
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
		TituloMenuAtual("Realizar Emprestimo");
		Console.Write("Qual Livro Deseja Emprestar: ");
		string? livroEntrada = Console.ReadLine();
		if (string.IsNullOrWhiteSpace(livroEntrada))
		{
			MostrarErro("Entrada Invalida Tente Novamente");
			return;
		}
		var LivrosEncontrados = biblioteca.GetLivroTitulo(livroEntrada).ToList();
		if (LivrosEncontrados.Count() == 0)
		{
			MostrarErro($"Nenhum Livro Com este Titulo Foi Encontrado");
			return;
		}
		var LivroSelecionado = SelecionarLivroComEsteTitulo(LivrosEncontrados);
		var livro = biblioteca.GetLivro(LivroSelecionado);
        Console.WriteLine($"Livro: {livro.Titulo} De {livro.Autor}");
		Console.Write("\nCPF: ");
		string? usuarioEntrada = Console.ReadLine();
		if (string.IsNullOrWhiteSpace(usuarioEntrada))
		{
			MostrarErro("Entrada Invalida Tente Novamente");
			return;
		}
		if (!long.TryParse(usuarioEntrada, out long cpf))
		{
			MostrarErro("CPF Deve ser um numero");
			return; 
		}
		Usuario? usuario = biblioteca.GetUsuarioPeloCpf(cpf);
		try
		{
			biblioteca.CriarEmprestimo(usuario, livro);
			Console.WriteLine($"Emprestimo Realizado {usuario.Name} - {usuario.TipoDoUsuario} - {livro.Titulo}");
			Thread.Sleep(2000);
		}
		catch (InvalidOperationException ex)
		{
			Console.WriteLine(ex.Message);
			Thread.Sleep(2000);
		}
	}
	static void CadastrarUsuario (Biblioteca biblioteca)
	{
		ExibirTitulo();
		TituloMenuAtual("Cadastrar novo usuario");

        Console.Write("Nome: ");
		var nomeEntrada = Console.ReadLine();
        Console.Write("CPF: ");
		var cpfEntrada = Console.ReadLine();
		Console.WriteLine("Tipo de Usuario :");
        Console.WriteLine("1.Aluno\n2.Funcionario\n3.Professor");
		Console.Write("Escolha: ");
        var tipoUsuarioEscolha = Console.ReadLine();
		if (string.IsNullOrEmpty(nomeEntrada) || string.IsNullOrEmpty(cpfEntrada) || string.IsNullOrEmpty(tipoUsuarioEscolha))
		{
			MostrarErro("Uma ou mais entradas foram nulas. Tente Novamente !!");
			return;
		}
		if (!long.TryParse(cpfEntrada, out long cpf))
		{
			MostrarErro("CPF Deve ser um numero");
			return;
		}
		if (!int.TryParse(tipoUsuarioEscolha, out int EscolhaInt ))
		{
			MostrarErro("Escolha deve Ser um numero !");
			return;
		}
		TipoUsuario tipoUsuario; 
		switch (EscolhaInt)
		{
			case 1:
				tipoUsuario = TipoUsuario.Aluno;
				break;
			case 2:
				tipoUsuario = TipoUsuario.Funcionario;
				break;
			case 3:
				tipoUsuario = TipoUsuario.Professor;
				break;
			default:
                Console.WriteLine("Escolha Invalida");
				return;
		}

		try
		{
			biblioteca.CadatrarUsuario(new Usuario(nomeEntrada, cpf, tipoUsuario));
			Console.WriteLine($"Usuario Cadastrado Com Sucesso !!");
			Console.WriteLine($"{nomeEntrada} - CPF: {cpf} - {tipoUsuario}");
			Thread.Sleep(2000);
		}
		catch (InvalidOperationException ex)
		{
			Console.WriteLine(ex.Message);
			Thread.Sleep(1000);
			return;
		}
	}
	static void ReservarLivro (Biblioteca biblioteca)
	{
		ExibirTitulo();
		TituloMenuAtual("Reservar Livro");
		Console.Write("Qual Livro Deseja Reservar: ");
		string? livroEntrada = Console.ReadLine();
		if (string.IsNullOrWhiteSpace(livroEntrada))
		{
			MostrarErro("Entrada Invalida Tente Novamente");
			return;
		}
		var LivrosEncontrados = biblioteca.GetLivroTitulo(livroEntrada).ToList();
		if (LivrosEncontrados.Count() == 0)
		{
			MostrarErro($"Nenhum Livro Com este Titulo Foi Encontrado");
			return;
		}
		var LivroSelecionado = SelecionarLivroComEsteTitulo(LivrosEncontrados);
		var livro = biblioteca.GetLivro(LivroSelecionado);
		Console.WriteLine($"Livro: {livro.Titulo} De {livro.Autor}");
		Console.Write("\nCPF: ");
		string? usuarioEntrada = Console.ReadLine();
		if (string.IsNullOrWhiteSpace(usuarioEntrada))
		{
			MostrarErro("Entrada Invalida Tente Novamente");
			return;
		}
		if (!long.TryParse(usuarioEntrada, out long cpf))
		{
			MostrarErro("CPF Deve ser um numero");
			return;
		}
		var usuario = biblioteca.GetUsuarioPeloCpf(cpf);
		try
		{
			biblioteca.ReservarLivro(usuario, livro);
			Console.WriteLine($"Reserva Realizada {usuario.Name} - {usuario.TipoDoUsuario} - {livro.Titulo}");
			Thread.Sleep(2000);
		}
		catch (InvalidOperationException ex)
		{
			Console.WriteLine(ex.Message);
			Thread.Sleep(2000);
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
	static void TituloMenuAtual (string titulo)
	{
		Console.WriteLine();
		Console.WriteLine($"*** {titulo} ***".PadLeft(35));
		Console.WriteLine("".PadLeft(50, '='));
        Console.WriteLine();

	}
	static void MostrarErro(string mensagem)
	{
		Console.WriteLine(mensagem);
		Thread.Sleep(2000);
		
	}
}


