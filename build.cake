var target = Argument("Target", "Default");
Task("Default")
  .IsDependentOn("HotReload")
  .Does(() =>
  {

  });

Task("HotReload")
    .Does(() => {
        StartProcess("adb", "forward tcp:4290 tcp:4290");

        var urls = "u=http://127.0.0.1:4290,http://127.0.0.1:4291";
        var windowsProcess = "./tools/Xamarin.Forms.HotReload.Observer.exe";
        var processName = IsRunningOnWindows() ? windowsProcess : "mono";
        
        var processSettings = new ProcessSettings()
        {
            Arguments = IsRunningOnWindows() ? urls : $"{windowsProcess} {urls}"
        };

        using (var hotReloadProcess = StartAndReturnProcess(processName, processSettings))
        {
            hotReloadProcess.WaitForExit();
        }
    });

RunTarget(target);
