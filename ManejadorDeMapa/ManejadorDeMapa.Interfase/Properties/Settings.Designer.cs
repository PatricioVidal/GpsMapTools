﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4952
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GpsYv.ManejadorDeMapa.Interfase.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("3")]
        public int DistanciaMáximaEnDecenasDeMetrosParaBuscarPdisDuplicados {
            get {
                return ((int)(this["DistanciaMáximaEnDecenasDeMetrosParaBuscarPdisDuplicados"]));
            }
            set {
                this["DistanciaMáximaEnDecenasDeMetrosParaBuscarPdisDuplicados"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("3")]
        public int DistanciaHammingBuscarPdisDuplicados {
            get {
                return ((int)(this["DistanciaHammingBuscarPdisDuplicados"]));
            }
            set {
                this["DistanciaHammingBuscarPdisDuplicados"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("-32000, -32000")]
        public global::System.Drawing.Point PosiciónDeLaFormaPrincipal {
            get {
                return ((global::System.Drawing.Point)(this["PosiciónDeLaFormaPrincipal"]));
            }
            set {
                this["PosiciónDeLaFormaPrincipal"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0, 0")]
        public global::System.Drawing.Size TamañoDeLaFormaPrincipal {
            get {
                return ((global::System.Drawing.Size)(this["TamañoDeLaFormaPrincipal"]));
            }
            set {
                this["TamañoDeLaFormaPrincipal"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool RequireActualizarOpcionesDelUsuario {
            get {
                return ((bool)(this["RequireActualizarOpcionesDelUsuario"]));
            }
            set {
                this["RequireActualizarOpcionesDelUsuario"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("3")]
        public int DistanciaMáximaBuscarPosiblesNodosDesconectados {
            get {
                return ((int)(this["DistanciaMáximaBuscarPosiblesNodosDesconectados"]));
            }
            set {
                this["DistanciaMáximaBuscarPosiblesNodosDesconectados"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool EstáMaximizada {
            get {
                return ((bool)(this["EstáMaximizada"]));
            }
            set {
                this["EstáMaximizada"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string ArchivoDeLímites {
            get {
                return ((string)(this["ArchivoDeLímites"]));
            }
            set {
                this["ArchivoDeLímites"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string Cultura {
            get {
                return ((string)(this["Cultura"]));
            }
            set {
                this["Cultura"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("100")]
        public double MinimumDistanceToMatchRoads {
            get {
                return ((double)(this["MinimumDistanceToMatchRoads"]));
            }
            set {
                this["MinimumDistanceToMatchRoads"] = value;
            }
        }
    }
}
