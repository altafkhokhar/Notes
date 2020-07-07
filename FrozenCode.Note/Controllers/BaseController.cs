using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrozenCode.Note.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
        protected int GetUserId()
        {
            return Convert.ToInt32(User.Claims.Where(wh => wh.Type == ClaimTypes.Name).FirstOrDefault().Value);
        }
        
    }
}