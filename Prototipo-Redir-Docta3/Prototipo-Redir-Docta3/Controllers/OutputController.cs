using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prototipo_Redir_Docta3.Dtos;

namespace Prototipo_Redir_Docta3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OutputController : ControllerBase
    {

        public OutputController()
        {

        }

        // POST api/output
        [HttpPost]
        public void Post([FromBody] OutputDto output)
        {
            TramasBuffer.Trama = output.Output;
        }
    }
}
