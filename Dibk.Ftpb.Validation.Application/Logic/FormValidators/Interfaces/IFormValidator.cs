using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Logic.FormValidators.Interfaces
{
    public interface IFormValidator
    {
        void Execute(string xmlData);
    }

    public interface IDataModelMapper<T, U>
    {
        T MapDataModelToFormEntity(U dataModel);
    }
}
