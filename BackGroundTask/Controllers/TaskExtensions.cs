using System.Threading;
using System.Threading.Tasks;

namespace BackGroundTask.Controllers
{
    public static class TaskExtensions
    {
        public static async Task WithCancellation(this Task originalTask, CancellationToken ct)
        {
            var cancelTask = new TaskCompletionSource<object>();
            using (ct.Register(t => ((TaskCompletionSource<object>)t).TrySetResult(new object()), cancelTask))
            {
                Task any = await Task.WhenAny(originalTask, cancelTask.Task);
                if (any == cancelTask.Task)
                {
                    ct.ThrowIfCancellationRequested();
                }
            }
            await originalTask;
        }
    }
}