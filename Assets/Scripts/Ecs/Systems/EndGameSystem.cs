using Leopotam.Ecs;

namespace Client
{
    internal class EndGameSystem : IEcsRunSystem
    {
        readonly EcsWorld _world;
        readonly EcsFilter<PlatformC> _filterPlatfom;
        readonly EcsFilter<TimerC> _filterTimer;
        readonly EcsFilter<ECubeOnTriggerEnterC> _filterCobeTriget;

        public void Run()
        {
            foreach (var itemPlatform in _filterPlatfom)
            {
                ref var c1 = ref _filterPlatfom.Get1(itemPlatform);
                bool result = c1.CountCubesOnCentr == c1.ListPositions[0].Length;

                if (result)
                    _world.NewEntity().Get<EWinC>();
            }
            foreach (var itemTimer in _filterTimer)
            {
                ref var c = ref _filterTimer.Get1(itemTimer);
                if (c.CurentTime <= 0)
                    _world.NewEntity().Get<ELoseC>();
            }
            foreach (var itemCube in _filterCobeTriget)
            {
                _world.NewEntity().Get<ELoseC>();
            }
        }
    }
}