using Leopotam.Ecs;
using YG;

namespace Client
{
    sealed class LoseSystem : IEcsRunSystem
    {
        readonly EcsWorld _world;
        readonly EcsFilter<ELoseC> _filterLose;
        readonly UIContainer _container;

        public void Run()
        {
            foreach (var itemLose in _filterLose)
            {
                _world.NewEntity().Get<EDestroyC>();
                _world.NewEntity().Get<ECheckScoreC>();

                Sound.ELose?.Invoke();
                _container.LeaderboardYG.UpdateLB();
                _container.UiLosePanel.SetActive(true);
            }
        }
    }
}