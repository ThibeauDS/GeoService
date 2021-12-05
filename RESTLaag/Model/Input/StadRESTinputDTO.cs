namespace RESTLaag.Model.Input
{
    public class StadRESTinputDTO
    {
        #region Properties
        public string Naam { get; set; }
        public int Bevolkingsaantal { get; set; }
        public bool IsHoofdStad { get; set; }
        public int ContinentId { get; set; }
        public int LandId { get; set; }
        #endregion
    }
}
