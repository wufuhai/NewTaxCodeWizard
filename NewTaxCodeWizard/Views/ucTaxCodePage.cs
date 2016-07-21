using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraEditors;
using NewTaxCodeWizard.ViewModels;
using System.Drawing;
using System.Windows.Forms;

namespace NewTaxCodeWizard
{
    public partial class UcTaxCodePage : Views.BaseWizardPage
    {
        public UcTaxCodePage()
        {
            InitializeComponent();
        }

        protected new TaxCodePageViewModel PageViewModel
        {
            get { return (TaxCodePageViewModel)WizardViewModel.CurrentPage; }
        }


        protected override void OnNavigatedTo(INavigationArgs args)
        {
            base.OnNavigatedTo(args);

            var taxCode = this.PageViewModel.TaxCode;
            lblTitle.Text = "Tax Code - " + taxCode.TaxCode;

            txtTaxDescription.Text = taxCode.TaxDescription.ToString();
            txtDescription.Text = taxCode.Description;

            PageViewModel.CheckIfTaxCodeExists();

            UpdateStatus();
        }

        private void UpdateStatus()
        {
            if (PageViewModel.Status == TaxCodeStatus.UpToDate)
            {
                lblStatus.Text = @"Congratulations, tax code is up to date!";
                lblStatus.AppearanceItemCaption.ForeColor = Color.LightSeaGreen;
            }
            else if (PageViewModel.Status == TaxCodeStatus.NeedToCreate)
            {
                btnUpdate.Text = "&Add Tax Code";
                lblStatus.Text = @"Tax code does not exist. Click “Add Code” to insert this tax code.";
                lblStatus.AppearanceItemCaption.ForeColor = Color.Red;
            }
            else if (PageViewModel.Status == TaxCodeStatus.NeedToUpdate)
            {
                btnUpdate.Text = "&Update Tax Code";
                lblStatus.Text =
                    string.Format(
                        @"A similar tax code “{0}” was created in your previous version to handle the same scenario. Since the new tax code “{1}” is now recommended by Royal Malaysian Customs Department, it is recommended to update this code from “{0}” to “{1}”. Click “Change Code” to update this tax code.",
                        PageViewModel.TaxCode.OldTaxCode, PageViewModel.TaxCode.TaxCode);

                lblStatus.AppearanceItemCaption.ForeColor = Color.Orange;
            }

            btnUpdate.Enabled = PageViewModel.AllowUpdate;
            btnSkip.Enabled = PageViewModel.AllowSkip;
        }

        private void btnUpdate_Click(object sender, System.EventArgs e)
        {
            var requiredAction = PageViewModel.Status;
            PageViewModel.AddOrUpdateTaxCode();

            if (requiredAction == TaxCodeStatus.NeedToCreate)
                XtraMessageBox.Show(string.Format("New Tax Code '{0}' has been created successfully!", PageViewModel.TaxCode.TaxCode));
            else if (requiredAction == TaxCodeStatus.NeedToUpdate)
                XtraMessageBox.Show(string.Format("Tax Code '{0}' has been changed to '{1}'!", PageViewModel.TaxCode.OldTaxCode, PageViewModel.TaxCode.TaxCode));

            UpdateStatus();
            WizardViewModel.PageCompleted();
        }

        private void btnSkip_Click(object sender, System.EventArgs e)
        {
            if (XtraMessageBox.Show("Do you want to skip this tax code?", "Skip Tax Code", MessageBoxButtons.YesNo) ==
                DialogResult.Yes)
            {
                PageViewModel.SkipTaxCode();
                WizardViewModel.PageCompleted();
            }
        }
    }
}