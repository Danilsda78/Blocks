using Leopotam.Ecs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Client
{
    internal class InitSystem : IEcsRunSystem
    {
        readonly EcsWorld _world;
        readonly EcsFilter<EInitGameC> _filterInit;
        readonly EcsFilter<PlatformC> _filterPlatf;
        readonly EcsFilter<ECubeEndAnimationStartC> _filterEndInit;
        readonly EcsFilter<ScoreC> _filterScore;
        readonly SceneData _sceneData;
        readonly UIContainer uIContainer;

        public void Run()
        {
            foreach (var item in _filterInit)
            {
                uIContainer.Timer.maxValue = 1;
                uIContainer.Timer.value = 1;

                int lvl = 0, rows = 0, colums = 0;

                foreach (var score in _filterScore)
                    lvl = _filterScore.Get1(score).Lvl;

                rows = Random.Range(2, 4);
                colums = Random.Range(2, 4);

                if (lvl < 4)
                {
                    rows = 2;
                    colums = 2;
                }
                else if (lvl < 9)
                    if (rows == 3 && colums == 3)
                        rows = 2;

                var go = GameObject.Instantiate(_sceneData.Platform).GetComponent<Platforms>();
                var ePlatform = go.Init(_world, rows, colums);
                ref var cPlatform = ref ePlatform.Get<PlatformC>();

                var posInstansCubs = SetCubePlatform(rows, colums);

                InstanCube(cPlatform, posInstansCubs);

                _filterInit.GetEntity(item).Destroy();
            }

            foreach (var item1 in _filterPlatf)
            {
                ref var c = ref _filterPlatf.Get1(item1);

                foreach (var item in _filterEndInit)
                    c.CountCubeInit++;

                if (_filterEndInit.GetEntitiesCount() == c.ListPositions[4].Length)
                {
                    var lvl = 1;
                    float time = 3f;

                    foreach (var score in _filterScore)
                        lvl = _filterScore.Get1(score).Lvl;

                    time += 2 / (lvl * 0.1f + 0.1f);

                    GamePlayStart(time);
                    c.CountCubeInit = 0;
                }
            }
        }

        private void GamePlayStart(float time)
        {
            _world.NewEntity().Get<FGamePlayC>();
            _world.NewEntity().Get<TimerC>().Init(time);
        }

        private void InstanCube(PlatformC platformC, int[,] arr)
        {
            int x = 0;

            for (int i = 0; i < arr.GetUpperBound(0) + 1; i++)
                for (int j = 0; j < arr.GetUpperBound(1) + 1; j++)
                {
                    var der = arr[i, j];
                    var gOCube = GameObject.Instantiate(_sceneData.Cube).GetComponent<Cube>();
                    var eCube = gOCube.Init(_world);
                    ref var cCube = ref eCube.Get<CubeC>();

                    cCube.Transform.position = platformC.ListPositions[der][x];
                    cCube.IdPlatformDirection = der;
                    cCube.IdPlatformPosition = x;
                    x++;
                }
        }

        private int[,] SetCubePlatform(int rows, int colums)
        {
            var platformCount = new int[rows, colums];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < colums; j++)
                    platformCount[i, j] = -1;

            var pullDerection = new int[4];
            var helpPullDerection = new List<int>() { 0, 1, 2, 3 };
            for (int i = 0; i < pullDerection.Length; i++)
            {
                var der = Random.Range(0, (-1) + (helpPullDerection.Count));
                pullDerection[i] = helpPullDerection[der];
                helpPullDerection.Remove(helpPullDerection[der]);
            }

            if (rows <= 3 || colums <= 3)
                platformCount[1, 1] = pullDerection[0];

            while (true)
            {
                var leftCubes = 0;
                foreach (var item in platformCount)
                    if (item == -1)
                        leftCubes++;

                for (int der = 0; der < 4; der++)
                {
                    if (leftCubes <= 0) return platformCount;

                    var freePull = new List<int[]>();
                    var derection = pullDerection[der];

                    if (derection == 0)
                        for (int i = 0; i < rows; i++)
                            for (int j = 0; j < colums; j++)
                            {
                                if (platformCount[i, j] != -1) continue;

                                int[] item = { i, j };
                                int[] prevItem = { i, j - 1 };

                                if (j == 0)
                                    freePull.Add(item);
                                else if (freePull.Count != 0 && freePull.Exists(e => e.SequenceEqual(prevItem)))
                                    freePull.Add(item);
                            }
                    else if (derection == 1)
                        for (int i = 0; i < rows; i++)
                            for (int j = 0; j < colums; j++)
                            {
                                if (platformCount[i, j] != -1) continue;

                                int[] item = { i, j };
                                int[] prevItem = { i - 1, j };

                                if (i == 0)
                                    freePull.Add(item);
                                else if (freePull.Count != 0 && freePull.Exists(e => e.SequenceEqual(prevItem)))
                                    freePull.Add(item);
                            }
                    else if (derection == 2)
                        for (int i = rows - 1; i >= 0; i--)
                            for (int j = colums - 1; j >= 0; j--)
                            {
                                if (platformCount[i, j] != -1) continue;

                                int[] item = { i, j };
                                int[] prevItem = { i, j + 1 };

                                if (j == colums - 1)
                                    freePull.Add(item);
                                else if (freePull.Count != 0 && freePull.Exists(e => e.SequenceEqual(prevItem)))
                                    freePull.Add(item);
                            }
                    else if (derection == 3)
                        for (int i = rows - 1; i >= 0; i--)
                            for (int j = colums - 1; j >= 0; j--)
                            {
                                if (platformCount[i, j] != -1) continue;

                                int[] item = { i, j };
                                int[] prevItem = { i + 1, j };

                                if (i == rows - 1)
                                    freePull.Add(item);
                                else if (freePull.Count != 0 && freePull.Exists(e => e.SequenceEqual(prevItem)))
                                    freePull.Add(item);
                            }

                    var setCubes = der == 3 ? leftCubes : Random.Range(0, (-1) + (der == 0 ? leftCubes : leftCubes + 1));
                    setCubes = freePull.Count == 1 ? 1 : setCubes;
                    setCubes = freePull.Count >= setCubes ? setCubes : freePull.Count;
                    leftCubes -= setCubes;

                    for (int i = 0; i < setCubes; i++)
                    {
                        var position = Random.Range(0, (-1) + (freePull.Count));
                        platformCount[freePull[position][0], freePull[position][1]] = derection;
                        freePull.Remove(freePull[position]);
                    }
                }
            }
        }
    }
}