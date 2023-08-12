using Microsoft.AspNetCore.Mvc;

namespace DevIO.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        //validacao de notificacoes e erro
        //validacao modelstate
        //validacao da operacao de negocios
    }
}
