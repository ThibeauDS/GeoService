using DomeinLaag.Exceptions;
using DomeinLaag.Interfaces;
using DomeinLaag.Klassen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeinLaag.Services
{
    public class StadService
    {
        #region Properties
        private readonly IStadRepository _repository;
        #endregion

        #region Constructors
        public StadService(IStadRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods
        public List<Stad> GeefStedenLand(int id)
        {
            try
            {
                return _repository.GeefStedenLand(id);
            }
            catch (Exception ex)
            {
                throw new StadServiceException("GeefLandenContinent - error", ex);
            }
        }

        public bool HeeftSteden(int landId)
        {
            try
            {
                return _repository.HeeftSteden(landId);
            }
            catch (Exception ex)
            {
                throw new LandServiceException("HeeftSteden - error", ex);
            }
        }
        #endregion
    }
}
