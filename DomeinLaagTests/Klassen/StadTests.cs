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
    public class StadTests
    {
        #region Properties
        private readonly Continent _continent;
        private readonly Land _land;
        private readonly Stad _stad;
        #endregion

        #region Constructors
        public StadTests()
        {
            _continent = new(1, "Europa", 0);
            _land = new(1, "België", 11000000, 40000000, _continent);
            _stad = new(1, "Woubrechtegem", 250, false, _land);
        }
        #endregion

        [Theory()]
        [InlineData(1, "Woubrechtegem", 250, false)]
        public void StadTest(int id, string naam, int bevolkingsaantal, bool isHoofdstad)
        {
            Stad stad = new(id, naam, bevolkingsaantal, isHoofdstad, _land);
            Assert.Equal(_stad.Id, stad.Id);
            Assert.Equal(_stad.Naam, stad.Naam);
            Assert.Equal(_stad.Bevolkingsaantal, stad.Bevolkingsaantal);
            Assert.Equal(_stad.IsHoofdstad, stad.IsHoofdstad);
            Assert.Equal(_stad.Land, stad.Land);
        }

        [Theory()]
        [InlineData("Woubrechtegem", 250, false)]
        public void StadTest1(string naam, int bevolkingsaantal, bool isHoofdstad)
        {
            Stad stad = new(naam, bevolkingsaantal, isHoofdstad, _land);
            Assert.Equal(_stad.Naam, stad.Naam);
            Assert.Equal(_stad.Bevolkingsaantal, stad.Bevolkingsaantal);
            Assert.Equal(_stad.IsHoofdstad, stad.IsHoofdstad);
            Assert.Equal(_stad.Land, stad.Land);
        }

        [Theory()]
        [InlineData(0)]
        public void ZetIdTest(int id)
        {
            Assert.Throws<StadException>(() => _stad.ZetId(id));
        }

        [Theory()]
        [InlineData("")]
        public void ZetNaamTest(string naam)
        {
            Assert.Throws<StadException>(() => _stad.ZetNaam(naam));
        }

        [Theory()]
        [InlineData(null)]
        public void ZetNaamTest2(string naam)
        {
            Assert.Throws<StadException>(() => _stad.ZetNaam(naam));
        }

        [Theory()]
        [InlineData(0)]
        public void ZetBevolkingsaantalTest(int bevolkingsaantal)
        {
            Assert.Throws<StadException>(() => _stad.ZetBevolkingsaantal(bevolkingsaantal));
        }

        [Theory()]
        [InlineData(true)]
        public void ZetIsHoofdstadTest(bool isHoofdstad)
        {
            Assert.True(isHoofdstad);
        }

        [Theory()]
        [InlineData(false)]
        public void ZetIsHoofdstadTest2(bool isHoofdstad)
        {
            Assert.False(isHoofdstad);
        }

        [Theory()]
        [InlineData(null)]
        public void ZetLandTest(Land land)
        {
            Assert.Throws<StadException>(() => _stad.ZetLand(land));
        }
    }
}