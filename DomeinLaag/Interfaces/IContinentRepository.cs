using DomeinLaag.Klassen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeinLaag.Interfaces
{
    public interface IContinentRepository
    {
        Continent ContinentToevoegen(Continent continent);
        bool BestaatContinent(int continentId);
        bool BestaatContinent(string naam);
        Continent ContinentWeergeven(int continentId);
    }
}
