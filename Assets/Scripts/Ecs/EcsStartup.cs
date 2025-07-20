using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Client
{
    sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField] private SceneData _sceneData;
        [SerializeField] private UIContainer _uIContainer;
        [SerializeField] private Camera _mainCamera;

        private EcsWorld _world;
        private EcsSystems _systems;

        private void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif

            _systems
                .Inject(_sceneData)
                .Inject(_uIContainer)
                .Inject(_mainCamera)

                .Add(new UIManagerSystem())
                .Add(new InitSystem())
                .Add(new InputSystem())
                .Add(new MoveSystem())
                .Add(new EndGameSystem())
                .Add(new WinSystem())
                .Add(new LoseSystem())
                .Add(new ScoreSystem())
                .Add(new DestroySystem())
                .Add(new TimerSystem())

                .OneFrame<EDestroyC>()
                .OneFrame<EInputC>()
                .OneFrame<ECubeOnTriggerEnterC>()
                .OneFrame<ECubeEndMoveC>()
                .OneFrame<ECubeEndAnimationStartC>()
                .OneFrame<EWinC>()
                .OneFrame<ELoseC>()
                .OneFrame<EAddScoreC>()
                .OneFrame<ECheckScoreC>()
                .OneFrame<EClearScoreC>()

                .Init();
        }

        private void Update()
        {
            _systems?.Run();
        }

        private void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
                _world.Destroy();
                _world = null;
            }
        }
    }
}