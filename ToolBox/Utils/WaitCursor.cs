namespace ToolBox.Utils
{
    public static class WaitCursor
    {
        private static List<WaitCursorSubscriber> Subscribers { get; } = new List<WaitCursorSubscriber>();

        private static void UnSubscribe(WaitCursorSubscriber subscriber) {
            Subscribers.Remove(subscriber);
            if(!Subscribers.Any())
            {
                Application.UseWaitCursor = false;
            }
        }

        public static WaitCursorSubscriber Subscribe()
        {
            if (!Subscribers.Any())
            {
                Application.UseWaitCursor = true;
            }

            var newSubscriber = new WaitCursorSubscriber();
            Subscribers.Add(newSubscriber);

            return newSubscriber;
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
