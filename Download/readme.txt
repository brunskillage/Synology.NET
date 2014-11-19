Synology.NET C# API Client Assembly Project
Author  : Allan Brunskill
Contact : abrunskill[at]yahoo.co.uk
======================
This is a complete C# Client implementation of the Synology DiskStation API found at https://www.google.co.uk/?q=synology%20api
It is also in source for reference as Synology_File_Station_API_Guide.pdf.

The Synology.dll is the assembly required which depends on Restsharp.dll from https://github.com/restsharp/RestSharp

To Use
--------------------

Package is available on NUGet

https://www.nuget.org/packages/Synology.NET/

Ensure your app.config or web.config has the following keys set to your relevant settings.

  <appSettings>
  ...
    <add key="Syno.User" value="yourdiskstationusername" />
    <add key="Syno.Pass" value="yourdiskstatiionpassword" />
    <add key="Syno.ApiBaseAddress" value="http://[yoursynology]/webapi" />
  </appSettings>

General flow is login and create SynologySession. 
This gets a key for subsequent method calls. 
Then LogOut.

  var session = new SynologySession(new AppSettingsClientConfig());
  session.Login();
  
  var api = new SynologyApi(session);
  
  // var resp = api.Method(parameters);
  // do something with resp
  
  session.LogOut();
  
Manual Usage

Copy dlls in Downloads folder to your app and add them as references.

To run the nunit integration tests 
----------------------------------
Set the Syno.TestFolder in app.config value to a directory of your DiskStation and run.

  <appSettings>
    <add key="Syno.TestFolder" value="/public/apitest" />
  </appSettings>

