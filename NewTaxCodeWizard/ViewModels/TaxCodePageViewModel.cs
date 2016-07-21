using DevExpress.Data.Filtering;
using DevExpress.Xpo;

namespace NewTaxCodeWizard.ViewModels
{
    public class TaxCodePageViewModel : IWizardPageViewModel
    {
        private TaxCodeViewModel taxCode;
        private bool completed;
        private TaxCodeStatus status;

        public TaxCodePageViewModel(TaxCodeViewModel taxCode)
        {
            this.taxCode = taxCode;
        }

        public bool AllowUpdate { get; set; }

        public bool AllowSkip { get; set; }

        public bool CanReturn
        {
            get { return true; }
        }

        public bool IsComplete
        {
            get { return completed; }
        }

        public TaxCodeViewModel TaxCode
        {
            get { return taxCode; }
        }

        public void SkipTaxCode()
        {
            completed = true;
        }


        public TaxCodeStatus Status
        {
            get { return status; }
        }
        //public string Status
        //{
        //    get { return status; }
        //}

        public void CheckIfTaxCodeExists()
        {
            using (var uow = new UnitOfWork())
            {
                var tcFromDb = uow.FindObject<TaxCodes>(new BinaryOperator("TaxCode", taxCode.TaxCode));

                // Tax code already exists
                if (tcFromDb != null)
                {
                    AllowUpdate = false;
                    AllowSkip = false;
                    completed = true;
                    status = TaxCodeStatus.UpToDate;
                    return;
                }

                //Check if there is an existing tax code with different code but handle the same scenario
                if (!string.IsNullOrEmpty(taxCode.OldTaxCode))
                {
                    tcFromDb = uow.FindObject<TaxCodes>(new BinaryOperator("TaxCode", taxCode.TaxCode));

                    // If a similar tax code is found, system will suggest user to change the code to the new code instead of creating a new one.
                    if (tcFromDb != null)
                    {
                        status = TaxCodeStatus.NeedToUpdate;

                        AllowUpdate = true;
                        AllowSkip = true;
                        return;
                    }
                }

                status = TaxCodeStatus.NeedToCreate;

                AllowUpdate = false;
                AllowSkip = true;
            }
        }

        public void AddOrUpdateTaxCode()
        {
            AddOrUpdateTaxCodeCore();

            AllowUpdate = false;
            AllowSkip = false;
            completed = true;
            status = TaxCodeStatus.UpToDate;
        }

        protected void AddOrUpdateTaxCodeCore()
        {
            using (var uow = new UnitOfWork())
            {
                TaxCodes tc = null;
                if (!string.IsNullOrEmpty(this.taxCode.OldTaxCode))
                    tc = uow.FindObject<TaxCodes>(new BinaryOperator("TaxCode", taxCode.TaxCode));

                // If tax code already exists, return
                if (tc != null)
                    return;

                if (!string.IsNullOrEmpty(this.taxCode.OldTaxCode))
                {
                    tc = uow.FindObject<TaxCodes>(new BinaryOperator("TaxCode", taxCode.OldTaxCode));

                    // Update existing tax code
                    if (tc != null)
                    {
                        tc.TaxCode = taxCode.TaxCode;
                        uow.CommitChanges();

                        return;
                    }
                }

                tc = new TaxCodes(uow);
                tc.TaxCode = taxCode.TaxCode;
                tc.Description = taxCode.Description.Substring(0, 100);
                uow.CommitChanges();
            }
        }
    }

    public enum TaxCodeStatus
    {
        UpToDate, NeedToCreate, NeedToUpdate
    }
}
