using System.Collections.Generic;

namespace RESTLaag.Model.Output
{
    public class ContinentRESToutputDTO
    {
        #region Properties
        public string Id { get; set; }
        public string Name { get; set; }
        public int Bevolkingsaantal { get; set; }
        public List<string> Landen { get; set; }
        #endregion

        #region Constructors
        public ContinentRESToutputDTO(string id, string name, int bevolkingsaantal, List<string> landen)
        {
            Id = id;
            Name = name;
            Bevolkingsaantal = bevolkingsaantal;
            Landen = landen;
        } 
        #endregion
    }
}
