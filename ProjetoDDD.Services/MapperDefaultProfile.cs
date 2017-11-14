using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoDDD.Model;
using ProjetoDDD.DTO;

namespace ProjetoDDD.Services
{
    public class MapperDefaultProfile : Profile
    {
        public MapperDefaultProfile()
        {
            CreateMap<Produto, ProdutoDTO>();
            CreateMap<ProdutoDTO, Produto>();
        }
    }
}
