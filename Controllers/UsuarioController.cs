using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using react.api.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace react.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IMediator mediator, ILogger<UsuarioController> logger)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page, int size)
        {
            var result = await _mediator.Send(new Get { Page = page, Size = size });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Crear model)
        {
            await _mediator.Send(model);

            return Ok();
        }

    }
}
