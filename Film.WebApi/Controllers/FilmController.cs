using Film.Business;
using Film.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Film.WebApi.Controllers
{
    public class FilmController : Controller
    {
        private readonly FilmsBusiness filmsBusiness;
        private readonly FilmContext filmContext;
        public FilmController(FilmContext filmContext)
        {
            this.filmContext = filmContext;
            this.filmsBusiness = new FilmsBusiness(filmContext);
        }

        [HttpGet]
        [Route("GetProducers")]
        public IActionResult GetProducers()
        {
            var response = filmsBusiness.GetProducers();

            return StatusCode(200, response);
        }
    }
}
