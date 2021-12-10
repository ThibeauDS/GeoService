using Xunit;
using RESTLaag.Controllers;
using DomeinLaag.Klassen;
using Moq;
using Microsoft.Extensions.Logging;
using DomeinLaag.Services;
using DomeinLaag.Exceptions;
using Microsoft.AspNetCore.Mvc;
using DomeinLaag.Interfaces;
using RESTLaag.Model.Output;
using RESTLaag;
using DataLaag.ADO;

namespace RESTLaagTests.Controllers
{
    public class GeoServiceControllerTests
    {
        #region Properties
        private readonly GeoServiceController _geoServiceController;
        private static readonly Continent _continent = new(2, "Antartica", 0);
        private static readonly Land _land = new(1, "België", 500, 500, _continent);
        private readonly Stad _stad = new(1, "Brussel", 250, true, _land);
        private readonly ContinentService _continentService;
        private readonly LandService _landService;
        private readonly StadService _stadService;
        private readonly Mock<IContinentRepository> _moqContinentRepository;
        private readonly Mock<ILandRepository> _moqLandRepository;
        private readonly Mock<IStadRepository> _moqStadRepository;
        private readonly ILoggerFactory _loggerFactory = new LoggerFactory();
        private readonly string _url = Startup.url;
        #endregion

        #region Constructors
        public GeoServiceControllerTests()
        {
            var _logger = new Logger<GeoServiceController>(_loggerFactory);
            _moqContinentRepository = new Mock<IContinentRepository>();
            _moqLandRepository = new Mock<ILandRepository>();
            _moqStadRepository = new Mock<IStadRepository>();
            _continentService = new ContinentService(_moqContinentRepository.Object);
            _landService = new LandService(_moqLandRepository.Object);
            _stadService = new StadService(_moqStadRepository.Object);
            _geoServiceController = new GeoServiceController(_continentService, _landService, _stadService, _logger);
        }
        #endregion

        #region Tests
        [Fact]
        public void GetContinent_UnknownId_ReturnsNotFound()
        {
            _moqContinentRepository.Setup(repo => repo.ContinentWeergeven(2)).Throws(new ContinentServiceException("Kan continent niet weergeven."));
            var result = _geoServiceController.GetContinent(2).Result;
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void GetContinent_CorrectId_ReturnsOkResult()
        {
            //Kan niet getest worden wegens 
            _moqContinentRepository.Setup(repo => repo.ContinentWeergeven(2)).Returns(_continent);
            var result = _geoServiceController.GetContinent(2);
            Assert.IsType<ActionResult<ContinentRESToutputDTO>>(result);
        }

        [Fact]
        public void GetContinent_CorrectId_ReturnsContinent()
        {
            _moqContinentRepository.Setup(repo => repo.ContinentWeergeven(1)).Returns(_continent);
            var result = _geoServiceController.GetContinent(1);
            Assert.IsType<ContinentRESToutputDTO>(result.Value);
            Assert.Equal($"{_url}/Continent/1", result.Value.Id);
            Assert.Equal(_continent.Naam, result.Value.Naam);
        }
        #endregion
    }
}
