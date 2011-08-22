using System;
using Autofac;

namespace Qupla.IndicatorServer.Server.Autofac
{
    public class AutofacResolver : IResolver
    {
        private readonly IComponentContext _componentContext;

        public AutofacResolver(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public object Resolve(Type type)
        {
            return _componentContext.Resolve(type);
        }
    }
}