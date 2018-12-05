using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StarterAPI.Jobs
{
    public abstract class HostService : IHostedService
    {
        private CancellationTokenSource _cts;
        private Task executingTask;
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            executingTask = ExecuteAsync(_cts.Token);
            return executingTask.IsCompleted ? executingTask : Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if(executingTask == null)
            {
                return;
            }
            try
            {
                _cts.Cancel();
            }
            finally
            {
                await Task.WhenAny(executingTask, Task.Delay(-1, cancellationToken));
            }
        }

        public abstract Task ExecuteAsync(CancellationToken cancellationToken);
    }
}
