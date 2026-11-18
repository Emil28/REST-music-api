# Deployment and Postman setup

The API and frontend are separate repositories. The workflows run when their current database branches are pushed:

- API: `Database`
- Frontend: `database`

## API on Azure App Service

Add these repository settings in `Emil28/REST-music-api`:

- Variable `AZURE_WEBAPP_NAME`: the API App Service name.
- Secret `AZURE_WEBAPP_PUBLISH_PROFILE`: the publish profile downloaded from that App Service.

Configure the API App Service with these settings. Keep the values in Azure, not in Git:

- `ConnectionStrings__MusicRecords`: the Azure SQL connection string.
- `Jwt__Key`: a long, random signing key.
- `Jwt__Issuer`: the issuer used when creating JWTs.
- `Jwt__Audience`: the audience used when creating JWTs.

The workflow restores and tests the solution, publishes the API, and deploys it to the App Service.

## Frontend on Azure App Service

Add these settings in `Neozt001/RestMusicFrontEnd`:

- Variable `MUSIC_RECORDS_API_BASE_URL`: the API origin, for example `https://<api-app-name>.azurewebsites.net`, without `/api` or a trailing slash.
- Secrets `AZURE_FTP_SERVER`, `AZURE_FTP_USERNAME`, and `AZURE_FTP_PASSWORD`: the App Service FTPS publishing credentials.

Enable FTPS publishing for the frontend App Service. The workflow writes the API origin into `api-config.js` during deployment and uploads the frontend to `/site/wwwroot/`.

## Postman

Import `postman/MusicRecords.postman_collection.json` into Postman. Set `baseUrl` to `http://localhost:5209` for local testing or the API App Service origin for Azure testing. Set `username` and `password` in a private Postman environment, then run the collection in order. It tests login, an unauthenticated 401, list and title filtering, and create/update/delete with a temporary record.
