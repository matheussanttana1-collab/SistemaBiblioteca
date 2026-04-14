using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaBiblioteca.Modelos;

public class Livro
{
    public Livro(string titulo, string autor, int anoPublicação)
    {
        Titulo = titulo;
        Id = Guid.NewGuid();
        Autor = autor;
        AnoPublicação = anoPublicação;
			this.StatusDoLivro = StatusDoLivro.Disponivel;
		}

    public string Titulo { get; }
    public Guid Id { get; }
    public string Autor { get; }
    public int AnoPublicação { get; }
    public StatusDoLivro StatusDoLivro { get; private set; }
    public Usuario? UsuarioQueReservou { get; private set; }
    public Usuario? UsuarioQueEmprestou { get; private set; }

    public void Emprestar(Usuario usuario)
    {
        if (StatusDoLivro != StatusDoLivro.Disponivel && UsuarioQueReservou != usuario)
        {
            throw new InvalidOperationException($"Impossivel realizar emprestimo pois o livro ja esta {StatusDoLivro}");
        }
        StatusDoLivro = StatusDoLivro.Emprestado;
        UsuarioQueEmprestou = usuario;
        UsuarioQueReservou = null;

    }
    public void Reservar(Usuario usuario)
    {
			if (StatusDoLivro == StatusDoLivro.Reservado || StatusDoLivro == StatusDoLivro.Reservado)
			{
				throw new InvalidOperationException($"Impossivel realizar reserva pois o livro esta {StatusDoLivro}");
			}
        UsuarioQueReservou = usuario;  
			StatusDoLivro = StatusDoLivro.Reservado;
    }
    public void Disponivel()
    {
			if (StatusDoLivro == StatusDoLivro.Disponivel)
			{
				throw new InvalidOperationException($"Livro já esta Disponivel");
			}
			StatusDoLivro = StatusDoLivro.Disponivel;
    }
    public void Inativar()
    {
			if (StatusDoLivro != StatusDoLivro.Disponivel)
			{
				throw new InvalidOperationException($"Impossivel inativar o livro pois ja esta {StatusDoLivro}");
			}
			StatusDoLivro = StatusDoLivro.Inativo;
    }        
}
