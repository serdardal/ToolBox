namespace ToolBox.Utils
{
    public static class WaitCursor
    {
        public delegate void WaitChangeCallback(bool isWaiting);

        private static List<WaitCursorSubscriber> Subscribers { get; } = new List<WaitCursorSubscriber>();
        private static List<WaitChangeCallback> WaitChangeCallbacks { get; } = new List<WaitChangeCallback>();

        public static WaitCursorSubscriber Subscribe()
        {
            if (!Subscribers.Any())
            {
                Application.UseWaitCursor = true;
                WaitChangeCallbacks.ForEach(c => c(true));
            }

            var newSubscriber = new WaitCursorSubscriber();
            Subscribers.Add(newSubscriber);

            return newSubscriber;
        }

        public static void AddWaitChangeCallback(WaitChangeCallback callback)
        {
            WaitChangeCallbacks.Add(callback);
        }

        private static void UnSubscribe(WaitCursorSubscriber subscriber)
        {
            Subscribers.Remove(subscriber);

            if (!Subscribers.Any())
            {
                Application.UseWaitCursor = false;
                WaitChangeCallbacks.ForEach(c => c(false));
            }
        }

        public class WaitCursorSubscriber : IDisposable
        {
            public void Dispose()
            {
                UnSubscribe(this);
            }
        }
    }
}
