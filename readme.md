# Synology.NET C# API Client Assembly Project ####
Author  : Allan Brunskill  
Contact : abrunskill[at]yahoo.co.uk

>This is a complete C# .net 4.0 client implementation of the Synology DiskStation API found at https://www.google.co.uk/?q=synology%20api based on Synology_File_Station_API_Guide.pdf.

A nuget package is available at https://www.nuget.org/packages/Synology.NET which you can add by running the following command in the NuGet package console or via the GUI online seach within Visual Studio.
```
Install-Package Synology.NET
```

## To use...
Ensure your app.config or web.config has the following keys set to your diskstations relevant values.
 ```xml
  <appSettings>
    <add key="Syno.User" value="[yourdiskstationusername]" />
    <add key="Syno.Pass" value="[yourdiskstatiionpassword]" />
    <add key="Syno.ApiBaseAddress" value="http://[yoursynologyhost]/webapi" />
  </appSettings>
```
### General flow is ...
 - Create a SynologySession 
 - Create an instance of the API using the session that has just been created
 - Make your calls to the diskstation using the API methods, passing in any relevant parameters 
 - Log out of the session

```c#
  var session = new SynologySession(new AppSettingsClientConfig());
  session.Login();
  
  var api = new SynologyApi(session);
  
  // var resp = api.SomeMethod(parameters);
  // .... do something with resp
  
  session.LogOut();
```
## To run the Nuunit Integration Tests 

Add the Syno.TestFolder key in app.config value to a directory of your DiskStation and run.
```xml
 <appSettings> 
     ...
    <add key="Syno.TestFolder" value="/public/apitest" />
    ...
</appSettings>
 ```