namespace NewTaxCodeWizard.ViewModels
{
    public class TaxCodeData
    {
        public string Version { get; set; }
        public TaxCodeViewModel[] TaxCodes { get; set; }
    }
    public class TaxCodeViewModel
    {
        public  string TaxCode { get; set; }
        public  string OldTaxCode { get; set; }

        public  int TaxDescEnumValue { get; set; }
        public string TaxDescription { get; set; }

        public string Description { get; set; }
    }
}