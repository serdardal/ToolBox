using ToolBox.Utils;

namespace ToolBox.Tools.Common
{
    public abstract class Tool
    {
        public string Name { get; private set; }
        public string SourcePath { get; private set; }
        public string LocalPath { get; private set; }

        private DirectoryCopier.ProgressChangeCallback UpdateProgressBarPercentage { get; set; }
        private DirectoryCopier.CompletedCallback RemoveProgressBar { get; set; }

        public Tool(string name, string sourcePath, string localPath,
            DirectoryCopier.ProgressChangeCallback updateProgressBarPercentage,
            DirectoryCopier.CompletedCallback removeProgressBar)
        {
            Name = name;
            SourcePath = sourcePath;
            LocalPath = localPath;
            UpdateProgressBarPercentage = updateProgressBarPercentage;
            RemoveProgressBar = removeProgressBar;
        }

        // every concrete tool implements its own logic for these methods.
        public abstract UserControl? GetUserControl();
        public abstract void Launch();

        public bool DeleteLocalFiles()
        {
            Directory.Delete(LocalPath, true);
            return true;
        }

        private void OnCopyCompleted() {
            RemoveProgressBar();
            CheckVersions();
        }

        public void GetFiles()
        {
            if (!Directory.Exists(LocalPath))
            {
                DirectoryCopier.Copy(SourcePath, LocalPath, UpdateProgressBarPercentage, OnCopyCompleted);
            }
            else
            {
                CheckVersions();
            }
        }

        public void CheckVersions()
        {
            var sourceVersion = FileHelper.GetFileKeyValueContent(@$"{SourcePath}\appInfo.txt")["version"];
            var localVersion = FileHelper.GetFileKeyValueContent(@$"{LocalPath}\appInfo.txt")["version"];

            if (sourceVersion != localVersion)
            {
                DeleteLocalFiles();
                GetFiles();
            }
            else {
                Launch();
            }
        }

        public void Run()
        {
            using (WaitCursor.BeginWaitCursorBlock())
            {
                GetFiles();
            }
        }
    }
}
