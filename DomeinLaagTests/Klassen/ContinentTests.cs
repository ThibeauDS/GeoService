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
    public class ContinentTests
    {
        #region Properties
        private readonly Continent _continent;
        #endregion

        #region Constructors
        public ContinentTests()
        {
            _continent = new(1, "Europa", 0);
        }
        #endregion

        #region Tests
        [Theory()]
        [InlineData(1, "Europa", 0)]
        public void ContinentTest(int id, string naam, int bevolkingsaantal)
        {
            Continent continent = new(id, naam, bevolkingsaantal);
            Assert.Equal(_continent, continent);
        }

        [Theory()]
        [InlineData("Europa", 0)]
        public void ContinentTest1(string naam, int bevolkingsaantal)
        {
            Continent continent = new(naam, bevolkingsaantal);
            Assert.Equal(_continent.Naam, continent.Naam);
            Assert.Equal(_continent.Bevolkingsaantal, continent.Bevolkingsaantal);
        }

        [Theory()]
        [InlineData("Europa")]
        public void ContinentTest2(string naam)
        {
            Continent continent = new(naam);
            Assert.Equal(_continent.Naam, continent.Naam);
        }

        [Theory()]
        [InlineData(0)]
        public void ZetIdTest(int id)
        {
            Assert.Throws<ContinentException>(() => _continent.ZetId(id));
        }

        [Theory()]
        [InlineData("")]
        public void ZetNaamTest(string naam)
        {
            Assert.Throws<ContinentException>(() => _continent.ZetNaam(naam));
        }

        [Theory()]
        [InlineData(null)]
        public void ZetNaamTest2(string naam)
        {
            Assert.Throws<ContinentException>(() => _continent.ZetNaam(naam));
        }

        [Theory()]
        [InlineData(-1)]
        public void ZetBevolkingsaantalTest(int bevolkingsaantal)
        {
            Assert.Throws<ContinentException>(() => _continent.ZetBevolkingsaantal(bevolkingsaantal));
        }
        #endregion
    }
}