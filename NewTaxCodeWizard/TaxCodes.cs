using DevExpress.Xpo;

namespace NewTaxCodeWizard
{
    public class TaxCodes : XPObject
    {
        public TaxCodes(Session session) : base(session)
        {
            
        }

        public  int TaxDescription { get; set; }
        public string TaxCode { get; set; }

        public string Description { get; set; }

    }
}
