using StarterAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StarterAPI.Jobs
{
    public class DataRefreshService : HostService
    {
        private readonly RandomStringProvider _provider;
        public DataRefreshService(RandomStringProvider provider)
        {
            _provider = provider;
        }
        public override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while(!cancellationToken.IsCancellationRequested)
            {
                await _provider.UpdateString(cancellationToken);
                await Task.Delay(5000, cancellationToken);
            }
        }
    }
}
