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
            int bevolkingsaantal = 0;
            List<Land> landenLijst = landService.GeefLandenContinent(continent.Id);
            foreach(Land land in landenLijst)
            {
                bevolkingsaantal += land.Bevolkingsaantal;
            }
            List<string> landen = landService.GeefLandenContinent(continent.Id).Select(x => continentURL + $"/Land/{x.Id}").ToList();
            ContinentRESToutputDTO dto = new(continentURL, continent.Naam, bevolkingsaantal, landen.Count, landen);
            return dto;
        }

        internal static LandRESToutputDTO MapVanLandDomein(string url, Land land, StadService stadService)
        {
            string continentURL = $"{url}/Continent/{land.Continent.Id}";
            string landURL = $"{continentURL}/Land/{land.Id}";
            List<string> steden = stadService.GeefStedenLand(land.Id).Select(x => landURL + $"/Stad/{x.Id}").ToList();
            LandRESToutputDTO dto = new(landURL, land.Naam, land.Bevolkingsaantal, continentURL, steden.Count, steden);
            return dto;
        }
    }
}
