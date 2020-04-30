namespace GpsYv.ManejadorDeMapa.Interfase
{
  partial class UserSettingsForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserSettingsForm));
      this.myPropertyGrid = new System.Windows.Forms.PropertyGrid();
      this.panel1 = new System.Windows.Forms.Panel();
      this.myCancelButton = new System.Windows.Forms.Button();
      this.mySaveButton = new System.Windows.Forms.Button();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // myPropertyGrid
      // 
      this.myPropertyGrid.AccessibleDescription = null;
      this.myPropertyGrid.AccessibleName = null;
      resources.ApplyResources(this.myPropertyGrid, "myPropertyGrid");
      this.myPropertyGrid.BackgroundImage = null;
      this.myPropertyGrid.Font = null;
      this.myPropertyGrid.Name = "myPropertyGrid";
      // 
      // panel1
      // 
      this.panel1.AccessibleDescription = null;
      this.panel1.AccessibleName = null;
      resources.ApplyResources(this.panel1, "panel1");
      this.panel1.BackgroundImage = null;
      this.panel1.Controls.Add(this.myCancelButton);
      this.panel1.Controls.Add(this.mySaveButton);
      this.panel1.Font = null;
      this.panel1.Name = "panel1";
      // 
      // myCancelButton
      // 
      this.myCancelButton.AccessibleDescription = null;
      this.myCancelButton.AccessibleName = null;
      resources.ApplyResources(this.myCancelButton, "myCancelButton");
      this.myCancelButton.BackgroundImage = null;
      this.myCancelButton.Font = null;
      this.myCancelButton.Name = "myCancelButton";
      this.myCancelButton.UseVisualStyleBackColor = true;
      this.myCancelButton.Click += new System.EventHandler(this.OnCancelButtonClick);
      // 
      // mySaveButton
      // 
      this.mySaveButton.AccessibleDescription = null;
      this.mySaveButton.AccessibleName = null;
      resources.ApplyResources(this.mySaveButton, "mySaveButton");
      this.mySaveButton.BackgroundImage = null;
      this.mySaveButton.Font = null;
      this.mySaveButton.Name = "mySaveButton";
      this.mySaveButton.UseVisualStyleBackColor = true;
      this.mySaveButton.Click += new System.EventHandler(this.OnSaveButtonClick);
      // 
      // UserSettingsForm
      // 
      this.AccessibleDescription = null;
      this.AccessibleName = null;
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackgroundImage = null;
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.myPropertyGrid);
      this.Font = null;
      this.Icon = null;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "UserSettingsForm";
      this.ShowInTaskbar = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
      this.Load += new System.EventHandler(this.OnLoad);
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.PropertyGrid myPropertyGrid;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button myCancelButton;
    private System.Windows.Forms.Button mySaveButton;
  }
}