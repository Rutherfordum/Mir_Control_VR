using MIR.Services.InputDevice;
using MIR.Services.MiRControlService;
using MIR.Services.PublisherMiRServices;
using MIR.Services.ROSConnector;
using UnityEngine;
using Zenject;

public class BootstrapInstaller: MonoInstaller
{
    [SerializeField] private ROSConnectConfiguration rosConfig;
    [SerializeField] private PublisherConfiguration publisherConfig;
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<InputUserDataService>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<ROSConnectorService>().AsSingle()
            .WithArguments(rosConfig.Protocol, rosConfig.IP, rosConfig.Port, rosConfig.Serializer)
            .NonLazy();

        Container.BindInterfacesTo<MovePublisherService>().AsSingle().WithArguments(publisherConfig.Topic).NonLazy();
        Container.BindInterfacesTo<MiRControlService>().AsSingle().NonLazy();
    }
}