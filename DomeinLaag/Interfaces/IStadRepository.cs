using DomeinLaag.Klassen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeinLaag.Interfaces
{
    public interface IStadRepository
    {
        List<Stad> GeefStedenLand(int id);
        bool HeeftSteden(int landId);
        Stad StadToevoegen(Stad stad);
        bool BestaatStad(int id);
        Stad StadWeergeven(int stadId);
        void StadVerwijderen(int stadId);
        Stad StadUpdaten(Stad stad);
        bool ControleerBevolkingsaantal(int landId, int bevolkingsaantal);
    }
}
