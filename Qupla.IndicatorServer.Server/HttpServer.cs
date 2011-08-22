using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Qupla.IndicatorServer.Server
{
    public class HttpServer
    {
        private readonly IEnumerable<string> _urlPrefixes;
        private readonly Action<HttpListenerContext> _handleRequestCallback;
        private HttpListener _httpListener;
        private CancellationTokenSource _cancellation;
        private CancellationToken _cancellationToken;
        private WaitHandle _cancel;

        public HttpServer(IEnumerable<string> urlPrefixes, Action<HttpListenerContext> handleRequestCallback)
        {
            _urlPrefixes = urlPrefixes;
            _handleRequestCallback = handleRequestCallback;
        }

        public void Start()
        {
            _cancellation = new CancellationTokenSource();
            _cancellationToken = _cancellation.Token;
            _cancel = _cancellationToken.WaitHandle;
            Task.Factory.StartNew(StartListener, _cancellationToken);
        }

        public void Stop()
        {
            _cancellation.Cancel();
            _httpListener.Stop();
        }

        private void StartListener()
        {
            _httpListener = new HttpListener();
            _urlPrefixes.ToList().ForEach(p => _httpListener.Prefixes.Add(p));
            _httpListener.Start();

            while (_httpListener.IsListening)
            {
                var asyncResult = _httpListener.BeginGetContext(ReceiveRequest, null);

                var signaledHandleIndex = WaitHandle.WaitAny(new[] {asyncResult.AsyncWaitHandle, _cancel});
                if (signaledHandleIndex == 1)
                    return;
            }
        }

        private void ReceiveRequest(IAsyncResult ar)
        {
            // Sometimes this method is called when the listener is closing
            if (!_httpListener.IsListening)
                return;

            var context = _httpListener.EndGetContext(ar);
            Task.Factory.StartNew(() => _handleRequestCallback(context), _cancellationToken);
        }
    }
}