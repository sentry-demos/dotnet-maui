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
4. **Run the app**
   Run the following command to create a new .NET MAUI app:
   ```bash
   dotnet build -t:Run -f net9.0-ios
   ```
   or for Android
   ```bash
   dotnet build -t:Run -f net9.0-android
   ```
