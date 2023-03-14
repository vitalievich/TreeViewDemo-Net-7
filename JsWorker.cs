using Microsoft.JSInterop;

namespace TreeViewDemo
{
    public interface IJsWorker
    {
        Task SetStringAsync(string key, string value);
        Task<string> GetStringAsync(string key);
    }
    public class JsWorker : IJsWorker
    {
        private readonly IJSRuntime _jsRuntime;
        public JsWorker(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }
        public async Task SetStringAsync(string key, string value)
        {
            if (value != null)
                await _jsRuntime.InvokeVoidAsync("set", key, value);
        }
   
        public async Task<string> GetStringAsync(string key)
        {
            var result = await _jsRuntime.InvokeAsync<string>("get", key);       
            return result;
        }

    }
}
