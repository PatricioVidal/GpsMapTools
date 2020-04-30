using System;
using System.Windows.Forms;
using GpsYv.ManejadorDeMapa.Interfase.Properties;

namespace GpsYv.ManejadorDeMapa.Interfase
{
  public partial class UserSettingsForm : Form
  {
    private Settings myUserSettings;

    public UserSettingsForm()
    {
      InitializeComponent();
    }
    private void OnLoad(object sender, EventArgs e)
    {
      myUserSettings = Settings.Default;
      myPropertyGrid.SelectedObject = myUserSettings;
      myPropertyGrid.PropertySort = PropertySort.Alphabetical;
    }

    private void OnSaveButtonClick(object sender, EventArgs e)
    {
      myUserSettings.Save();
      Close();
    }

    private void OnCancelButtonClick(object sender, EventArgs e)
    {
      myUserSettings.Reload();
      Close();
    }
  }
}
