using ProjetoDDD.DTO;
using ProjetoDDD.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjetoDDD.WebApi.Controllers
{
    [RoutePrefix("api/Produto")]
    public class ProdutoController : ApiController
    {
        private readonly IProdutoService _produtoService;
        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [Authorize]
        [HttpGet]
        [Route("ListarProdutos")]
        public HttpResponseMessage ListarFuncionarios([FromUri] ListarDTO entrada)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _produtoService.Listar(entrada));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
