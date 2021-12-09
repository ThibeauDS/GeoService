using DomeinLaag.Klassen;
using DomeinLaag.Services;
using RESTLaag.Exceptions;
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
            try
            {
                string continentURL = $"{url}/Continent/{continent.Id}";
                int bevolkingsaantal = 0;
                List<Land> landenLijst = landService.GeefLandenContinent(continent.Id);
                foreach (Land land in landenLijst)
                {
                    bevolkingsaantal += land.Bevolkingsaantal;
                }
                List<string> landen = landenLijst.Select(x => continentURL + $"/Land/{x.Id}").ToList();
                ContinentRESToutputDTO dto = new(continentURL, continent.Naam, bevolkingsaantal, landen.Count, landen);
                return dto;
            }
            catch (Exception ex)
            {
                throw new MapVanDomeinException("MapVanContinentDomein - error", ex);
            }
        }

        public static LandRESToutputDTO MapVanLandDomein(string url, Land land, StadService stadService)
        {
            try
            {
                string continentURL = $"{url}/Continent/{land.Continent.Id}";
                string landURL = $"{continentURL}/Land/{land.Id}";
                List<string> steden = stadService.GeefStedenLand(land.Id).Select(x => landURL + $"/Stad/{x.Id}").ToList();
                LandRESToutputDTO dto = new(landURL, land.Naam, land.Bevolkingsaantal, land.Oppervlakte, continentURL, steden.Count, steden);
                return dto;
            }
            catch (Exception ex)
            {
                throw new MapVanDomeinException("MapVanLandDomein - error", ex);
            }
        }

        public static StadRESToutputDTO MapVanStadDomein(string url, Stad stad)
        {
            try
            {
                string continentURL = $"{url}/Continent/{stad.Land.Continent.Id}";
                string landURL = $"{continentURL}/Land/{stad.Land.Id}";
                string stadURL = $"{landURL}/Stad/{stad.Id}";
                StadRESToutputDTO dto = new(stadURL, stad.Naam, stad.Bevolkingsaantal, stad.IsHoofdstad, continentURL, landURL);
                return dto;
            }
            catch (Exception ex)
            {
                throw new MapVanDomeinException("MapVanStadDomein - error", ex);
            }
        }
    }
}
