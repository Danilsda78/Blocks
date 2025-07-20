using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class MoveSystem : IEcsRunSystem
    {
        readonly EcsFilter<CubeC>.Exclude<FCubeBlockC> _filterC;
        readonly EcsFilter<CubeC, FCubeMoveC> _filterCM;
        readonly EcsFilter<PlatformC> _filterP;
        readonly EcsFilter<EInputC> _filterI;


        void IEcsRunSystem.Run()
        {
            foreach (var input in _filterI)
            {
                var derection = _filterI.Get1(input).Derection;
                foreach (var cube in _filterC)
                {
                    ref var e = ref _filterC.GetEntity(cube);
                    var c1 = _filterC.Get1(cube);

                    if (c1.IdPlatformDirection == derection)
                        e.Get<FCubeMoveC>();
                }
            }

            foreach (var item in _filterCM)
            {
                ref var cubeC = ref _filterCM.Get1(item);
                var listPositionEnd = _filterP.Get1(0).ListPositions;
                var targetPos = listPositionEnd[4][cubeC.IdPlatformPosition];

                cubeC.Transform.position = Vector3.MoveTowards(cubeC.Transform.position, targetPos, 10f * Time.deltaTime);

                if (cubeC.Transform.position == targetPos)
                {
                    ref var cm = ref _filterCM.GetEntity(item);
                    cm.Del<FCubeMoveC>();
                    cm.Get<FCubeBlockC>();
                    cm.Get<ECubeEndMoveC>();
                    _filterP.Get1(0).CountCubesOnCentr += 1;
                }
            }
        }
    }
}