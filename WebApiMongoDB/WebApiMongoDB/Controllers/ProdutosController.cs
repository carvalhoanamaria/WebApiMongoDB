using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiMongoDB.Models;
using WebApiMongoDB.Services;

namespace WebApiMongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ProdutoServices _produtoServices;

        public ProdutosController(ProdutoServices produtoServices)
        {
            _produtoServices = produtoServices;
        }


        [HttpPost]
        public async Task<Produto> PostProdutos(Produto produto) 
        {
          await _produtoServices.CreateAsync(produto);
            return produto;
        }

        [HttpGet]
        public async Task<List<Produto>> GetProdutos()
         => await _produtoServices.GetAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProdutoId(string id)
        {
           var prod = await _produtoServices.GetAsync(id);

            if (prod is null)
            {
                return NotFound();
            }
           

            return prod;
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> PutProdutoId(string id, Produto produto) 
        {

           var prod = await _produtoServices.GetAsync(id);


            if (prod is null)
            {
                return NotFound();
            }

            produto.Id = prod.Id;

            await _produtoServices.UpdateAsync(id, produto);

            return NoContent();

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePordutoId(string id) 
        {
            var prod = await _produtoServices.GetAsync(id);

            if( prod is null) 
            {
                return NotFound();
            }


            await _produtoServices.DeleteAsync(id);

            return NoContent();
        }
    }
}
