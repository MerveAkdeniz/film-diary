using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace FilmDiary.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AiController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AiController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("generate-description")]
        public IActionResult GenerateDescription([FromBody] string title)
        {
            var fakeResponse = $"{title} filmi, izleyiciyi içine çeken etkileyici bir hikayeye sahiptir.";

            return Ok(fakeResponse);
        }
    }
}
