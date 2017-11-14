using ProjetoDDD.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDDD.Services.Interfaces
{
    public interface IProdutoService
    {
        List<ProdutoDTO> Listar(ListarDTO dto);
    }
}
