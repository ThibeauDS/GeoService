using DomeinLaag.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeinLaag.Klassen
{
    public class Land
    {
        #region Properties
        public int Id { get; private set; }
        public string Naam { get; private set; }
        public int Bevolkingsaantal { get; private set; }
        public decimal Oppervlakte { get; private set; }
        public Continent Continent { get; private set; }
        #endregion

        #region Constructors
        public Land(int id, string naam, int bevolkingsaantal, decimal oppervlakte, Continent continent) : this(naam, bevolkingsaantal, oppervlakte, continent)
        {
            ZetId(id);
        }

        public Land(string naam, int bevolkingsaantal, decimal oppervlakte, Continent continent)
        {
            ZetNaam(naam);
            ZetBevolkingsaantal(bevolkingsaantal);
            ZetOppervlakte(oppervlakte);
            ZetContinent(continent);
        }
        #endregion

        #region Methods
        public void ZetId(int id)
        {
            if (id <= 0)
            {
                throw new LandException("Id moet groter zijn dan 0.");
            }
            Id = id;
        }

        public void ZetNaam(string naam)
        {
            if (string.IsNullOrWhiteSpace(naam))
            {
                throw new LandException("Naam mag niet leeg zijn.");
            }
            Naam = naam;
        }

        public void ZetBevolkingsaantal(int bevolkingsaantal)
        {
            if (bevolkingsaantal <= 0)
            {
                throw new LandException("Bevolkingsaantal kan niet kleiner of gelijk zijn dan 0.");
            }
            Bevolkingsaantal = bevolkingsaantal;
        }

        public void ZetOppervlakte(decimal oppervlakte)
        {
            if (oppervlakte <= 0)
            {
                throw new LandException("Oppervlakte kan niet kleiner of gelijk zijn dan 0.");
            }
            Oppervlakte = oppervlakte;
        }

        public void ZetContinent(Continent continent)
        {
            if (continent == null)
            {
                throw new LandException("Object continent kan niet null zijn.");
            }
            Continent = continent;
        }

        public override bool Equals(object obj)
        {
            return obj is Land land &&
                   Naam == land.Naam &&
                   EqualityComparer<Continent>.Default.Equals(Continent, land.Continent);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Naam, Continent);
        }
        #endregion
    }
}
