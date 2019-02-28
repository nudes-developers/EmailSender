using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TesteController : ControllerBase
    {
        private readonly TesteService _testeService;
        public TesteController(TesteService testeService)
        {
            _testeService = testeService;
        }

        [HttpPost("email")]
        public async Task SendEmailTest([FromBody] EmailViewModel model)
        {
            await _testeService.SendEmailTeste(model.Subject , model.Content, model.Email);
        }
    }
}
