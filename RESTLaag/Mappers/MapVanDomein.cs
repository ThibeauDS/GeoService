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
        public static ContinentRESToutputDTO MapVanContinentDomein(string url, Continent continent, LandService landService)
        {
            string continentURL = $"{url}/Continent/{continent.Id}";
            List<string> landen = landService.GeefLandenContinent(continent.Id).Select(x => continentURL + $"/Land/{x.Id}").ToList();
            ContinentRESToutputDTO dto = new(continentURL, continent.Naam, continent.Bevolkingsaantal, landen);
            return dto;
        }

        internal static LandRESToutputDTO MapVanLandDomein(string url, Land land, StadService stadService)
        {
            throw new NotImplementedException();
        }
    }
}
