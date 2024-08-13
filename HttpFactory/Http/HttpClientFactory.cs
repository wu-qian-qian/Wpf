using System.Collections.Concurrent;

namespace HttpFactory
{
    public  class HttpClientFactory
    {
        int _count;
        public static ConcurrentQueue<HttpClient> _dic;
        public HttpClientFactory(int count = 10)
        {
            _count=count;
            _dic = new ConcurrentQueue<HttpClient>();
            Initialization();
        }

        private void Initialization()
        {
            for (int i = 0; i < _count; i++)
            {
                _dic.Enqueue(new HttpClient());
            }
        }

        private void Enqueue(HttpClient client)
        {
            if (_dic.Count <2*_count)
            {
                _dic.Enqueue(client);
            }
        }

        private HttpClient? TryDequeue()
        {
            _dic.TryDequeue(out var client);
            if(client==null)
                client=new HttpClient();
            return client;
        }

        /// <summary>
        /// 发送josn格式以body
        /// </summary>
        /// <param name="josn"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        HttpRequestMessage CreatRequest(string josn, HttpMethod method,Uri uri)
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.Method = method;
            httpRequest.Content=new StringContent(josn);
            httpRequest.Headers.Add("Accept", "application/json");
            httpRequest.RequestUri = uri;
            httpRequest.Content.Headers.ContentType=new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return httpRequest;
        }
        HttpRequestMessage CreatRequest(Uri uri, HttpMethod method)
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.Method = method;
            httpRequest.RequestUri = uri;
            return httpRequest;
        }

        /// <summary>
        /// heat.get
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetAsync(string uri)
        {
            var client = TryDequeue();
            var res=  await client.GetAsync(uri);
            Enqueue(client);
            return res;
        }
        /// <summary>
        /// header.get
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="josn"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetAsync(string uri, string josn)
        {
            var client = TryDequeue();
            var request = CreatRequest(josn, HttpMethod.Get, new Uri(uri));
            var res = await client.SendAsync(request);
            Enqueue(client);
            return res;
        }
        /// <summary>
        /// header.post
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async  Task<HttpResponseMessage> PostAsync(string uri)
        {
            var client = TryDequeue();
            var request = CreatRequest(new Uri(uri), HttpMethod.Post);
            var res= await client.SendAsync(request);
            Enqueue(client);
            return res;
        }

        /// <summary>
        /// body.post
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="josn"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostAsync(string uri,string josn)
        {
            var client = TryDequeue();
            var request = CreatRequest(josn, HttpMethod.Post,new Uri(uri));
            var res = await client.SendAsync(request);
            Enqueue(client);
            return res;
        }
    }
}
