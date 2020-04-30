#region Copyright (c) Patricio Vidal (http://www.gpsyv.net)
// (For English scroll down.)
//
// GpsYv.ManejadorDeMapa es una aplicación para manejar Mapas de GPS en el
// formato Polish (.mp).  Esta escrito en C# usando el .NET Framework 3.5. 
//
// Esta programa nació por la necesidad del Grupo GPS de Venezuela, 
// GPS_YV (http://www.gpsyv.net), de analizar y corregir los mapas que el
// grupo genera para la comunidad.  GpsYv.ManejadorDeMapa se distribuye bajo 
// la licencia GPL con la finalidad de que sea útil para otros grupos o
// individuos que hacen mapas, y también para promover la colaboración 
// con este proyecto.
//
// Visita https://github.com/PatricioVidal/GpsMapTools para más información.
//
// La lógica de este programa se ha desarrollado con las ideas de los miembros
// del grupo GPS_YV. 
//
// Autor: Patricio Vidal.
//
// Este programa es software libre. Puede redistribuirlo y/o modificarlo
// bajo los términos de la Licencia Pública General de GNU según es publicada
// por la Free Software Foundation, bien de la versión 2 de dicha Licencia o 
// bien (según su elección) de cualquier versión posterior. 
//
// Este programa se distribuye con la esperanza de que sea útil, 
// pero SIN NINGUNA GARANTÍA, incluso sin la garantía MERCANTIL
// implícita o sin garantizar la CONVENIENCIA PARA UN PROPÓSITO PARTICULAR.
// Véase la Licencia Pública General de GNU para más detalles. 
//
// Debería haber recibido una copia de la Licencia Pública General 
// junto con este programa. Si no ha sido así, escriba a la 
// Free Software Foundation, Inc., en 675 Mass Ave, 
// Cambridge, MA 02139, EEUU.
//
//-----------------------------------------------------------------------------
//
// GpsYv.ManejadorDeMapa (GPS Map Manager) is an application to Manage 
// GPS Maps in Polish format (.mp).  It is written in C# using the 
// .NET Framework 3.5.
//
// This program was born by the need of the GPS Group of Venezuela,
// GPS_YV (http://www.gpsyv.net), to analyze and fix the maps that
// the group generates for the community. GpsYv.ManejadorDeMapa is 
// distributed under the GPL license with the purpose that it could 
// be useful for other groups or individuals that create maps, and 
// also to promote the collaboration with this project.
//
// Visit https://github.com/PatricioVidal/GpsMapTools for more information.
//
// The logic of this program has been develop with ideas of the members
// of the GPS_YV group.
//
// Author: Patricio Vidal.
//
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
#endregion

using System;

namespace GpsYv.ManejadorDeMapa.Vías
{
  /// <summary>
  /// Arregla las cosas  generales de lss Vías que se pueden arreglar automáticamente.
  /// </summary>
  /// <remarks>
  /// Las cosas que son arregladas son:
  ///   - Nombres de las Vías.
  /// </remarks>
  public class ArregladorGeneral : ProcesadorBase<ManejadorDeVías, Vía>
  {
    #region Campos
    private readonly Tipo miTipoCaminería = new Tipo("0x16");
    private readonly CampoParámetrosDeRuta miCampoParámetrosDeRutaDeCaminería = new CampoParámetrosDeRuta(
      new LímiteDeVelocidad(0),
      new ClaseDeRuta(0),
      new bool[] { false, false, true, true, true, true, true, false, false, true });
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Descripción de éste procesador.
    /// </summary>
    public static readonly string Descripción =
      "- Arregla los nombres de las Vías.\n" + 
      "- Arregla parametros de caminerías.";


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elManejadorDeVías">El manejador de Vías.</param>
    /// <param name="elEscuchadorDeEstatus">El escuchador de estatus.</param>
    public ArregladorGeneral(
      ManejadorDeVías elManejadorDeVías,
      IEscuchadorDeEstatus elEscuchadorDeEstatus)
      : base(elManejadorDeVías, elEscuchadorDeEstatus)
    {
    }
    #endregion

    #region Métodos Protegidos.
    /// <summary>
    /// Procesa una Vía.
    /// </summary>
    /// <param name="laVía">La Vía.</param>
    /// <returns>El número de problemas detectados al procesar el elemento.</returns>
    protected override int ProcesaElemento(Vía laVía)
    {
      int númeroDeProblemasDetectados = 0;

      númeroDeProblemasDetectados += ArreglaNombres(laVía);
      númeroDeProblemasDetectados += ArreglaCaminerías(laVía);
      númeroDeProblemasDetectados += ArreglaSentidos(laVía);

      return númeroDeProblemasDetectados;
    }


    private int ArreglaNombres(Vía laVía)
    {
      int númeroDeProblemasDetectados = 0;

      string nombreACorregir = laVía.Nombre;

      #region Cambia el nombre a mayúsculas
      string nombreCorregido = nombreACorregir.ToUpper();
      if (nombreCorregido != nombreACorregir)
      {
        laVía.ActualizaNombre(nombreCorregido, "M102: Cambiado a mayúsculas.");
        ++númeroDeProblemasDetectados;
        nombreACorregir = nombreCorregido;
      }
      #endregion

      // Remueve los espacios en blanco alrededor.
      nombreCorregido = nombreACorregir.Trim();

      #region Remueve espacios en blanco extra entre medio de las palabras.
      string[] palabras = nombreCorregido.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
      nombreCorregido = string.Join(" ", palabras);
      if (nombreCorregido != nombreACorregir)
      {
        laVía.ActualizaNombre(nombreCorregido, "M103: Eliminados espacios en blanco extra.");
        ++númeroDeProblemasDetectados;
        nombreACorregir = nombreCorregido;
      }
      #endregion

      #region Busca si el nombre de la vía comienza por un calificativo conocido.
      bool nombreComienzaConCalificativo = false;
      string calificativo = null;
      foreach (string posibleCalificativo in CalificativosDeVías.Calificativos)
      {
        if (nombreACorregir.StartsWith(posibleCalificativo))
        {
          nombreComienzaConCalificativo = true;
          calificativo = posibleCalificativo;
          break;
        }
      }
      #endregion

      #region Ve si el calificativo está al final y arreglalo
      bool nombreTerminaConCalificativo = false;
      if (!nombreComienzaConCalificativo)
      {
        foreach (string posibleCalificativo in CalificativosDeVías.Calificativos)
        {
          if (nombreACorregir.EndsWith(posibleCalificativo))
          {
            nombreTerminaConCalificativo = true;
            calificativo = posibleCalificativo;
            break;
          }
        }

        if (nombreTerminaConCalificativo)
        {
          nombreCorregido = nombreACorregir.Replace(' ' + calificativo, string.Empty);
          nombreCorregido = calificativo + ' ' + nombreCorregido;
          laVía.ActualizaNombre(nombreCorregido, "M104: Movido el calificativo al principio.");
          ++númeroDeProblemasDetectados;
        }
      }
      #endregion

      #region Genera el nombre secundario si tenemos un calificativo.
      if (calificativo != null)
      {
        // El calificativo está al principio.
        string nombreSinCalificativo = nombreCorregido.Replace(calificativo + ' ', string.Empty);
        string nombreSecundario = nombreSinCalificativo + ' ' + calificativo;
        laVía.ActualizaNombreSecundario(nombreSecundario, nombreSecundario);
      }
      #endregion

      return númeroDeProblemasDetectados;
    }
    

    private int ArreglaCaminerías(Vía laVía)
    {
      int númeroDeProblemasDetectados = 0;

      // Solo procesa caminerías.
      if (laVía.Tipo != miTipoCaminería)
      {
        return númeroDeProblemasDetectados;
      }

      if (laVía.CambiaCampoParámetrosDeRuta(
        miCampoParámetrosDeRutaDeCaminería,
        "M105: Cambiado a Parámetros de Caminería estándar."))
      {
        ++númeroDeProblemasDetectados;
      }

      return númeroDeProblemasDetectados;
    }


    private int ArreglaSentidos(Vía laVía)
    {
      int númeroDeProblemasDetectados = 0;

      string indicadorDeDirección = null;
      if (laVía.CampoIndicadorDeDirección != null)
      {
        indicadorDeDirección = laVía.CampoIndicadorDeDirección.Texto;
      }
      bool unSoloSentido = laVía.CampoParámetrosDeRuta.OtrosParámetros[CampoParámetrosDeRuta.IndiceUnSoloSentido];
      if ((indicadorDeDirección == null)&& unSoloSentido)
      {
       CampoGenérico campoIndicadorDeDirecciónDeUnSoloSentido = new CampoGenérico(Vía.IdentificadorIndicadorDeDirección, "1");
       laVía.CambiaCampoIndicadorDeDirección(
          campoIndicadorDeDirecciónDeUnSoloSentido,
          "M107: La vía no tiene Indicador de Dirección pero es de un solo sentido. Añadido el Indicadorde Dirrección con valor '1'.");
        ++númeroDeProblemasDetectados;
      }
      else if ((indicadorDeDirección == "1") && !unSoloSentido)
      {
        CampoParámetrosDeRuta campoParámetrosDeRuta = laVía.CampoParámetrosDeRuta;
        bool[] otrosParámetrosNuevos = (bool[])campoParámetrosDeRuta.OtrosParámetros.Clone();
        otrosParámetrosNuevos[CampoParámetrosDeRuta.IndiceUnSoloSentido] = true;
        CampoParámetrosDeRuta campoParámetrosDeRutaNuevo = new CampoParámetrosDeRuta(
          campoParámetrosDeRuta.LímiteDeVelocidad,
          campoParámetrosDeRuta.ClaseDeRuta,
          otrosParámetrosNuevos);
        laVía.CambiaCampoParámetrosDeRuta(
          campoParámetrosDeRutaNuevo,
          "M106: La vía tiene el Indicador de Dirección igual a '1' pero no es de un solo sentido. Cambiada a un solo sentido.");
        ++númeroDeProblemasDetectados;
      }
      else if ((indicadorDeDirección == "0") && unSoloSentido)
      {
        CampoGenérico campoIndicadorDeDirecciónDeUnSoloSentido = new CampoGenérico(Vía.IdentificadorIndicadorDeDirección, "1");
        laVía.CambiaCampoIndicadorDeDirección(
          campoIndicadorDeDirecciónDeUnSoloSentido,
          "M107: La vía tiene el Indicador de Dirección igual a '0' pero es de un solo sentido. Cambiado el Indicador de Dirección a valor '1'.");
        ++númeroDeProblemasDetectados;
      }

      return númeroDeProblemasDetectados;
    }


    /// <summary>
    /// Maneja el evento cuando hay un mapa nuevo.
    /// </summary>
    /// <param name="elEnviador">El objecto que envía el evento.</param>
    /// <param name="losArgumentos">Los argumentos del evento.</param>
    protected override void EnMapaNuevo(object elEnviador, EventArgs losArgumentos)
    {
      // No necesitamos hacer nada aquí.
    }


    /// <summary>
    /// Maneja el evento cuando hay elementos modificados en el mapa.
    /// </summary>
    /// <param name="elEnviador">El objecto que envía el evento.</param>
    /// <param name="losArgumentos">Los argumentos del evento.</param>
    protected override void EnElementosModificados(object elEnviador, EventArgs losArgumentos)
    {
      // No necesitamos hacer nada aquí.
    }
    #endregion
  }
}
