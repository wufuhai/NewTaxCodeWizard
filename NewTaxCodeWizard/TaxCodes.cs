using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo;

namespace NewTaxCodeWizard
{
    public class TaxCodes : XPObject
    {
        public TaxCodes(Session session) : base(session)
        {
            
        }

        public string TaxCode { get; set; }

        public string Description { get; set; }

    }
}
