using System.Diagnostics;
using System.Globalization;
using ToolBox.Utils;

namespace ToolBox
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");

            if (!Debugger.IsAttached)
                CheckSelfVersion();

            Application.Run(new MainForm());
        }

        static void CheckSelfVersion()
        {
            var sourceVersion = FileHelper.GetFileKeyValueContent(@$"{FileHelper.SELF_SHARED_PATH}\appInfo.txt")["version"];
            var localVersion = FileHelper.GetFileKeyValueContent(@$"{FileHelper.SELF_LOCAL_PATH}\appInfo.txt")["version"];

            if (sourceVersion != localVersion)
            {
                var result = MessageBox.Show("There is a newer version of ToolBox! Application will be updated, please click Ok to continue.", "New Version",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (result == DialogResult.OK)
                {
                    UpdateSelf();
                }
            }
        }

        static void UpdateSelf()
        {
            FileHelper.StartPowershellProcessAsAdmin(FileHelper.CURRENT_PATH, $"{FileHelper.UPDATER_PATH}");
            Environment.Exit(0);
        }
    }
}