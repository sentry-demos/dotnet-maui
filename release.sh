#!/bin/bash

dotnet build DotNetMaui.csproj -t:Build -f net9.0-ios -r iossimulator-x64
zip -r dotnet-maui-ios-x64.zip bin/Debug/net9.0-ios

dotnet publish DotNetMaui.csproj -f net9.0-android
cp bin/Release/net9.0-android/publish/io.sentry.dotnet.maui.empowerplant-Signed.apk dotnet-maui-android.apk
