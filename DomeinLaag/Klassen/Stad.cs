using DomeinLaag.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeinLaag.Klassen
{
    public class Stad
    {
        #region Properties
        public int Id { get; private set; }
        public string Naam { get; private set; }
        public int Bevolkingsaantal { get; private set; }
        public bool IsHoofdstad { get; private set; }
        public Land Land { get; private set; }
        #endregion

        #region Constructors
        public Stad(int id, string naam, int bevolkingsaantal, bool isHoofdstad, Land land) : this(naam, bevolkingsaantal, isHoofdstad, land)
        {
            ZetId(id);
        }

        public Stad(string naam, int bevolkingsaantal, bool isHoofdstad, Land land)
        {
            ZetNaam(naam);
            ZetBevolkingsaantal(bevolkingsaantal);
            ZetIsHoofdstad(isHoofdstad);
            ZetLand(land);
        }
        #endregion

        #region Methods
        public void ZetId(int id)
        {
            if (id <= 0)
            {
                throw new StadException("Id moet groter zijn dan 0.");
            }
            Id = id;
        }

        public void ZetNaam(string naam)
        {
            if (string.IsNullOrWhiteSpace(Naam))
            {
                throw new StadException("Naam mag niet leeg zijn.");
            }
            Naam = naam;
        }

        public void ZetBevolkingsaantal(int bevolkingsaantal)
        {
            if (bevolkingsaantal <= 0)
            {
                throw new StadException("Bevolkingsaantal kan niet kleiner of gelijk zijn dan 0.");
            }
            Bevolkingsaantal = bevolkingsaantal;
        }

        public void ZetIsHoofdstad(bool isHoofdstad)
        {
            IsHoofdstad = isHoofdstad;
        }

        public void ZetLand(Land land)
        {
            if (land == null)
            {
                throw new StadException("Object land kan niet null zijn.");
            }
            Land = land;
        }
        #endregion
    }
}
