using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraEditors;

namespace NewTaxCodeWizard.Views
{
    public partial class BaseWizardPage : XtraUserControl, ISupportNavigation
    {
        public BaseWizardPage()
        {
            InitializeComponent();
        }
        protected IWizardPageViewModel PageViewModel
        {
            get { return WizardViewModel.CurrentPage; }
        }
        protected IWizardViewModel WizardViewModel
        {
            get { return wizardViewModel; }
        }

        protected virtual void OnNavigatedFrom(INavigationArgs args)
        {
            
        }

        protected virtual void OnNavigatedTo(INavigationArgs args)
        {
            wizardViewModel = args.Parameter as IWizardViewModel;
        }
        #region ISupportNavigation Members
        IWizardViewModel wizardViewModel;
        void ISupportNavigation.OnNavigatedTo(INavigationArgs args)
        {
            OnNavigatedTo(args);
        }
        void ISupportNavigation.OnNavigatedFrom(INavigationArgs args)
        {
            OnNavigatedFrom(args);
        }
        #endregion
    }
}