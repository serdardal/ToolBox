using System.Diagnostics;

namespace ToolBox.Utils
{
    public static class FileHelper
    {
        public static readonly string CURRENT_PATH = Environment.CurrentDirectory;
        public static readonly string TOOLS_SHARED_BASE_PATH = @$"{CURRENT_PATH}\..\..\..\..\SomeSharedToolsFolder";
        public static readonly string TOOLS_LOCAL_BASE_PATH = @$"{CURRENT_PATH}\Tools";

        public static void StartPowershellProcessAsAdmin(string workingDirectory, string command)
        {
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = "powershell.exe";
            startInfo.Arguments = @$"-ExecutionPolicy Unrestricted -command ""Start-Process PowerShell -ArgumentList {"{"}-NoProfile -ExecutionPolicy Unrestricted -NoExit cd '{workingDirectory}'; {command}{"}"} -Verb RunAs""";
            Process.Start(startInfo);
        }

        public static Dictionary<string, string> GetFileKeyValueContent(string path)
        {
            var dict = new Dictionary<string, string>();
            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line)) { continue; }

                var trimmedLine = line.Trim();

                var equalIndex = trimmedLine.IndexOf('=');
                dict.Add(
                    key: trimmedLine.Substring(0, equalIndex),
                    value: trimmedLine.Substring(equalIndex + 1, trimmedLine.Length - equalIndex - 1)
                );
            }

            return dict;
        }
    }
}
