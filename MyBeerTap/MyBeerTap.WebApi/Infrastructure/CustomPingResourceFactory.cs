using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using IQ.Platform.Framework.WebApi.Diagnostic;

namespace MyBeerTap.WebApi.Infrastructure
{
    /// <summary>
    /// A ping resource creator.
    /// </summary>
    class CustomPingResourceFactory : ICreatePingResource
    {
        readonly Lazy<Tuple<string, DateTime>> _assemblyData = new Lazy<Tuple<string, DateTime>>(() =>
            {
                var assembly = Assembly.GetExecutingAssembly();
                var versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
                var compileTime = File.GetLastWriteTimeUtc(assembly.Location);
                return Tuple.Create(versionInfo.FileVersion, compileTime);
            });

        public PingResource Create()
        {
            var assemblyData = _assemblyData.Value;
            return new PingResource(applicationVersion: assemblyData.Item1, dbVersion: "", compileTime: assemblyData.Item2);
        }
    }
}