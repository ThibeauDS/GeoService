using System.Collections.Generic;

namespace RESTLaag.Model.Output
{
    public class LandRESToutputDTO
    {
        #region Properties
        public string Id { get; set; }
        public string Naam { get; set; }
        public int Bevolkingsaantal { get; set; }
        public decimal Oppervlakte { get; set; }
        public string ContinentId { get; set; }
        public int AantalSteden { get; set; }
        public List<string> Steden { get; set; }
        #endregion

        #region Constructors
        public LandRESToutputDTO(string id, string naam, int bevolkingsaantal, decimal oppervlakte, string continentId, int aantalSteden, List<string> steden)
        {
            Id = id;
            Naam = naam;
            Bevolkingsaantal = bevolkingsaantal;
            Oppervlakte = oppervlakte;
            ContinentId = continentId;
            AantalSteden = aantalSteden;
            Steden = steden;
        }
        #endregion
    }
}
