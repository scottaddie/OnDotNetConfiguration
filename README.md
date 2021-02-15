# OnDotNetConfiguration

A sample ASP.NET Core 5.0 Blazor Server app to accompany the On .NET configuration/secrets management episode.

## Setup

To run the project on your machine, complete the following steps:

1. Install the .NET SDK 5.0 or later.
1. Get a Yahoo Finance API key from https://rapidapi.com/apidojo/api/yahoo-finance1/pricing.
1. Store the API key as follows:
	1. If using Visual Studio, right-click the *BlazorServerConfiguration* project in **Solution Explorer** and select **Manage User Secrets**. Add the following snippet to the *secrets.json* file, replacing `<API_KEY>` with your API key. Save the file.
		
		```json
		{
		  "StockOptions": {
			"ApiKey": "<API_KEY>"
		  }
		}
		```
	1. If not using Visual Studio, replace `<API_KEY>` in the following command with your API key. Run the command from the directory containing the *BlazorServerConfiguration.csproj* file.

		```bash
		dotnet user-secrets set "StockOptions:ApiKey" "<API_KEY>"
		```