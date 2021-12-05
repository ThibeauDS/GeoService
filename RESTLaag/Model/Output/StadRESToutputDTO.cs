namespace RESTLaag.Model.Output
{
    public class StadRESToutputDTO
    {
        #region Properties
        public string Id { get; set; }
        public string Naam { get; set; }
        public int Bevolkingsaantal { get; set; }
        public bool IsHoodsstad { get; set; }
        public string ContinentId { get; set; }
        public string LandId { get; set; }
        #endregion

        #region Constructors
        public StadRESToutputDTO(string id, string naam, int bevolkingsaantal, bool isHoodsstad, string continentId, string landId)
        {
            Id = id;
            Naam = naam;
            Bevolkingsaantal = bevolkingsaantal;
            IsHoodsstad = isHoodsstad;
            ContinentId = continentId;
            LandId = landId;
        }        
        #endregion
    }
}
