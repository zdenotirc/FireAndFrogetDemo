﻿using System;
using System.Diagnostics;
using System.Threading;
using System.Web.Hosting;

namespace BackGroundTask.Controllers
{
    public class JobHost : IRegisteredObject
    {
        private readonly object _lock = new object();
        private bool _shuttingDown;

        public JobHost()
        {
            HostingEnvironment.RegisterObject(this);
        }

        public void Stop(bool immediate)
        {
            lock (_lock)
            {
                _shuttingDown = true;
            }
            HostingEnvironment.UnregisterObject(this);
        }

        public void DoWork(Action work)
        {
            lock (_lock)
            {
                if (_shuttingDown)
                {
                    return;
                }
                Debug.WriteLine($"[WebFramework_Debug]: 2DWB: {Thread.CurrentThread.ManagedThreadId}");
                work();
            }
        }
    }
}