using DomeinLaag.Klassen;
using RESTLaag.Exceptions;
using RESTLaag.Model.Input;
using System;

namespace RESTLaag.Mappers
{
    public class MapNaarDomein
    {
        public static Continent MapNaarContinentDomein(ContinentRESTinputDTO dto)
        {
            try
            {
                Continent continent = new(dto.Naam);
                return continent;
            }
            catch (Exception ex)
            {
                throw new MapNaarDomeinException("MapNaarContinentDomein - error", ex);
            }
        }
    }
}
