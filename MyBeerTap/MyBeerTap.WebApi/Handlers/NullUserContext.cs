using System;

namespace MyBeerTap.WebApi.Handlers
{
    public class NullUserContext : IDisposable
    {
        public void Dispose() { }
    }
}