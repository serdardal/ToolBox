using ToolBox.Utils;

namespace ToolBox.Tools.Common
{
    public abstract class Tool
    {
        public string Name { get; private set; }
        public string SourcePath { get; private set; }
        public string LocalPath { get; private set; }

        public Tool(string name, string sourcePath, string localPath)
        {
            Name = name;
            SourcePath = sourcePath;
            LocalPath = localPath;
        }

        public bool DeleteLocalFiles()
        {
            Directory.Delete(LocalPath, true);
            return true;
        }

        public bool GetFiles()
        {
            if (!Directory.Exists(LocalPath))
            {
                FileHelper.CopyDirectory(SourcePath, LocalPath);
            }
            return true;
        }

        public bool CheckVersions()
        {
            var sourceVersion = FileHelper.GetFileKeyValueContent(@$"{SourcePath}\appInfo.txt")["version"];
            var localVersion = FileHelper.GetFileKeyValueContent(@$"{LocalPath}\appInfo.txt")["version"];

            if (sourceVersion != localVersion)
            {
                DeleteLocalFiles();
                GetFiles();
            }
            return true;
        }

        public void Run()
        {
            GetFiles();
            CheckVersions();
            Launch();
        }

        public abstract UserControl? GetUserControl();

        public abstract void Launch();
    }
}
