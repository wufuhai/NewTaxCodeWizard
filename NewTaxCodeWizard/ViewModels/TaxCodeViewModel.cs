namespace NewTaxCodeWizard.ViewModels
{
    public class TaxCodeData
    {
        public string Version { get; set; }
        public TaxCodeViewModel[] TaxCodes { get; set; }
    }
    public class TaxCodeViewModel
    {
        public  bool IsOutputTax { get; set; }
        public  string TaxCode { get; set; }
        public  string OldTaxCode { get; set; }
        public int TaxDescription { get; set; }

        public string Description { get; set; }
    }
}