using LibrarySystem.DomainExcpetion.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Applications.Ports.Out;

public interface IEmprestimoRepository
{
	Task<IEnumerable<Emprestimo>> BuscarLivros(int? take, int? skip);
	Task<IEnumerable<Emprestimo>> BuscarEmprestimosDoUsuario(Guid UserId);
	Task<Emprestimo> BuscarEmprestimoPeloId(Guid id);
	Task AdicionarEmprestimo(Emprestimo livro);
	Task SalvarMudancas(Emprestimo livro);
	Task DeletarEmprestimo(Emprestimo livro);
}
