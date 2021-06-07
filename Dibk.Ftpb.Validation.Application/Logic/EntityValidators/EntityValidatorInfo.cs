namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class EntityValidatorInfo
    {
        private string _entityValidator;
        private string _parentValidator;
        private string _parentXPath;

        //public EntityValidatorInfo(string entityValidator, string parentValidator)
        //{
        //    _entityValidator = entityValidator;
        //    _parentValidator = parentValidator;

        //    if (entityValidator.Equals("EiendomByggestedValidator") && (parentValidator != null))
        //    {
        //        if (parentValidator.Equals(""))
        //        {
        //            _parentXPath = 
        //        }
        //    }



        //}


        public string EntityValidator { get => _entityValidator; set => _entityValidator = value;  }
        public string ParentValidator { get => _parentValidator; set => _parentValidator = value; }
        public string ParentXPath { get => _parentXPath; set => _parentXPath = value; }
    }
}
