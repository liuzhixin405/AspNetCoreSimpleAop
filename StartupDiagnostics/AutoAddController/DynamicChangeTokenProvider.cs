using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Primitives;
using System.Threading;

namespace StartupDiagnostics
{
    public class DynamicChangeTokenProvider : IActionDescriptorChangeProvider
    {
        private CancellationTokenSource _source;
        private CancellationChangeToken _token;
        public DynamicChangeTokenProvider()
        {
            _source = new CancellationTokenSource();
            _token = new CancellationChangeToken(_source.Token);
        }
        public IChangeToken GetChangeToken() => _token;

        public void NotifyChanges()
        {
            var old = Interlocked.Exchange(ref _source, new CancellationTokenSource());
            _token = new CancellationChangeToken(_source.Token);
            old.Cancel();
        }
    }
}
