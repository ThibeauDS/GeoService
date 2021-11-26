﻿using DomeinLaag.Klassen;
using DomeinLaag.Services;
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

        public static Land MapNaarLandDomein(LandRESTinputDTO dto, ContinentService continentService)
        {
            try
            {
                Continent continent = continentService.ContinentWeergeven(dto.ContinentId);
                Land land = new(dto.Naam, dto.Bevolkingsaantal, dto.Oppervlakte, continent);
                return land;
            }
            catch (Exception ex)
            {
                throw new MapNaarDomeinException("MapNaarLandDomein - error", ex);
            }
        }
    }
}
