using Leopotam.Ecs;
using UnityEngine;
using YG;

namespace Client
{
    internal class UIManagerSystem : IEcsInitSystem, IEcsRunSystem
    {
        readonly EcsWorld _world;
        readonly UIContainer _container;
        readonly EcsFilter<ScoreC> _filterScore;
        readonly EcsFilter<TimerC> _filterTimer;

        public void Init()
        {
            OpenMenu();

            _container.ButtonStart.onClick.AddListener(() =>
            {
                _world.NewEntity().Get<EInitGameC>();
                _world.NewEntity().Get<EClearScoreC>();
                OpenGame();
            });

            _container.ButtonRestart[0].onClick.AddListener(ReplayGame);

            _container.ButtonMenu[0].onClick.AddListener(OpenMenu);
            _container.ButtonMenu[1].onClick.AddListener(OpenMenu);

            _container.ButtonStop.onClick.AddListener(StopGame);
            _container.ButtonPlay.onClick.AddListener(PlayGame);
            _container.ButtonADS.onClick.AddListener(() =>
            {
                YandexGame.RewVideoShow(0);
            });
        }

        public void Run()
        {
            foreach (var item in _filterScore)
            {
                ref var c = ref _filterScore.Get1(item);

                foreach (var best in _container.BestScore)
                    best.text = c.BestCount.ToString();

                foreach (var temp in _container.TempScore)
                    temp.text = c.TempCount.ToString();
            }

            foreach (var item in _filterTimer)
            {
                ref var c = ref _filterTimer.Get1(item);
                _container.Timer.maxValue = c.MaxTime;
                _container.Timer.value = c.CurentTime;
            }
        }

        private void Close()
        {
            _container.UiMenuPanel.SetActive(false);
            _container.UiGamePanel.SetActive(false);
            _container.UiLosePanel.SetActive(false);
            _container.UiStopPanel.SetActive(false);
            Time.timeScale = 1;
        }

        private void OpenMenu()
        {
            _world.NewEntity().Get<EClearScoreC>();
            _world.NewEntity().Get<EDestroyC>();
            Close();
            _container.UiMenuPanel.SetActive(true);
        }

        private void OpenGame()
        {
            Close();
            _container.UiGamePanel.SetActive(true);
        }

        private void StopGame()
        {
            Time.timeScale = 0;
            _container.UiStopPanel.SetActive(true);
        }

        private void PlayGame()
        {
            _container.UiStopPanel.SetActive(false);
            Time.timeScale = 1;
        }

        private void ReplayGame()
        {
            _world.NewEntity().Get<EClearScoreC>();
            _world.NewEntity().Get<EInitGameC>();
            OpenGame();
        }

        private void OpenLose()
        {
            Close();
            _container.UiLosePanel.SetActive(true);
        }
    }
}