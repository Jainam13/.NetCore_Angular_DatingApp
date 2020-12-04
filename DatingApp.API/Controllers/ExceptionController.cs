using System;
using DatingApp.API.Data;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExceptionController : ControllerBase
    {
        private readonly DataContext _context;

        public ExceptionController(DataContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "secret text";
        }

        [HttpGet("not-found")]
        public ActionResult<User> GetNotFound()
        {
            var data = _context.Users.Find(-1);
            if (data == null) return NotFound();
            return Ok(data);
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var data = _context.Users.Find(-1);
            return data.ToString();
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBaRequest()
        {
            return BadRequest("Request not valid");
        }
    }
}
