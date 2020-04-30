[English](Configuration-for-Contributors.md)

**CodePlex** está basado en el Microsoft Team Foundation Server (TFS), por lo cual funciona mejor con **Visual Studio 2008** Standard, Professional, o cualquiera de las versiones del Team System (pero no funciona con Visual C# 2008 Express).  Si ya tienes instalado una de estas versiones de Visual Studio 2008 entonces:
* Instala el [Team Explorer 2008](http://www.codeplex.com/CodePlex/Wiki/View.aspx?titleObtaining%20the%20Team%20Explorer%20Client) y configúralo con la información de  [aquí](http://www.codeplex.com/GPSYVManejadorDeMapa/SourceControl/ListDownloadableCommits.aspx).
* Obtiene una licencia (es gratis para desarrolladores de Open Source) e instala [TestDriven.NET](http://testdriven.net/download.aspx).
* Instala [FxCop](http://www.microsoft.com/downloads/details.aspx?familyid3389F7E4-0E55-4A4D-BC74-4AEABB17997B&displaylangen).
* En Visual Studio ve a Tools->Options->Text Editor->All Languages->Tabs y pone el "Tab size" y el "Indent size" en 2, y selecciona "Insert Spaces".


Si no tienes acceso a una versión de Visual Studio 2008 compatible con TFS, entonces lo mas recomendable es usar **SharpDevelop**:
* Instala [SharpDevelop 3.0, FxCop y el .NET Framework 3.5](http://www.icsharpcode.net/OpenSource/SD/Download/#SharpDevelop30).
* Instala y configura [TortoiseSVN](http://www.codeplex.com/CodePlex/Wiki/View.aspx?titleUsing%20TortoiseSVN%20with%20CodePlex&referringTitleSource%20control%20clients).  Si usas Windows Vista entonces sigue las instrucciones de [aquí](http://iguanagears.blogspot.com/2008/02/getting-tortoisesvn-running-with.html).
* En SharpDevelop ve a Tools->Text Editor->Behavior y pone el "Tab size" y el "Indentation size" en 2, y selecciona "Convert Tabs to spaces".
* Para poder ejecutar las Pruebas Automáticas (Unit Test), en SharpDevelop ve a Tools->Options->Unit Test y de-selecciona la opción de "Shadow copy" y "Run tests on separate thread".
* Si eres un desarrollador o coordinador: Cuando arregles un Ítem de Trabajo (workitem) usa la información de [aquí](http://www.codeplex.com/SvnBridge/Wiki/View.aspx?title=Work%20Items%20Integration&referringTitle=Home) para subir tus cambios y que se asocien al Ítem de Trabajo.


Es también posible usar Visual C# 2008 Express, pero es mucho más limitado que la opción con SharpDevelop:
* Instala [Visual C# 2008 Express](http://www.microsoft.com/express/download/).
* Instala y configura [TortoiseSVN](http://www.codeplex.com/CodePlex/Wiki/View.aspx?titleUsing%20TortoiseSVN%20with%20CodePlex&referringTitleSource%20control%20clients). Si usas Windows Vista entonces sigue las instrucciones de [aquí](http://iguanagears.blogspot.com/2008/02/getting-tortoisesvn-running-with.html).
* En Visual C# ve a Tools->Options->Text Editor->All Languages->Tabs y pon el "Tab size" y el "Indent size" en 2, y selecciona "Insert Spaces".
* Para tener el control de versiones disponible desde el IDE puedes probar [esto](http://garrys-brain.blogspot.com/2007/07/tortoisesvn-and-visual-studio.html). **NOTA**: Yo lo probé con Visual Studio 2008 Professional y funcionó, pero no estoy seguro que funcione con Visual C# 2008 Express porque el autor tenía una versión específica para [Visual C++/C# 2005 Express](http___garrys-brain.blogspot.com_2006_10_tortoisesvn-vsnet-2005-good-stuff.html) y hay una sola versión para 2008.


Para ejecutar las pruebas automáticas de integración tienes que bajar la [Data para Pruebas de Integración](Data-para-Pruebas-de-Integración.md).