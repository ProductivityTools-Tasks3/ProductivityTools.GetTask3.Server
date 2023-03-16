using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ProductivityTools.GetTask3.API.Controllers
{
    public class GTController : ControllerBase
    {
        protected string UserEmail
        {
            get
            {
                var x1 = HttpContext.User;
                var identity = (ClaimsIdentity)HttpContext.User.Identity;
                IEnumerable<Claim> claims = identity.Claims;
                var email = claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
                return email;
            }
        }
    }
}
