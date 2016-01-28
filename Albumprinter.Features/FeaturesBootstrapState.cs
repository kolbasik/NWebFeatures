namespace Albumprinter.Features
{
    public enum FeaturesBootstrapState
    {
        None,
        Starting,
        ActivateFeatures,
        ActivatedFeatures,
        RegisterServices,
        RegisteredServices,
        RegisterMiddleware,
        RegisteredMiddleware,
        StartShell,
        StartedShell,
        Started,
        Stoping,
        DeactivateFeatures,
        DeactivatedFeatures,
        Stoped
    }
}