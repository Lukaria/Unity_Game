using UnityEngine;
using Zenject;

public class GameStateInstaller : MonoInstaller
{
    [SerializeField] private PauseMenu pauseMenu;
    public override void InstallBindings()
    {
        Container.Bind<PauseMenu>().FromInstance(pauseMenu).AsSingle().NonLazy();
        Container.QueueForInject(pauseMenu);
    }
}