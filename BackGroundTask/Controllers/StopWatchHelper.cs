using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BackGroundTask.Controllers
{
    public static class StopWatchHelper
    {
        public static void LogEllapsedMs(string formatMessage, Action func)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            func();
            stopwatch.Stop();

            Debug.WriteLine($"[WebFramework_Debug]: Message: {formatMessage} Ticks: {stopwatch.ElapsedTicks} ms: {stopwatch.ElapsedMilliseconds}");
        }
    }
}