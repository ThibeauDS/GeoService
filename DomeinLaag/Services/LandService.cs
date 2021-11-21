using DomeinLaag.Exceptions;
using DomeinLaag.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeinLaag.Services
{
    public class LandService
    {
        #region Properties
        private readonly ILandRepository _repository;
        #endregion

        #region Constructors
        public LandService(ILandRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        public List<string> GeefLandenContinent(int id)
        {
            try
            {
                //TODO: ADO bestand aanmaken voor land
                return _repository.GeefLandenContinent(id);
            }
            catch (Exception ex)
            {
                throw new LandServiceException("GeefLandenContinent", ex);
            }
        }
        #endregion
    }
}
