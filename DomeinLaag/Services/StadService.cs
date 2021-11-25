using DomeinLaag.Interfaces;
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
        #endregion
    }
}
