using System;

namespace Qupla.IndicatorServer.Server
{
    public interface IResolver
    {
        object Resolve(Type type);
    }
}