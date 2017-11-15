using System;
using System.Diagnostics;
using System.Threading;

namespace BackGroundTask.Controllers
{
    public static class SampleAspNetTimer
    {
        // The timer executes its callback on CLR Threadpool threads !!!
        private static readonly Timer _timer = new Timer(OnTimerElapsed);
        private static readonly JobHost _jobHost = new JobHost();

        public static void Start()
        {
            _timer.Change(TimeSpan.Zero, TimeSpan.FromMilliseconds(1000));
        }

        private static void OnTimerElapsed(object sender)
        {
            _jobHost.DoWork(() =>
            {
                Debug.WriteLine($"[WebFramework_Debug]: JobSample1 Thread: {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(5000);
                Debug.WriteLine($"[WebFramework_Debug]: JobSample1 task finish");
            });

            _jobHost.DoWork(() =>
            {
                Debug.WriteLine($"[WebFramework_Debug]: JobSample2 Thread: {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(5000);
                Debug.WriteLine($"[WebFramework_Debug]: JobSample2 task finish");
            });
        }
    }
}