using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using static BackGroundTask.Controllers.StopWatchHelper;

namespace BackGroundTask.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            LogEllapsedMs("Index()", () =>
            {
                ViewBag.Title = "Home Page";

                LogEllapsedMs("Background task 1", () =>
                {
                    HostingEnvironment.QueueBackgroundWorkItem(ctx =>
                    {
                        Thread.Sleep(5000);
                        Debug.WriteLine($"[WebFramework_Debug]: Background task 1 finish");
                    });
                });

                LogEllapsedMs("Background task 2", () =>
                {
                    HostingEnvironment.QueueBackgroundWorkItem(ctx =>
                    {
                        Thread.Sleep(5000);
                        Debug.WriteLine($"[WebFramework_Debug]: Background task 2 finish");
                    });
                });

                LogEllapsedMs("Background task 3", () =>
                {
                    HostingEnvironment.QueueBackgroundWorkItem(ctx =>
                    {
                        Thread.Sleep(5000);
                        Debug.WriteLine($"[WebFramework_Debug]: Background task 3 finish");
                    });
                });
            });

            Debug.WriteLine($"[WebFramework_Debug]: return View()");
            return View();
        }

        public JsonResult Job()
        {
            JObject returnResult = new JObject();

            try
            {
                LogEllapsedMs("Job()", () =>
                {
                    IEnumerable<string> overDueTasks = null;
                    string milestoneChart = null;
                    IEnumerable<string> taskCategoriesData = null;

                    Debug.WriteLine($"[WebFramework_Debug]: 1B: {Thread.CurrentThread.ManagedThreadId}");
                    new JobHost().DoWork(() =>
                    {
                        Debug.WriteLine($"[WebFramework_Debug]: 1DWB: {Thread.CurrentThread.ManagedThreadId}");
                        Thread.Sleep(5000);
                        Debug.WriteLine($"[WebFramework_Debug]: GetOverDueTasks task finish");

                        overDueTasks = new List<string> {"A", "B", "C"};
                    });


                    Debug.WriteLine($"[WebFramework_Debug]: 2B: {Thread.CurrentThread.ManagedThreadId}");
                    new JobHost().DoWork(() =>
                    {
                        Thread.Sleep(5000);
                        Debug.WriteLine($"[WebFramework_Debug]: GetTasksByDueTime task finish");
                        milestoneChart = "M, N, O";
                    });

                    new JobHost().DoWork(() =>
                    {
                        Thread.Sleep(5000);
                        Debug.WriteLine($"[WebFramework_Debug]: GetTaskModules task finish");

                        taskCategoriesData = new List<string> {"A", "B", "C"};
                    });

                    returnResult.Add(new JProperty("GetOverDueTasks", JToken.FromObject(overDueTasks)));
                    returnResult.Add(new JProperty("GetTasksByDueTime", milestoneChart));
                    returnResult.Add(new JProperty("GetTaskModules", JToken.FromObject(taskCategoriesData)));
                });
            }
            catch (Exception ex)
            {
                // Log
                Debug.WriteLine($"[WebFramework_Debug]: Catch called");
            }
            finally
            {
                Debug.WriteLine($"[WebFramework_Debug]: Finally called");
            }

            Debug.WriteLine($"[WebFramework_Debug]: return Result");
            return new JsonResult
            {
                Data = returnResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult Job1()
        {
            JObject returnResult = new JObject();

            try
            {
                LogEllapsedMs("Job1()", () =>
                {
                    IEnumerable<string> overDueTasks = null;
                    string milestoneChart = null;
                    IEnumerable<string> taskCategoriesData = null;

                    Debug.WriteLine($"[WebFramework_Debug]: Before Job1 Thread: {Thread.CurrentThread.ManagedThreadId}");
                    BackgroundTaskManager.Run(() =>
                    {
                        Debug.WriteLine($"[WebFramework_Debug]: Job1 Thread: {Thread.CurrentThread.ManagedThreadId}");
                        Thread.Sleep(5000);
                        Debug.WriteLine($"[WebFramework_Debug]: GetOverDueTasks1 task finish");

                        overDueTasks = new List<string> { "A", "B", "C" };
                    });

                    Debug.WriteLine($"[WebFramework_Debug]: Before Job2 Thread: {Thread.CurrentThread.ManagedThreadId}");
                    BackgroundTaskManager.Run(() =>
                    {
                        Debug.WriteLine($"[WebFramework_Debug]: Job2 Thread: {Thread.CurrentThread.ManagedThreadId}");
                        Thread.Sleep(5000);
                        Debug.WriteLine($"[WebFramework_Debug]: GetTasksByDueTime1 task finish");
                        milestoneChart = "M, N, O";
                    });

                    Debug.WriteLine($"[WebFramework_Debug]: Before Job3 Thread: {Thread.CurrentThread.ManagedThreadId}");
                    BackgroundTaskManager.Run(() =>
                    {
                        Debug.WriteLine($"[WebFramework_Debug]: Job3 Thread: {Thread.CurrentThread.ManagedThreadId}");
                        Thread.Sleep(5000);
                        Debug.WriteLine($"[WebFramework_Debug]: GetTaskModules1 task finish");

                        taskCategoriesData = new List<string> { "A", "B", "C" };
                    });

                    // returnResult.Add(new JProperty("GetOverDueTasks", JToken.FromObject(overDueTasks)));
                    // returnResult.Add(new JProperty("GetTasksByDueTime", milestoneChart));
                    // returnResult.Add(new JProperty("GetTaskModules", JToken.FromObject(taskCategoriesData)));
                });
            }
            catch (Exception ex)
            {
                // Log
                Debug.WriteLine($"[WebFramework_Debug]: Catch1 called");
            }
            finally
            {
                Debug.WriteLine($"[WebFramework_Debug]: Finally1 called");
            }

            Debug.WriteLine($"[WebFramework_Debug]: return Result1");
            return new JsonResult
            {
                Data = returnResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult Job2()
        {
            JObject returnResult = new JObject();
            SampleAspNetTimer.Start();

            try
            {
                LogEllapsedMs("Job2()", () =>
                {
                    IEnumerable<string> overDueTasks = null;
                    string milestoneChart = null;
                    IEnumerable<string> taskCategoriesData = null;

                    HostingEnvironment.QueueBackgroundWorkItem(ctx =>
                    {
                        Thread.Sleep(5000);
                        Debug.WriteLine($"[WebFramework_Debug]: GetOverDueTasks2 task finish");

                        overDueTasks = new List<string> { "A", "B", "C" };
                    });

                    HostingEnvironment.QueueBackgroundWorkItem(ctx =>
                    {
                        Thread.Sleep(5000);
                        Debug.WriteLine($"[WebFramework_Debug]: GetTasksByDueTime2 task finish");
                        milestoneChart = "M, N, O";
                    });

                    HostingEnvironment.QueueBackgroundWorkItem(ctx =>
                    {
                        Thread.Sleep(5000);
                        Debug.WriteLine($"[WebFramework_Debug]: GetTaskModules2 task finish");

                        taskCategoriesData = new List<string> { "A", "B", "C" };
                    });

                    // returnResult.Add(new JProperty("GetOverDueTasks2", JToken.FromObject(overDueTasks)));
                    // returnResult.Add(new JProperty("GetTasksByDueTime2", milestoneChart));
                    // returnResult.Add(new JProperty("GetTaskModules2", JToken.FromObject(taskCategoriesData)));
                });
            }
            catch (Exception ex)
            {
                // Log
                Debug.WriteLine($"[WebFramework_Debug]: Catch2 called");
            }
            finally
            {
                Debug.WriteLine($"[WebFramework_Debug]: Finally2 called");
            }

            Debug.WriteLine($"[WebFramework_Debug]: return Result2");
            return new JsonResult
            {
                Data = returnResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}
