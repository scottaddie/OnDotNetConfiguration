# OnDotNetConfiguration

A sample ASP.NET Core 5.0 Blazor Server app to accompany the On .NET configuration/secrets management episode.

## Setup

### Local development machine

To run the project on your machine, complete the following steps:

1. Install the [.NET SDK 5.0](https://dotnet.microsoft.com/download/dotnet/5.0) or later.
1. Get a Yahoo Finance API key from [RapidAPI]( https://rapidapi.com/apidojo/api/yahoo-finance1/pricing).
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

### Azure resources

To run the project in Azure, complete the following steps:

1. Deploy the app to Azure App Service.
1. Provision an Azure SignalR Service instance with a **Service Mode** of **Default**.
1. Provision an Azure Key Vault instance.
1. In the Key Vault instance, navigate to **Settings** > **Secrets**. Create the following secrets by selecting **Generate/Import**.

	|Name								  |Value				|
	|-------------------------------------|---------------------|
	|`Azure--SignalR--ConnectionString`|The primary or secondary SignalR Service connection string found at **Settings** > **Keys**. For example, `Endpoint=https://blazorserverconfigsignalr.service.signalr.net;AccessKey=<ACCESS_KEY>;Version=1.0;`.|
	|`StockOptions--ApiKey`			  |The API key obtained from RapidAPI.|

1. Apply the following changes in App Service:
	1. In **Settings** > **Identity**, enable a System-assigned managed identity via the **Status** toggle button.
	1. In **Settings** > **Configuration** > **General settings**:
		1. In the **Stack settings** section:
			1. Select **.NET** from the **Stack** drop-down list.
			1. Select **.NET 5** from the **.NET version** drop-down list.
		1. In the **Platform settings** section, select **On** for the **Web sockets** and **ARR affinity** radio buttons.
	1. In **Settings** > **Configuration** > **Application settings**, select **New application setting**. Create a new environment variable named `KEYVAULT_ENDPOINT` whose value is the Key Vault resource's URI (e.g., https://blazorserverconfigvault.vault.azure.net/).
