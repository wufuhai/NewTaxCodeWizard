using System;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;

namespace NewTaxCodeWizard
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SkinManager.EnableFormSkins();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Office 2013");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            XpoDefault.DataLayer = XpoDefault.GetDataLayer(InMemoryDataStore.GetConnectionStringInMemory(false), AutoCreateOption.DatabaseAndSchema);
            Application.Run(new MainForm());
        }
    }
}
