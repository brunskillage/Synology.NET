Synology.NET 
Author Allan Brunskill
======================
This is a complete C# Client implementation of the Synology DiskStation API found at https://www.google.co.uk/?q=synology%20api
It is also in source for reference.


To run the tests 
--------------------
set the Syno.TestFolder in app.config value to a directory of your DiskStation and run.

  <appSettings>
    <add key="Syno.TestFolder" value="/public/apitest" />
  </appSettings>

To Use
--------------------
Ensure your app has the following keys set to your relevant settings.

  <appSettings>
  ...
    <add key="Syno.User" value="yourdiskstationusername" />
    <add key="Syno.Pass" value="yourdiskstatiionpassword" />
    <add key="Syno.ApiBaseAddress" value="http://[yoursynology]/webapi" />
  </appSettings>

General flow is login and create SynologySession. This gets a key for subsequent method calls. Then LogOut

  var session = new SynologySession(new AppSettingsClientConfig());
  session.Login();
  
  var api = new SynologyApi(session);
  
  // var resp = api.Method(parameters);
  // do something with resp
  
  session.LogOut();