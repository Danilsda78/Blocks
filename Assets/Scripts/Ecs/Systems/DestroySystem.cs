using Leopotam.Ecs;
using UnityEngine;
using YG;

namespace Client
{
    sealed class DestroySystem : IEcsRunSystem
    {
        readonly EcsFilter<EDestroyC> _filterDestroy;
        readonly EcsFilter<CubeC> _filterCube;
        readonly EcsFilter<PlatformC> _filterPlatform;
        readonly EcsFilter<FGamePlayC> _filterPlay;
        readonly EcsFilter<TimerC> _filterTimer;

        public void Run()
        {
            foreach (var itemDestroy in _filterDestroy)
            {
                foreach (var itemCube in _filterCube)
                {
                    ref var eCube = ref _filterCube.GetEntity(itemCube);
                    ref var cCube = ref _filterCube.Get1(itemCube);
                    var go = cCube.Transform.gameObject;

                    go.GetComponent<Cube>().Destroy();
                }
                foreach (var itemPlatform in _filterPlatform)
                {
                    ref var ePlat = ref _filterPlatform.GetEntity(itemPlatform);
                    ref var cPlat = ref _filterPlatform.Get1(itemPlatform);
                    var go = cPlat.Transform.gameObject;

                    go.GetComponent<Platforms>().Destroy();
                }
                foreach(var itemPlay in _filterPlay)
                {
                    _filterPlay.GetEntity(itemPlay).Destroy();
                }
                foreach (var item in _filterTimer)
                {
                    _filterTimer.GetEntity(item).Destroy();
                }

            }
        }
    }
}