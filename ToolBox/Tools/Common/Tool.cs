using ToolBox.Utils;

namespace ToolBox.Tools.Common
{
    public abstract class Tool
    {
        public string Name { get; private set; }
        public string SourcePath { get; private set; }
        public string LocalPath { get; private set; }

        private DirectoryCopier.ProgressChangeCallback? ProgressChangeCallback { get; set; }
        private DirectoryCopier.CompletedCallback? ProgressCompletedCallback { get; set; }

        public Tool(string name, string sourcePath, string localPath)
        {
            Name = name;
            SourcePath = sourcePath;
            LocalPath = localPath;
        }

        // every concrete tool implements its own logic for these methods.
        public abstract UserControl? GetUserControl();
        public abstract void Launch();

        public void SetProgressCallbacks(DirectoryCopier.ProgressChangeCallback progressChangeCallback,
            DirectoryCopier.CompletedCallback progressCompletedCallback)
        {
            ProgressChangeCallback = progressChangeCallback;
            ProgressCompletedCallback = progressCompletedCallback;
        }
        private void OnCopyCompleted()
        {
            ProgressCompletedCallback?.Invoke();
            CheckVersions();
        }

        private bool DeleteLocalFiles()
        {
            Directory.Delete(LocalPath, true);
            return true;
        }

        private void GetFiles()
        {
            if (!Directory.Exists(LocalPath))
            {
                DirectoryCopier.Copy(SourcePath, LocalPath, ProgressChangeCallback, OnCopyCompleted);
            }
            else
            {
                CheckVersions();
            }
        }

        private void CheckVersions()
        {
            var sourceVersion = FileHelper.GetFileKeyValueContent(@$"{SourcePath}\appInfo.txt")["version"];
            var localVersion = FileHelper.GetFileKeyValueContent(@$"{LocalPath}\appInfo.txt")["version"];

            if (sourceVersion != localVersion)
            {
                DeleteLocalFiles();
                GetFiles();
            }
            else
            {
                Launch();
            }
        }

        public void Run()
        {
            using (WaitCursor.Subscribe())
            {
                GetFiles();
            }
        }
    }
}
