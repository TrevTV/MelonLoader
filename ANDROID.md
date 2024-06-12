Notes for running the WIP Android build of CoreCLR/new MelonLoader
Uses .NET 8.0.6 as it was the newest available at the time of writing.

Rough steps
1. Compile both `UnityAndroidProxy` and `MelonBootstrap` using the `cargo ndk` command. They should compile at the same time as they are part of the same workspace.
2. Copy the following files into the decompiled APK's arm64 library folder
   - `libmain.so` (compiled)
   - `libmelon_bootstrap.so` (compiled)
   - `libdobby.so` (available [here](https://github.com/RinLovesYou/dobby-sys/raw/master/dobby_libraries/android/arm64/libdobby.so))
   - `libnethost.so` (available inside [this](https://api.nuget.org/v3-flatcontainer/runtime.linux-bionic-arm64.microsoft.netcore.dotnetapphost/8.0.6/runtime.linux-bionic-arm64.microsoft.netcore.dotnetapphost.8.0.6.nupkg) NuGet package)
   - `libhostfxr.so` (available inside the dotnet runtime [here](https://dotnetcli.azureedge.net/dotnet/Runtime/8.0.6/dotnet-runtime-8.0.6-linux-bionic-arm64.tar.gz))
3. Create a folder named `MelonLoader` inside your app's `Android/data` folder.
4. Download the .NET runtime for Android [here](https://dotnetcli.azureedge.net/dotnet/Runtime/8.0.6/dotnet-runtime-8.0.6-linux-bionic-arm64.tar.gz) and extract it into `MelonLoader/Dependencies/dotnet` (creating each folder as needed).
5. Compile the MelonLoader solution and copy the resulting output into the `MelonLoader` folder.