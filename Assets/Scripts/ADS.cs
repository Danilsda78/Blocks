using Client;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;
using YG;

public class ADS : MonoBehaviour
{
    public LeaderboardYG leaderboardYG;


    private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;

    private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

    void Rewarded(int _)
    {
        ref var score = ref ScoreSystem.SCORE.Get<ScoreC>().BestCount;
        score += 5;

        YandexGame.savesData.score = score;

        leaderboardYG.NewScore(score);
        leaderboardYG.UpdateLB();

        YandexGame.SaveProgress();
        //YandexGame.SaveCloud();
    }

    void ExampleOpenRewardAd()
    {
        YandexGame.RewVideoShow(0);
    }
}
