﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources.Models;

namespace Dibk.Ftpb.Validation.Application.DataSources
{
    public class MunicipalityApiService
    {
        public MunicipalityViewModel GetMunicipality(string municipalityCode)
        {
            return new MunicipalityViewModel();
        }
    }
}