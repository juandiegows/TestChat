using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using TestChat.Helpers;
using TestChat.Models;
using TestChat.Models.view;

namespace TestChat.Controllers
{
    public class AuthController : ApiController
    {
        [HttpPost]
        [Route("auth")]
        public IHttpActionResult Index([FromBody] LoginRequest login) {

            using (Mobile2Entities model = new Mobile2Entities()) {

                try {
                    StudentResponse usuario = model.Students.ToList()
                        .Where(x => (login.Username.ToLower() == x.username &&  x.password.ToString() == Encrypt.GetSHA256(login.Password.ToString())))
                        .Select(x=> new StudentResponse() {
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            Id = x.Id,
                            Photo = x.Photo,
                        })
                        .FirstOrDefault();

                    if (usuario == default(StudentResponse)) {

                        return Content<LoginResponse>(System.Net.HttpStatusCode.Unauthorized, null);
                    }

                    return Ok(new LoginResponse {
                        Token = TokenGenerator.GenerateTokenJwt(usuario.Id.ToString()),
                        DateTime = DateTime.Now,
                        User = usuario

                    });
                }
                catch (Exception e) {

                    return Content(System.Net.HttpStatusCode.BadRequest, e);
                }
            }

        }
    }
}
