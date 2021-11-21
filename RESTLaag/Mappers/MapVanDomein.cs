using DomeinLaag.Klassen;
using DomeinLaag.Services;
using RESTLaag.Model.Output;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RESTLaag.Mappers
{
    public class MapVanDomein
    {
        internal static ContinentRESToutputDTO MapVanContinentDomein(string url, Continent continent, LandService landService)
        {
            string continentURL = $"{url}/Continent/{continent.Id}";
            List<string> landen = landService.GeefLandenContinent(continent.Id).Select(x => continentURL + $"/Land/{x.Id}").ToList();
        }
    }
}
