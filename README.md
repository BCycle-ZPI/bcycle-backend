Backend for BCycle
==================

.NET Core SDK 2.2.6 is required to build and run the app.

Creating the database
---------------------

1. Install the SDK
2. Change into the project directory (that contains `Startup.cs` and `bcycle-backend.csproj`)
3. Run `dotnet tool install --global dotnet-ef --version 2.2.6`
4. Run `dotnet ef database update`

If `dotnet ef` fails to run and complains about a missing path, go to ~/.dotnet/tools/.store/dotnet-ef, and copy `2.2.6` to the version requested by the tool (`2.2.6-servicing-10079`), then go into the new directory, into `dotnet-ef`, and also make a copy with the full version number.


Firebase API key
----------------

1. Generate an API key for the service account [here](https://console.firebase.google.com/u/0/project/bcycle-6d8e7/settings/serviceaccounts/adminsdk).
2. Place it in the project directory (that contains `Startup.cs`) and name it `bcycle-credentials.json`
3. Make sure you don’t commit that file to git!
