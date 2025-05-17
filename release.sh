#!/bin/bash

dotnet build EmpowerPlant.csproj -t:Build -f net9.0-ios -r iossimulator-x64
zip -r dotnet-maui-ios-x64.zip bin/Debug/net9.0-ios

dotnet publish EmpowerPlant.csproj -f net9.0-android
cp bin/Release/net9.0-android/publish/io.sentry.dotnet.maui.empowerplant-Signed.apk dotnet-maui-android.apk
