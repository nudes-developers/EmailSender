
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TesteEmail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly TesteService _testeService;
        public ValuesController(TesteService testeService)
        {
            _testeService = testeService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("teste")]
        public async Task Testar()
        {
            await _testeService.SendEmailWithTemplate(new Nudes.Email.EmailMessageView { Subject = "teste", Content = "" }, "teste@gmail.com", new { Nome = "NomeTeste", Idade = "20" });
        }

        [HttpPost("email")]
        public async Task SendEmailTest([FromBody] EmailViewModel model)
        {
            await _testeService.SendEmailTeste(model.Subject, model.Content, model.Email);
        }
    }    
}
