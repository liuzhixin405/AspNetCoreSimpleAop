using Contract;
using Microsoft.AspNetCore.Mvc;

namespace Impl
{
    public class SecondImplService : IPlugin
    {
        public async Task<object> Add( string entity)
        {
            await Task.CompletedTask;
            return "add two 1";
        }

        public Task<object> Get(string entity)
        {
            throw new NotImplementedException();
        }

        public async Task<object> Update( string entity)
        {
            await Task.CompletedTask;
            return "add two 2";
        }
    }
}
