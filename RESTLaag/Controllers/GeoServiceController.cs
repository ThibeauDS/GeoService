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
        private readonly ILogger _logger;
        #endregion

        #region Constructors
        public GeoServiceController(ContinentService continentService, LandService landService, StadService stadService, ILogger<GeoServiceController> logger)
        {
            _continentService = continentService;
            _landService = landService;
            _stadService = stadService;
            _logger = logger;
        }
        #endregion

        #region Continent
        [HttpGet("{continentId}")]
        public ActionResult<ContinentRESToutputDTO> GetContinent(int continentId)
        {
            try
            {
                Continent continent = _continentService.ContinentWeergeven(continentId);
                _logger.LogInformation("GetContinent methode werdt succesvol opgeroepen.");
                return Ok(MapVanDomein.MapVanContinentDomein(_url, continent, _landService));
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetContinent methode error: {ex.Message}");
                return NotFound(ex);
            }
        }

        [HttpPost]
        public ActionResult<ContinentRESToutputDTO> PostContinent([FromBody] ContinentRESTinputDTO dto)
        {
            try
            {
                Continent continent = _continentService.ContinentToevoegen(MapNaarDomein.MapNaarContinentDomein(dto));
                _logger.LogInformation("PostContinent methode werdt succesvol opgeroepen.");
                return CreatedAtAction(nameof(GetContinent), new { continentId = continent.Id }, MapVanDomein.MapVanContinentDomein(_url, continent, _landService));
            }
            catch (Exception ex)
            {
                _logger.LogError($"PostContinent methode error: {ex.Message}");
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
                    _logger.LogError($"Kan land niet verwijderen wegens bevat landen.");
                    return BadRequest();
                }
                _continentService.ContinentVerwijderen(continentId);
                _logger.LogInformation("GetContinent methode werdt succesvol opgeroepen.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeleteContinent methode error: {ex.Message}");
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
                    _logger.LogError($"Het continent bestaat al of ingevulde informatie is leeg/null.");
                    return BadRequest();
                }
                Continent continent = MapNaarDomein.MapNaarContinentDomein(dto);
                continent.ZetId(continentId);
                Continent continentDb = _continentService.ContinentUpdaten(continent);
                _logger.LogInformation("PutContinent methode werdt succesvol opgeroepen.");
                return CreatedAtAction(nameof(GetContinent), new { continentId = continent.Id }, MapVanDomein.MapVanContinentDomein(_url, continent, _landService));
            }
            catch (Exception ex)
            {
                _logger.LogError($"PutContinent methode error: {ex.Message}");
                return BadRequest(ex);
            }
        }
        #endregion

        #region Land
        [HttpGet]
        [Route("{continentId}/Land/{landId}")]
        public ActionResult<LandRESToutputDTO> GetLand(int continentId, int landId)
        {
            try
            {
                if (!_continentService.BestaatContinent(continentId))
                {
                    _logger.LogError($"Het continent bestaat niet.");
                    return BadRequest();
                }
                Land land = _landService.LandWeergeven(landId);
                _logger.LogInformation("GetLand methode werdt succesvol opgeroepen.");
                return Ok(MapVanDomein.MapVanLandDomein(_url, land, _stadService));
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetLand methode error: {ex.Message}");
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
                    _logger.LogError($"Het ingevulde Continent Id komt niet overeen met het Continent Id van in de body.");
                    return BadRequest("ContinentId klopt niet.");
                }
                Land land = _landService.LandToevoegen(MapNaarDomein.MapNaarLandDomein(dto, _continentService));
                _logger.LogInformation("PostLand methode werdt succesvol opgeroepen.");
                return CreatedAtAction(nameof(GetLand), new { continentId, landId = land.Id }, MapVanDomein.MapVanLandDomein(_url, land, _stadService));
            }
            catch (Exception ex)
            {
                _logger.LogError($"PostLand methode error: {ex.Message}");
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route("{continentId}/Land/{landId}")]
        public IActionResult DeleteLand(int continentId, int landId)
        {
            try
            {
                if (!_continentService.BestaatContinent(continentId))
                {
                    _logger.LogError($"Het continent bestaat niet.");
                    return BadRequest();
                }
                if (!_landService.BestaatLand(landId))
                {
                    _logger.LogError($"Het land bestaat niet.");
                    return BadRequest();
                }
                if (_stadService.HeeftSteden(landId))
                {
                    _logger.LogError($"Het opgegeven land kan niet worden verwijderd wegens bevat steden.");
                    return BadRequest();
                }
                _landService.LandVerwijderen(landId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeleteLand methode error: {ex.Message}");
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("{continentId}/Land/{landId}")]
        public ActionResult<ContinentRESToutputDTO> PutLand(int continentId, int landId, [FromBody] LandRESTinputDTO dto)
        {
            try
            {
                if (!_continentService.BestaatContinent(continentId) || !_landService.BestaatLand(landId) || dto == null || string.IsNullOrWhiteSpace(dto.Naam))
                {
                    _logger.LogError($"Het continent/land bestaat al of ingevulde informatie is leeg/null.");
                    return BadRequest();
                }
                Land land = MapNaarDomein.MapNaarLandDomein(dto, _continentService);
                land.ZetId(landId);
                Land landDb = _landService.LandUpdaten(land);
                _logger.LogInformation("PutLand methode werdt succesvol opgeroepen.");
                return CreatedAtAction(nameof(GetLand), new { continentId, landId = land.Id }, MapVanDomein.MapVanLandDomein(_url, land, _stadService));
            }
            catch (Exception ex)
            {
                _logger.LogError($"PutLand methode error: {ex.Message}");
                return BadRequest(ex);
            }
        }
        #endregion

        #region Stad
        [HttpGet]
        [Route("{continentId}/Land/{landId}/Stad/{stadId}")]
        public ActionResult<LandRESToutputDTO> GetStad(int continentId, int landId, int stadId)
        {
            try
            {
                if (!_continentService.BestaatContinent(continentId))
                {
                    _logger.LogError($"Het continent bestaat niet.");
                    return BadRequest();
                }
                if (!_landService.BestaatLand(landId))
                {
                    _logger.LogError($"Het land bestaat niet.");
                    return BadRequest();
                }
                Stad stad = _stadService.StadWeergeven(stadId);
                _logger.LogInformation("GetStad methode werdt succesvol opgeroepen.");
                return Ok(MapVanDomein.MapVanStadDomein(_url, stad));
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetStad methode error: {ex.Message}");
                return NotFound(ex);
            }
        }

        [HttpPost]
        [Route("{continentId}/Land/{landId}/Stad")]
        public ActionResult<StadRESToutputDTO> PostStad(int continentId, int landId, [FromBody] StadRESTinputDTO dto)
        {
            try
            {
                if (continentId != dto.ContinentId)
                {
                    _logger.LogError($"Het ingevulde Continent Id komt niet overeen met het Continent Id van in de body.");
                    return BadRequest("ContinentId klopt niet.");
                }
                if (landId != dto.LandId)
                {
                    _logger.LogError($"Het ingevulde Land Id komt niet overeen met het Land Id van in de body.");
                    return BadRequest("LandId klopt niet.");
                }
                Stad stad = _stadService.StadToevoegen(MapNaarDomein.MapNaarStadDomein(dto, _continentService, _landService));
                _logger.LogInformation("PostStad methode werdt succesvol opgeroepen.");
                return CreatedAtAction(nameof(GetStad), new { continentId, landId, stadId = stad.Id }, MapVanDomein.MapVanStadDomein(_url, stad));
            }
            catch (Exception ex)
            {
                _logger.LogError($"PostStad methode error: {ex.Message}");
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route("{continentId}/Land/{landId}/Stad/{stadId}")]
        public IActionResult DeleteStad(int continentId, int landId, int stadId)
        {
            try
            {
                if (!_continentService.BestaatContinent(continentId))
                {
                    _logger.LogError($"Het continent bestaat niet.");
                    return BadRequest();
                }
                if (!_landService.BestaatLand(landId))
                {
                    _logger.LogError($"Het land bestaat niet.");
                    return BadRequest();
                }
                if (!_stadService.BestaatStad(stadId))
                {
                    _logger.LogError($"Stad bestaat niet.");
                    return BadRequest();
                }
                _stadService.StadVerwijderen(stadId);
                _logger.LogInformation("DeleteStad methode werdt succesvol opgeroepen.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeleteStad methode error: {ex.Message}");
                return BadRequest(ex);
            }
        }
        #endregion
    }
}
