namespace RESTLaag.Model.Input
{
    public class ContinentRESTinputDTO
    {
        #region Properties
        public string Naam { get; set; }
        #endregion

        #region Constructors
        public ContinentRESTinputDTO(string naam)
        {
            Naam = naam;
        }
        #endregion
    }
}
