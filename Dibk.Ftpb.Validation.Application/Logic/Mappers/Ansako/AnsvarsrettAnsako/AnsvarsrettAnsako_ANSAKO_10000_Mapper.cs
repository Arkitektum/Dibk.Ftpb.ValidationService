using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Models.FormEntities.Ansako;
using no.kxml.skjema.dibk.ansvarsrettAnsako;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.Ansako.AnsvarsrettAnsako
{
    public class AnsvarsrettAnsako_ANSAKO_10000_Mapper
    {
        public AnsvarsrettAnsako_ANSAKO_10000_Form GetFormEntity(ErklaeringAnsvarsrettType erklaeringAnsvarsrett)
        {
            var noko = new AnsvarsrettAnsako_ANSAKO_10000_Form()
            {
                AnsvarligSoekerValidationEntity = AktoerMapper.MAP(erklaeringAnsvarsrett.ansvarligSoeker)
            };
            
            return noko;
        }
    }
}
