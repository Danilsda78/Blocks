using Leopotam.Ecs;
using UnityEngine.SocialPlatforms.Impl;
using YG;

namespace Client
{
    internal class ScoreSystem : IEcsInitSystem, IEcsRunSystem
    {
        readonly UIContainer uIContainer;
        readonly EcsWorld _world;
        readonly EcsFilter<ScoreC> _filterScore;
        readonly EcsFilter<EAddScoreC> _filterAddScore;
        readonly EcsFilter<EClearScoreC> _filterClearScore;
        readonly EcsFilter<ECheckScoreC> _filterCheck;

        static public EcsEntity SCORE;

        public void Init()
        {
            var eScore = _world.NewEntity();
            ref var c = ref eScore.Get<ScoreC>();

            SCORE = eScore;

            c.BestCount = YandexGame.savesData.score;
            uIContainer.LeaderboardYG.NewScore(c.BestCount);
            uIContainer.LeaderboardYG.UpdateLB();
        }

        public void Run()
        {
            foreach (var itemScore in _filterScore)
            {
                ref var cScore = ref _filterScore.Get1(itemScore);

                foreach (var itemAdd in _filterAddScore)
                {
                    cScore.TempCount++;
                }

                foreach (var itemClear in _filterClearScore)
                {
                    if (cScore.TempCount > cScore.BestCount)
                    {
                        cScore.BestCount = cScore.TempCount;
                        Save(cScore.TempCount);
                    }

                    cScore.TempCount = 0;
                    cScore.Lvl = 0;
                }

                foreach (var itemCheck in _filterCheck)
                {
                    if (cScore.TempCount > cScore.BestCount)
                    {
                        cScore.BestCount = cScore.TempCount;
                        Save(cScore.TempCount);
                    }
                }
            }
        }

        private void Save(int count)
        {
            YandexGame.savesData.score = count;
            uIContainer.LeaderboardYG.NewScore(count);
            uIContainer.LeaderboardYG.UpdateLB();
            YandexGame.SaveProgress();
            YandexGame.SaveCloud();
        }
    }
}