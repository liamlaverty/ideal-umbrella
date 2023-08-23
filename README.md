# ideal-umbrella
Messing around with Umbraco 


## Run the application 

- open a terminal 
- change into the site's directory `cd .\IdealUmbrella.site\`
- launch the site using `dotnet run`
 

## User secrets

To enable user secrets in dev, follow the guide: https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=windows#secret-manager

- Run `dotnet user-secrets init` to init the user secret feature in your dev env
- Set secrets with `dotnet user-secrets set "json:path:to:your:secrete:key" "value"`

### Secrets to set

`dotnet user-secrets set "MapboxConfig:Settings:FrontEndKey" "your-mapbox-api-key-here"`
