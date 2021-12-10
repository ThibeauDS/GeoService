using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RESTLaag.Controllers;
using DomeinLaag.Klassen;
using Moq;
using Microsoft.Extensions.Logging;
using DomeinLaag.Services;
using DomeinLaag.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace RESTLaagTests.Controllers
{
    public class GeoServiceControllerTests
    {
        #region Properties
        private readonly GeoServiceController _geoServiceController;
        private static readonly Continent _continent = new(1, "Europa", 0);
        private static readonly Land _land = new(1, "België", 500, 500, _continent);
        private readonly Stad _stad = new(1, "Brussel", 250, true, _land);
        private readonly Mock<ContinentService> _moqContientnService;
        private readonly Mock<LandService> _moqLandService;
        private readonly Mock<StadService> _moqStadService;
        private readonly Mock<Logger<GeoServiceController>> _logger;
        #endregion

        #region Constructors
        public GeoServiceControllerTests()
        {
            _moqContientnService = new Mock<ContinentService>();
            _moqLandService = new Mock<LandService>();
            _moqStadService = new Mock<StadService>();
            _logger = new Mock<Logger<GeoServiceController>>();
            _geoServiceController = new GeoServiceController(_moqContientnService.Object, _moqLandService.Object, _moqStadService.Object);
        }
        #endregion

        #region Tests
        [Fact]
        public void GET_UnknownID_ReturnsNotFound()
        {
            _moqContientnService.Setup(service => service.ContinentWeergeven(2)).Throws(new ContinentServiceException("Kan continent niet weergeven."));
            var result = _geoServiceController.GetContinent(2).Result;
            Assert.IsType<NotFoundObjectResult>(result);
        }
        #endregion
    }
}
