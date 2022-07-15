using System.Threading;

namespace UdemyCourseExample
{
    public class PerThreadSingleton
    {
        private static readonly ThreadLocal<PerThreadSingleton> _threadInstance = 
            new (() => new PerThreadSingleton());

        public int Id { get; set; }
        
        private PerThreadSingleton()
        {
            Id = Thread.CurrentThread.ManagedThreadId;
        }

        public static PerThreadSingleton Instance => _threadInstance.Value;
    }
}