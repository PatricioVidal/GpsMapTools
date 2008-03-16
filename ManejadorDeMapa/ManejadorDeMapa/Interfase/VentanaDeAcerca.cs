#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace GpsYv.ManejadorDeMapa.Interfase
{
  partial class VentanaDeAcerca : Form
  {
    public VentanaDeAcerca()
    {
      InitializeComponent();
      this.Text = String.Format("Acerca {0}", AssemblyTitle);
      this.labelProductName.Text = AssemblyProduct;
      this.labelVersion.Text = String.Format("Version {0}   ({1})", AssemblyVersion, FechaDelAssembly.ToString("MMM. d, yyyy"));
      this.labelCopyright.Text = AssemblyCopyright;
      this.labelCompanyName.Text = AssemblyCompany;
      this.textBoxDescription.Text = 
        "La búsqueda de palabras similares usa la librería "+
        "\"Ternary Search Tree Implementation for C#\" de Jonathan de Halleux (http://www.codeproject.com/KB/recipes/tst.aspx)\r\n\r\n";
    }

    #region Assembly Attribute Accessors

    public static string AssemblyTitle
    {
      get
      {
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
        if (attributes.Length > 0)
        {
          AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
          if (titleAttribute.Title != "")
          {
            return titleAttribute.Title;
          }
        }
        return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
      }
    }

    public string AssemblyVersion
    {
      get
      {
        return Assembly.GetExecutingAssembly().GetName().Version.ToString();
      }
    }


    public DateTime FechaDelAssembly
    {
      get
      {
        // Con el formato del Assembly usando Major.minor.* (ver AssemblyInfo.cs)
        // entonces el Build es automaticamente el número de días desde 1/1/2000.
        Version version = Assembly.GetExecutingAssembly().GetName().Version;
        int díasDesdeEnero1990 = version.Build;
        DateTime fecha = new DateTime(2000, 1, 1) + new TimeSpan(díasDesdeEnero1990, 0, 0, 0);
        return fecha;
      }
    }

    
    public static string AssemblyDescription
    {
      get
      {
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
        if (attributes.Length == 0)
        {
          return "";
        }
        return ((AssemblyDescriptionAttribute)attributes[0]).Description;
      }
    }

    public string AssemblyProduct
    {
      get
      {
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
        if (attributes.Length == 0)
        {
          return "";
        }
        return ((AssemblyProductAttribute)attributes[0]).Product;
      }
    }

    public string AssemblyCopyright
    {
      get
      {
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
        if (attributes.Length == 0)
        {
          return "";
        }
        return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
      }
    }

    public static string AssemblyCompany
    {
      get
      {
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
        if (attributes.Length == 0)
        {
          return "";
        }
        return ((AssemblyCompanyAttribute)attributes[0]).Company;
      }
    }
    #endregion
  }
}
