namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class EntityValidatorInfo
    {
        private string _entityValidator;
        private string _parentValidator;
        private string _xPathAfterParent;

        public EntityValidatorInfo(string entityValidator)
            : this(entityValidator, null) { }

        public EntityValidatorInfo(string entityValidator, string parentValidator)
        {
            _entityValidator = entityValidator;
            _parentValidator = parentValidator;

            if (parentValidator == null)
            {
                _xPathAfterParent = "";
            }
            else
            {
                switch (parentValidator)
                {
                    case "EiendomByggestedValidator":
                        _xPathAfterParent = "eiendomByggested{0}";
                        break;
                    case "TiltakshaverValidator":
                        _xPathAfterParent = "tiltakshaver";
                        break;
                    case "FakturamottakerValidator":
                        _xPathAfterParent = "fakturamottaker";
                        break;




                }
            }
        }


        public string EntityValidator { get => _entityValidator; set => _entityValidator = value;  }
        public string ParentValidator { get => _parentValidator; set => _parentValidator = value; }
        public string XPathAfterParent { get => _xPathAfterParent; set => _xPathAfterParent = value; }
    }
}
