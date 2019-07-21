using Microsoft.Extensions.Hosting;
using System;
using System.ServiceProcess;
//using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

namespace GenericHostLab.Service
{
    public class ServiceBaseLifetime : ServiceBase, IHostLifetime
    {
        private readonly TaskCompletionSource<object> delayStart = new TaskCompletionSource<object>();

        public ServiceBaseLifetime(IApplicationLifetime applicationLifetime)
        {
            ApplicationLifetime = applicationLifetime ?? throw new ArgumentNullException(nameof(applicationLifetime));
        }

        private IApplicationLifetime ApplicationLifetime { get; }

        public Task WaitForStartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("ServiceBaseLifetime - WaitForStartAsync");
            cancellationToken.Register(() => delayStart.TrySetCanceled());
            ApplicationLifetime.ApplicationStopping.Register(Stop);
            ApplicationLifetime.ApplicationStopped.Register(Stop);
            new Thread(Run).Start();
            return delayStart.Task;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("ServiceBaseLifetime - StopAsync");
            Stop();
            return Task.CompletedTask;
        }

        private void Run()
        {
            Console.WriteLine("ServiceBaseLifetime - Run");

            try
            {
                Run(this);
                delayStart.TrySetException(new InvalidOperationException("Stopped without starting"));
            }
            catch (Exception ex)
            {
                delayStart.TrySetException(ex);
            }
        }

        protected override void OnStart(string[] args)
        {
            Console.WriteLine("ServiceBaseLifetime - OnStart");

            delayStart.TrySetResult(null);
            base.OnStart(args);
        }
        protected override void OnStop()
        {
            Console.WriteLine("ServiceBaseLifetime - OnStop");

            ApplicationLifetime.StopApplication();
            base.OnStop();
        }

        protected override void OnShutdown()
        {
            base.OnShutdown();
        }
    }
}
