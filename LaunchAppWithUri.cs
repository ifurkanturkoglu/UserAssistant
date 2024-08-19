using System;
using Microsoft.Maui.ApplicationModel;
namespace UserAssistant;

public static class LaunchAppWithUri
{
    public static async void LaunchApp(string appName){

        var uri  = new Uri($"{appName}://open");

        try
        {
           var a = await Launcher.Default.OpenAsync(uri);
           Console.WriteLine(a);
        }
        catch (Exception e)
        {
            
            Console.WriteLine(e);
        }
    }
}
