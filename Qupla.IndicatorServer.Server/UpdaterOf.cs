namespace Qupla.IndicatorServer.Server
{
    public interface UpdaterOf<T> : IDynamicallyLoadedUpdater where T : IIndicatorConfiguration
    {
        IIndicatorState Update(T indicatorConfiguration);
    }

    public interface IDynamicallyLoadedUpdater
    {

    }
}