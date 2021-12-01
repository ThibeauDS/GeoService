using DomeinLaag.Klassen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeinLaag.Interfaces
{
    public interface ILandRepository
    {
        List<Land> GeefLandenContinent(int id);
        bool HeeftLanden(int continentId);
        Land LandToevoegen(Land land);
        bool BestaatLand(int landId);
        Land LandWeergeven(int landId);
        void LandVerwijderen(int landId);
        Land LandUpdaten(Land land);
    }
}
