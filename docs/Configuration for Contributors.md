[Español](Configuración-para-Colaboradores)

**CodePlex** is based on Microsoft Team Foundation Server (TFS), therefore it works better with  **Visual Studio 2008** Standard, Professional, or any of the Team System versions (but it does not work with Visual C# 2008 Express).  If you already have one of these Visual Studio 2008 version then:
* Install [Team Explorer 2008](http://www.codeplex.com/CodePlex/Wiki/View.aspx?titleObtaining%20the%20Team%20Explorer%20Client) and configure it with the information [here](http://www.codeplex.com/GPSYVManejadorDeMapa/SourceControl/ListDownloadableCommits.aspx).
* Get a license (it is free for Open Source developers) and install [TestDriven.NET](http://testdriven.net/download.aspx).
* Install [FxCop](http://www.microsoft.com/downloads/details.aspx?familyid3389F7E4-0E55-4A4D-BC74-4AEABB17997B&displaylangen).
* In Visual Studio go to Tools->Options->Text Editor->All Languages->Tabs and set the "Tab size" and "Indent size" to 2, and select "Insert Spaces".

If you don't have a TFS compatible Visual Studio 2008 version, then the best option is **SharpDevelop**:
* Install [SharpDevelop 3.0, FxCop and the .NET Framework 3.5](http://www.icsharpcode.net/OpenSource/SD/Download/#SharpDevelop30).
* Install and configure [TortoiseSVN](http://www.codeplex.com/CodePlex/Wiki/View.aspx?titleUsing%20TortoiseSVN%20with%20CodePlex&referringTitleSource%20control%20clients).  If you use Windows Vista then follow the instructions [here](http://iguanagears.blogspot.com/2008/02/getting-tortoisesvn-running-with.html).
* In SharpDevelop go to Tools->Text Editor->Behavior and set the "Tab size" and "Indentation size" to 2, and select "Convert Tabs to spaces".
* To execute the Unit Tests in SharpDevelop go to Tools->Options->Unit Test and deselect the option "Shadow copy" and "Run tests on separate thread".
* If you are a Developer or Coordinator: When you fix a Workitem use the information in [here](http://www.codeplex.com/SvnBridge/Wiki/View.aspx?title=Work%20Items%20Integration&referringTitle=Home) to upload your changes so that they are associated to the Workitem.

It is also possible to use Visual C# 2008 Express, but it is very limited compared to the SharpDevelop option:
* Install [Visual C# 2008 Express](http://www.microsoft.com/express/download/).
* Install and configure [TortoiseSVN](http://www.codeplex.com/CodePlex/Wiki/View.aspx?titleUsing%20TortoiseSVN%20with%20CodePlex&referringTitleSource%20control%20clients).  If you use Windows Vista then follow the instructions [here](http://iguanagears.blogspot.com/2008/02/getting-tortoisesvn-running-with.html).
* In Visual C# go to Tools->Options->Text Editor->All Languages->Tabs and set the "Tab size" and "Indent size" to 2, and select "Insert Spaces".
* To have the Source Control commands available within the IDE you can try [this](http://garrys-brain.blogspot.com/2007/07/tortoisesvn-and-visual-studio.html). **NOTA**: I tried and it worked in Visual Studio 2008 Professional, but I'm not sure that it will work with Visual C# 2008 Express because the author had an specific version for [Visual C++/C# 2005 Express](http://garrys-brain.blogspot.com/2006/10/tortoisesvn-vsnet-2005-good-stuff.html) and there is only one version for 2008.


To run the automated Integration Tests you have to download the [Data for Integration Tests](Data-for-Integration-Tests).