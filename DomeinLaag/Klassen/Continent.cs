using DomeinLaag.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeinLaag.Klassen
{
    public class Continent
    {
        #region Properties
        public int Id { get; private set; }
        public string Naam { get; private set; }
        public int Bevolkingsaantal { get; private set; }
        #endregion

        #region Constructors
        public Continent(int id, string naam, int bevolkingsaantal) : this(naam, bevolkingsaantal)
        {
            ZetId(id);
        }

        public Continent(string naam, int bevolkingsaantal) : this(naam)
        {
            ZetBevolkingsaantal(bevolkingsaantal);
        }

        public Continent(string naam)
        {
            ZetNaam(naam);
        }
        #endregion

        #region Methods
        public void ZetId(int id)
        {
            if (id <= 0)
            {
                throw new ContinentException("Id moet groter zijn dan 0.");
            }
            Id = id;
        }

        public void ZetNaam(string naam)
        {
            if (string.IsNullOrWhiteSpace(Naam))
            {
                throw new ContinentException("Naam mag niet leeg zijn");
            }
            Naam = naam;
        }

        public void ZetBevolkingsaantal(int bevolkingsaantal)
        {
            if (bevolkingsaantal < 0)
            {
                throw new ContinentException("Bevolkingsaantal kan niet kleiner zijn dan 0.");
            }
            Bevolkingsaantal = bevolkingsaantal;
        }
        #endregion
    }
}
