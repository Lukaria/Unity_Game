 using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerMovementScript player;
    public override void InstallBindings()
    {
        Container.Bind<PlayerMovementScript>().FromInstance(player).AsSingle().NonLazy();
        Container.QueueForInject(player);
    }
}