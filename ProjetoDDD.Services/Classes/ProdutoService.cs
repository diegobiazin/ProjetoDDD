using AutoMapper;
using ProjetoDDD.DTO;
using ProjetoDDD.Interfaces;
using ProjetoDDD.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDDD.Services.Classes
{
    public class ProdutoService : IProdutoService
    {
        private readonly IUnitOfWork _iUnitOfWork;
        public ProdutoService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        public List<ProdutoDTO> Listar(ListarDTO dto)
        {
            var query = _iUnitOfWork.ProdutoRepository.Query();
            if (dto.Skip != null)
                query = query.Skip(dto.Skip.Value);
            if (dto.Take != null)
                query = query.Take(dto.Take.Value);
            return Mapper.Map<List<ProdutoDTO>>(query.ToList());
        }
    }
}
