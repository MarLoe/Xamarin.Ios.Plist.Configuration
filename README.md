# Xamarin.Ios.Plist.Configuration

Merge `$(Configuration)` specific plist settings file into the "master" file

Example:
If you need debug specific entries in your Info.plist file,
then add the following to your iOS project file, below the Info.plist entry:
```
	<None Include="Info.Debug.plist">
		<DependentUpon>Info.plist</DependentUpon>
	</None>
```
The Info.Debug.plist file must be a normal Info.plist file with debug specific entires.
