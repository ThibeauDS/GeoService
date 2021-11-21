using DomeinLaag.Klassen;
using DomeinLaag.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RESTLaag.Exceptions;
using RESTLaag.Mappers;
using RESTLaag.Model.Input;
using RESTLaag.Model.Output;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace RESTLaag.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class GeoServiceController : ControllerBase
    {
        #region Properties
        private readonly string _url = Startup.url;
        private readonly ContinentService _continentService;
        private readonly LandService _landService;
        #endregion

        #region Continent
        [HttpGet("{continentId}")]
        public ActionResult<ContinentRESToutputDTO> GetContinent(int continentId)
        {
            try
            {
                Continent continent = _continentService.ContinentWeergeven(continentId);
                return Ok(MapVanDomein.MapVanContinentDomein(_url, continent, _landService));
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpPost]
        public ActionResult<ContinentRESToutputDTO> PostContinent([FromBody] ContinentRESTinputDTO dto)
        {
            try
            {
                if (_continentService.BestaatContinent(dto.Naam))
                {
                    return BadRequest("Continent bestaat al.");
                }
                //TODO: ContinentToevoegen verder afwerken.
                Continent continent = _continentService.ContinentToevoegen(MapNaarDomein.MapNaarContinentDomein(dto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion
    }
}
