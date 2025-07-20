using Leopotam.Ecs;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Client
{
    sealed class InputSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;
        readonly UIContainer uIContainer = null;
        readonly EcsFilter<FGamePlayC> _filter = null;
        readonly EcsFilter<FCubeMoveC> _filterMove = null;

        private Vector2 _positionStart;
        private Vector2 _positionDelta;
        private Vector2 _positionEnd;
        private int _derection;

        void IEcsRunSystem.Run()
        {
            if (_filterMove.GetEntitiesCount() > 0 || uIContainer.UiStopPanel.activeInHierarchy || EventSystem.current.IsPointerOverGameObject())
                return;

            foreach (var item in _filter)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    SwipeStart();
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    SwipeEnd();

                    var x = _positionDelta.x;
                    var y = _positionDelta.y;

                    if (_positionDelta.magnitude < 40) return;

                    if (x > 0 && Math.Abs(y) < Math.Abs(x)) _derection = 0;
                    else if (y < 0 && Math.Abs(y) > Math.Abs(x)) _derection = 1;
                    else if (x < 0 && Math.Abs(y) < Math.Abs(x)) _derection = 2;
                    else _derection = 3;

                    var e = _world.NewEntity();
                    ref var c1 = ref e.Get<EInputC>();
                    c1.Derection = _derection;

                    Sound.EBlob?.Invoke();
                }
            }
        }


        private void SwipeStart()
        {
            _positionDelta = Vector2.zero;
            _positionEnd = Vector2.zero;

            _positionStart = Input.mousePosition;
        }

        private void SwipeEnd()
        {
            _positionEnd = Input.mousePosition;
            _positionDelta = _positionEnd - _positionStart;
        }
    }
}