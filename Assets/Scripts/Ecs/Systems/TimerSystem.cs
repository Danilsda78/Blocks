using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    internal class TimerSystem : IEcsInitSystem, IEcsRunSystem
    {
        readonly EcsWorld _world;
        readonly EcsFilter<TimerC> _filterTimer;

        public void Init()
        {
        }

        public void Run()
        {
            foreach (var itemTimer in _filterTimer)
            {
                ref var c1 = ref _filterTimer.Get1(itemTimer);
                c1.CurentTime -= 1f * Time.deltaTime;
            }
        }
    }
}