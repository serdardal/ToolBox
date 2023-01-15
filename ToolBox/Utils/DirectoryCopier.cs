using System.ComponentModel;

namespace ToolBox.Utils
{
    public static class DirectoryCopier
    {
        public delegate void ProgressChangeCallback(int percentage);
        public delegate void CompletedCallback();

        private static BackgroundWorker? BackgroundWorker { get; set; }

        private static bool IsFirstIteration { get; set; } = true;
        private static int AllFilesCount { get; set; } = 0;
        private static int ProcessedFileCount { get; set; } = 0;

        private static string SourceDir { get; set; } = "";
        private static string DestinationDir { get; set; } = "";
        private static ProgressChangeCallback? _ProgressChangeCallback { get; set; }
        private static CompletedCallback? _CompletedCallback { get; set; }


        private static void ResetCopyProps() {
            IsFirstIteration = true;
            AllFilesCount = 0;
            ProcessedFileCount = 0;
        }

        private static void BgWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            CopyDirectory(SourceDir, DestinationDir);

            // delay for progress bar visual update complete
            Thread.Sleep(1000);
        }

        private static void BgWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            if (_ProgressChangeCallback == null)
                return;

            _ProgressChangeCallback(e.ProgressPercentage);
        }

        private static void BgWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            if (_CompletedCallback == null)
                return;

            _CompletedCallback();
        }

        private static void CopyDirectory(string sourceDir, string destinationDir)
        {
            if (IsFirstIteration)
            {
                AllFilesCount = Directory.GetFiles(sourceDir, "*.*", SearchOption.AllDirectories).Length;
                IsFirstIteration = false;
            }

            // get the subdirectories for the specified directory.
            var sourceDirInfo = new DirectoryInfo(sourceDir);

            if (!sourceDirInfo.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                + sourceDir);
            }

            DirectoryInfo[] sourceSubDirs = sourceDirInfo.GetDirectories();
            // if the destination directory doesn't exist, create it.
            if (!Directory.Exists(destinationDir))
            {
                Directory.CreateDirectory(destinationDir);
            }

            // get the files in the directory and copy them to the new location.
            FileInfo[] files = sourceDirInfo.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destinationDir, file.Name);
                file.CopyTo(tempPath, false);
                ProcessedFileCount++;

                // progress change notify
                var percentage = (int) ((ProcessedFileCount / (decimal)AllFilesCount) * 100);
                BackgroundWorker?.ReportProgress(percentage);
            }

            foreach (DirectoryInfo subdir in sourceSubDirs)
            {
                string tempPath = Path.Combine(destinationDir, subdir.Name);
                CopyDirectory(subdir.FullName, tempPath);
            }
        }

        private static void RunBackgroundWorker() {
            BackgroundWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true
            };
            BackgroundWorker.DoWork += new DoWorkEventHandler(BgWorker_DoWork);
            BackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(BgWorker_ProgressChanged);
            BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BgWorker_RunWorkerCompleted);
            BackgroundWorker.RunWorkerAsync();
        }

        public static void Copy(string sourceDir, string destinationDir, 
            ProgressChangeCallback progressChangeCallback, CompletedCallback completedCallback)
        {
            SourceDir = sourceDir;
            DestinationDir = destinationDir;
            _ProgressChangeCallback = progressChangeCallback;
            _CompletedCallback = completedCallback;

            ResetCopyProps();

            RunBackgroundWorker();
        }
    }
}
