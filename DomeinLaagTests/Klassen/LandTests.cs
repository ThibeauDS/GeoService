using Xunit;
using DomeinLaag.Klassen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomeinLaag.Exceptions;

namespace DomeinLaag.Klassen.Tests
{
    public class LandTests
    {
        #region Properties
        private readonly Continent _continent;
        private readonly Land _land;
        #endregion

        #region Constructors
        public LandTests()
        {
            _continent = new(1, "Europa", 0);
            _land = new(1, "België", 11000000, 40000000, _continent);
        }
        #endregion

        #region Tests
        [Theory()]
        [InlineData(1, "België", 11000000, 40000000)]
        public void LandTest(int id, string naam, int bevolkingsaantal, decimal oppervlakte)
        {
            Land land = new(id, naam, bevolkingsaantal, oppervlakte, _continent);
            Assert.Equal(_land, land);
        }

        [Theory()]
        [InlineData("België", 11000000, 40000000)]
        public void LandTest1(string naam, int bevolkingsaantal, decimal oppervlakte)
        {
            Land land = new(naam, bevolkingsaantal, oppervlakte, _continent);
            Assert.Equal(_land.Naam, land.Naam);
            Assert.Equal(_land.Bevolkingsaantal, land.Bevolkingsaantal);
            Assert.Equal(_land.Oppervlakte, land.Oppervlakte);
            Assert.Equal(_land.Continent, land.Continent);
        }

        [Theory()]
        [InlineData(0)]
        public void ZetIdTest(int id)
        {
            Assert.Throws<LandException>(() => _land.ZetId(id));
        }

        [Theory()]
        [InlineData("")]
        public void ZetNaamTest(string naam)
        {
            Assert.Throws<LandException>(() => _land.ZetNaam(naam));
        }

        [Theory()]
        [InlineData(null)]
        public void ZetNaamTest2(string naam)
        {
            Assert.Throws<LandException>(() => _land.ZetNaam(naam));
        }

        [Theory()]
        [InlineData(0)]
        public void ZetBevolkingsaantalTest(int bevolkingsaantal)
        {
            Assert.Throws<LandException>(() => _land.ZetBevolkingsaantal(bevolkingsaantal));
        }

        [Theory()]
        [InlineData(0)]
        public void ZetOppervlakteTest(decimal oppervlakte)
        {
            Assert.Throws<LandException>(() => _land.ZetOppervlakte(oppervlakte));
        }

        [Theory()]
        [InlineData(null)]
        public void ZetContinentTest(Continent continent)
        {
            Assert.Throws<LandException>(() => _land.ZetContinent(continent));
        } 
        #endregion
    }
}