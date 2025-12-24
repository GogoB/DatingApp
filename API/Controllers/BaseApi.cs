using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // one of its functionalities is it 
    //automatically tries to validate parameters sent to method
    //no ako imas required vo parametrite togas moze da se prati prazno duri
    public class BaseApi : ControllerBase
    {
    }
}
