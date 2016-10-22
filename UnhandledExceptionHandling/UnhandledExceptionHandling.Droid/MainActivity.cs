using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using System.IO;

namespace UnhandledExceptionHandling.Droid
{
    [Activity(Label = "UnhandledExceptionHandling", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
        private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs)
        {
            var exception = new Exception("TaskSchedulerOnUnobservedTaskException", unobservedTaskExceptionEventArgs.Exception);
            ProcessUnhandledException(exception);
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            var exception = new Exception("CurrentDomainOnUnhandledException", unhandledExceptionEventArgs.ExceptionObject as Exception);
            ProcessUnhandledException(exception);
        }
        internal static void ProcessUnhandledException(Exception exception)
        {
            try
            {
                const string errorFileName = "unhandledexception.log";
                var localStoragePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); 
                var logFilePath = Path.Combine(localStoragePath, errorFileName);
                var logMessage = String.Format("Time: {0}\r\nError: Unhandled Exception\r\n{1}",
                DateTime.Now, exception.ToString());
                File.WriteAllText(logFilePath, logMessage);
                Android.Util.Log.Error("UnhandledExceptionHandling", "unhandled exception", logMessage);
            }
            catch
            {
                Android.Util.Log.Error("UnhandledExceptionHandling", "unhandled exception catch");
            }
        }
    }
}

