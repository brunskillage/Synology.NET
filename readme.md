# Synology.NET C# API Client Assembly Project
Author  : Allan Brunskill  
Contact : abrunskill[at]yahoo.co.uk

>This is a complete C# .net 4.0 client implementation of the Synology DiskStation API found at https://www.google.co.uk/?q=synology%20api

However Synology chnaged everything in the last year or so and it broke. The tests now pass again and the client is working.

A nuget package is available at https://www.nuget.org/packages/Synology.NET which you can add by running the following command in the NuGet package console.

More extensive clients are available - good luck :)

```
Install-Package Synology.NET
```

## To use ...
Update your app.config or web.config so it contains the following keys set to your diskstations relevant values.
 ```xml
  <appSettings>
    <add key="Syno.User" value="[yourdiskstationusername]" />
    <add key="Syno.Pass" value="[yourdiskstatiionpassword]" />
    <add key="Syno.ApiBaseAddress" value="http://[yoursynologyhost]/webapi" />
  </appSettings>
```
**General flow is ...**  
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
## To run the NUnit Integration Tests 

Add the Syno.TestFolder key in app.config value to a directory of your DiskStation and run.
```xml
 <appSettings> 
     ...
    <add key="Syno.TestFolder" value="/public/apitest" />
    ...
</appSettings>
 ```
