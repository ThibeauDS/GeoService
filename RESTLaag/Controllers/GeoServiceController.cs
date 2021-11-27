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
    [Route("[controller]/api/Continent")]
    public class GeoServiceController : ControllerBase
    {
        #region Properties
        private readonly string _url = Startup.url;
        private readonly ContinentService _continentService;
        private readonly LandService _landService;
        private readonly StadService _stadService;
        #endregion

        #region Constructors
        public GeoServiceController(ContinentService continentService, LandService landService, StadService stadService)
        {
            _continentService = continentService;
            _landService = landService;
            _stadService = stadService;
        }
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
                Continent continent = _continentService.ContinentToevoegen(MapNaarDomein.MapNaarContinentDomein(dto));
                return CreatedAtAction(nameof(GetContinent), new { continentId = continent.Id }, MapVanDomein.MapVanContinentDomein(_url, continent, _landService));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{continentId}")]
        public IActionResult DeleteContinent(int continentId)
        {
            try
            {
                if (_landService.HeeftLanden(continentId))
                {
                    return BadRequest();
                }
                _continentService.ContinentVerwijderen(continentId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{continentId}")]
        public ActionResult<ContinentRESToutputDTO> PutContinent(int continentId, [FromBody] ContinentRESTinputDTO dto)
        {
            try
            {
                if (!_continentService.BestaatContinent(continentId) || dto == null || string.IsNullOrWhiteSpace(dto.Naam))
                {
                    return BadRequest();
                }
                Continent continent = MapNaarDomein.MapNaarContinentDomein(dto);
                continent.ZetId(continentId);
                Continent continentDb = _continentService.ContinentUpdaten(continent);
                return CreatedAtAction(nameof(GetContinent), new { continentId = continent.Id }, MapVanDomein.MapVanContinentDomein(_url, continent, _landService));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region Land
        [HttpGet]
        [Route("{continentId}/Land/{landId}")]
        public ActionResult<LandRESToutputDTO> GetLand(int landId)
        {
            try
            {
                Land land = _landService.LandWeergeven(landId);
                return Ok(MapVanDomein.MapVanLandDomein(_url, land, _stadService));
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpPost]
        [Route("{continentId}/Land")]
        public ActionResult<LandRESToutputDTO> PostLand(int continentId, [FromBody] LandRESTinputDTO dto)
        {
            try
            {
                if (continentId != dto.ContinentId)
                {
                    return BadRequest("ContinentId klopt niet.");
                }
                Land land = _landService.LandToevoegen(MapNaarDomein.MapNaarLandDomein(dto, _continentService));
                return CreatedAtAction(nameof(GetLand), new { continentId = continentId, landId = land.Id }, MapVanDomein.MapVanLandDomein(_url, land, _stadService));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route("{continentId}/Land/{landId}")]
        public IActionResult DeleteLand(int continentId, int landId)
        {
            try
            {

                if (_stadService.HeeftSteden(landId))
                {
                    return BadRequest();
                }
                _landService.LandVerwijderen(landId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion
    }
}
