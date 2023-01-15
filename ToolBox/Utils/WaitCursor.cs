namespace ToolBox.Utils
{
    public class WaitCursor : IDisposable
    {
        private readonly Cursor? OldCursor;
        private static bool IsWaitCursorActive;
        private WaitCursor()
        {
            IsWaitCursorActive = true;
            OldCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
        }

        public void Dispose()
        {
            Cursor.Current = OldCursor;
            IsWaitCursorActive = false;
        }
        
        public static IDisposable? BeginWaitCursorBlock()
        {
            return (!IsWaitCursorActive) ? (IDisposable)new WaitCursor() : null;
        }
    }
}
