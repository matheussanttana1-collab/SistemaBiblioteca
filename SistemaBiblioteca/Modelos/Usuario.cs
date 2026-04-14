using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;


namespace SistemaBiblioteca.Modelos;

public class Usuario
{
    public Usuario(string name, long cpf, TipoUsuario tipoUsuario)
    {
        IdUsuario = Guid.NewGuid();
        Name = name;
        CPF = cpf;
        TipoDoUsuario = tipoUsuario;
        Emprestimos = new List<Emprestimo>();
        AtividadeUsuario = StatusAtividade.Ativo;
    }

    public Guid IdUsuario { get; }
    public string Name { get; }
    public long CPF { get; }
    public TipoUsuario TipoDoUsuario { get; }
    public StatusAtividade AtividadeUsuario { get; private set; }
    private List<Emprestimo> Emprestimos = new List<Emprestimo>();
    public List<Livro> Reservas = new List<Livro>();
    public IReadOnlyCollection<Emprestimo> _Emprestimos => Emprestimos;

    private int LimiteDeEmprestimos
    {
        get
        {
            return TipoDoUsuario switch
            {
                TipoUsuario.Aluno => 1,
                TipoUsuario.Professor => 5,
                TipoUsuario.Funcionario => 3,
                _ => 0,
            };
        }
    }
    private int LimiteDeReservas
    {
        get
        {
            return TipoDoUsuario switch
            {
                TipoUsuario.Aluno => 2,
                TipoUsuario.Professor => 3,
                TipoUsuario.Funcionario => 2,
                _ => 0,
            };
        }
    }
    internal void DesativarUsuario()
    {
        if (AtividadeUsuario == StatusAtividade.Inativo)
        {
            throw new InvalidOperationException("Usuario ja Esta Inativo");
        }
        AtividadeUsuario = StatusAtividade.Inativo;
    }
    public void AdicionarEmprestimoAoUsuario(Emprestimo emprestimo)
    {
        if (!VerificarEmprestimos())
        {
            throw new InvalidOperationException("O usuário não está ativo para realizar empréstimos.");
        }
        Emprestimos.Add(emprestimo);
    }
    public void DevolverLivro(Emprestimo emprestimo)
    {
        if (!Emprestimos.Contains(emprestimo))
        {
            throw new InvalidOperationException("Emprestimo Não Existe");
        }
        Emprestimos.Remove(emprestimo);
    }
    public void ReservarLivro(Livro livro)
    {
        if (!VerificarReserva())
        {
            throw new InvalidOperationException("O usuário não está ativo para realizar Reservas.");
        }
        Reservas.Add(livro);
    }  
    public bool VerificarEmprestimos()
        => Emprestimos.Count <= LimiteDeEmprestimos && AtividadeUsuario != StatusAtividade.Inativo;
    public bool VerificarReserva()
        => Reservas.Count <= LimiteDeReservas && AtividadeUsuario != StatusAtividade.Inativo;
	}
