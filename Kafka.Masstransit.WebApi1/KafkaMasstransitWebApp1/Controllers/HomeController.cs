using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KafkaMasstransitWebApp1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebAppController : ControllerBase
    {

        [HttpPost("{title}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> PostAsync(string title)
        {
            return Ok(title);
        }
    }
}
