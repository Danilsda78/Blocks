using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class WinSystem : IEcsRunSystem
    {
        readonly EcsWorld _world;
        readonly EcsFilter<EWinC> _filterWin;
        readonly EcsFilter<ScoreC> _filterScore;

        void IEcsRunSystem.Run()
        {
            foreach (var win in _filterWin)
            {
                _world.NewEntity().Get<EDestroyC>();
                _world.NewEntity().Get<EAddScoreC>();
                _world.NewEntity().Get<EInitGameC>();

                foreach (var item in _filterScore)
                    _filterScore.Get1(item).Lvl++;
            }
        }
    }
}