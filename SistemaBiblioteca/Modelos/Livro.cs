using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaBiblioteca.Modelos
{
    internal class Livro
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

        public void Emprestar()
        {
            if (StatusDoLivro != StatusDoLivro.Disponivel)
            {
                throw new InvalidOperationException($"Impossivel realizar emprestimo pois o livro ja esta {StatusDoLivro}");
            }
            StatusDoLivro = StatusDoLivro.Emprestado;
        }
        public void Reservar()
        {
			if (StatusDoLivro != StatusDoLivro.Disponivel)
			{
				throw new InvalidOperationException($"Impossivel realizar reserva pois o livro ja esta {StatusDoLivro}");
			}
			StatusDoLivro = StatusDoLivro.Reservado;
        }
        public void Devolver()
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
}
