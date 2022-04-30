using System;
using UnityEngine;
using Zenject;

public class GameContextInstaller : MonoInstaller
{
    
    [Header("Floating Text System")]
    [SerializeField] private GameObject _floatingTextPrefab;
    [Header("Dummy Spawner")]
    [SerializeField] private GameObject _dummyPrefab;
    
    public override void InstallBindings()
    {
        Container.Bind<GameObject>().WithId(FloatingTextSystem.FLOATING_TEXT_PREFAB_NAME).FromInstance(_floatingTextPrefab);
        
        Container.Bind<IFloatingTextSystem>().To<FloatingTextSystem>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        
        Container.Bind<DummySpawner>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        Container.BindFactory<DummyEntity, DummyEntity.Factory>().FromComponentInNewPrefab(_dummyPrefab);
        
        Container.Bind<IScreenShake>().To<ScreenShake>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PauseSystem>().FromNew().AsSingle().NonLazy();
        Container.Bind<IScoreSystem>().To<ScoreSystem>().FromNew().AsSingle().NonLazy();

        Container.Bind<IPhysicsSystem>().To<PhysicsSystem>().FromNew().AsSingle();
    }

    private void OnDestroy()
    {
        Container.Unbind<PauseSystem>();
    }
}