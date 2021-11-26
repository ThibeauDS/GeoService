using System.Collections.Generic;

namespace RESTLaag.Model.Output
{
    public class ContinentRESToutputDTO
    {
        #region Properties
        public string Id { get; set; }
        public string Naam { get; set; }
        public int Bevolkingsaantal { get; set; }
        public int AantalLanden { get; set; }
        public List<string> Landen { get; set; }
        #endregion

        #region Constructors
        public ContinentRESToutputDTO(string id, string naam, int bevolkingsaantal, int aantalLanden, List<string> landen)
        {
            Id = id;
            Naam = naam;
            Bevolkingsaantal = bevolkingsaantal;
            AantalLanden = aantalLanden;
            Landen = landen;
        } 
        #endregion
    }
}
