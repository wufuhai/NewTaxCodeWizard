namespace NewTaxCodeWizard.ViewModels
{
    class TaxCodePageViewModel : IWizardPageViewModel
    {
        private bool taxCodeExists;
        public bool CanReturn
        {
            get
            {
               return  true;
            }
        }

        public bool IsComplete
        {
            get
            {
                return ShouldSkip || taxCodeExists;
            }
        }

        public  bool ShouldSkip { get; set; }

        public void SkipTaxCode()
        {
            ShouldSkip = true;
        }

        public void CheckIfTaxCodeExists()
        {
            
        }
    }
}
