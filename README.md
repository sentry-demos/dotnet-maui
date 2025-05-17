# .NET MAUI EmpowerPlant Sentry Sample

1. **Install the .NET SDK** with .NET MAUI support.
    - Download and install the latest .NET SDK from [Microsoft's .NET Download page](https://dotnet.microsoft.com/download).

2. Install platform-specific tools:
    - **For Android**: Install the Android SDK, Emulator, and necessary dependencies.
    - **For iOS**: Install Xcode on a Mac (required for iOS development).

3. **Set up the environment**:
    - Ensure `dotnet` is in your system PATH.
    - Install additional tools for .NET MAUI development:
      ```bash
      sudo dotnet workload install maui
      ```
4. **Upload debug symbols**
    The project is configured to [upload symbols on build automatically via MSBuild](https://docs.sentry.io/platforms/dotnet/guides/maui/configuration/msbuild/).  
    It requires an auth token set in the environment:  

    ```
    SENTRY_AUTH_TOKEN
    ```
    
    By default pointing to the Sentry `demo` org, on the `dotnet-maui` project.
    You can [override that directly on the `csproj` file](DotNetMaui.csproj#L45-L46),
    or set the following build properties (append them to the build command below):

    `-p:SentryOrg={org} -p:SentryProject={proj}`

5. **Run the app**

   Run the following command to create a new .NET MAUI app in **Debug** mode:
   ```bash
   dotnet build -t:Run -f net9.0-ios
   ```
   or for Android
   ```bash
   dotnet build -t:Run -f net9.0-android
   ```
