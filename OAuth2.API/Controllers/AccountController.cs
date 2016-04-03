using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using OAuth2.API.Models;
using OAuth2.API.Services;

namespace OAuth2.API.Controllers
{
    [RoutePrefix("Account")]
    public class AccountController : ApiController
    {
        private UserService _userService;
        public AccountController()
        {
            _userService = new UserService();
        }

        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(UserModel user)
        {
            if (user == null || !ModelState.IsValid)
                return BadRequest();
            await _userService.Register(user);

            return Ok();
        }
    }
}
