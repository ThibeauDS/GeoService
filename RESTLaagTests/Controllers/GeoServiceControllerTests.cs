using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RESTLaag.Controllers;
using DomeinLaag.Klassen;
using DomeinLaag.Interfaces;
using Moq;
using Microsoft.Extensions.Logging;

namespace RESTLaagTests.Controllers
{
    public class GeoServiceControllerTests
    {
        #region Properties
        private readonly GeoServiceController _geoServiceController;
        private static readonly Continent _continent = new(1, "Europa", 0);
        private static readonly Land _land = new(1, "België", 500, 500, _continent);
        private readonly Stad _stad = new(1, "Brussel", 250, true, _land);
        private readonly Mock<IContinentRepository> _moqContientnRepository;
        private readonly Mock<ILandRepository> _moqLandRepository;
        private readonly Mock<IStadRepository> _moqStadRepository;
        #endregion

        #region Constructors
        public GeoServiceControllerTests()
        {
            _moqContientnRepository = new Mock<IContinentRepository>();
            _moqLandRepository = new Mock<ILandRepository>();
            _moqStadRepository = new Mock<IStadRepository>();
            _geoServiceController = new GeoServiceController(_moqContientnRepository.Object, _moqLandRepository.Object, _moqStadRepository.Object);
        }
        #endregion

        #region Tests

        #endregion
    }
}
