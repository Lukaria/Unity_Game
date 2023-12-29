using UnityEngine;
using Zenject;
public class KillCounterInstaller : MonoInstaller
{
    [SerializeField] private KillCounter killCounter;

    public override void InstallBindings()
    {
        Container.Bind<KillCounter>().FromInstance(killCounter).AsSingle().NonLazy();
        Container.QueueForInject(killCounter);
    }
}
