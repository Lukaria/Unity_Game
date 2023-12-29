using UnityEngine;
using Zenject;

public class CameraInstaller : MonoInstaller
{
    [SerializeField] private CameraScript cameraScript;
    public override void InstallBindings()
    {
        Container.Bind<CameraScript>().FromInstance(cameraScript).AsSingle().NonLazy();
        Container.QueueForInject(cameraScript);
    }
}