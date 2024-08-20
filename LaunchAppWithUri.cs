using System;
using System.Management;
using Microsoft.Maui.ApplicationModel;
using System.Management.Automation;


#if ANDROID
using Android.Content;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
#endif

using System.Diagnostics;

namespace UserAssistant;

public static class LaunchAppWithUri
{   
    static List<string> apps = new List<string>();
    static PowerShell powerShell = PowerShell.Create();
    static  LaunchAppWithUri()
    {
        #if WINDOWS
        ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Product");

        foreach (ManagementObject obj in searcher.Get())
        {
            apps.Add(obj["Name"].ToString());
        }
        #endif
    }


    public static async void LaunchApp(string appName, string activityName)
    {

        // var uri  = new Uri($"{appName}://open");
#if ANDROID
        var intent = new Intent(Intent.ActionMain);
        intent.SetComponent(new ComponentName(packageName, activityName));
        Platform.CurrentActivity.StartActivity(intent);
#endif

#if WINDOWS
        
        var app = apps.FirstOrDefault(a => a.ToLower().Contains(appName));
         //burda kaldın
        Process.Start(app);
#endif

        // var a = await Launcher.Default.OpenAsync(uri);

    }
}
