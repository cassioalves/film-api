using Film.Business;
using Film.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        [Route("GetAllMenus")]
        public IActionResult GetAllMenus()
        {
            filmsBusiness.GetProducers();

            return StatusCode(200, "");
        }
    }
}
