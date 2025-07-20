using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    internal class ShopSystem : IEcsInitSystem, IEcsRunSystem
    {
        readonly SceneData _sceneData;
        readonly UIContainer _container;
        readonly EcsWorld _world;
        readonly EcsFilter<EBuyCardC> _filterBuy;
        readonly EcsFilter<ShopC> _filterShop;
        readonly EcsFilter<EAddMoneyC> _filterMoney;

        public void Init()
        {
            var listCardsData = _sceneData.CardsData;
            var eShop = _world.NewEntity();
            ref var cShop = ref eShop.Get<ShopC>();

            // Загружаем магазин
            // Получаем сохранения
            cShop.Money = 100;
            cShop.Cards = new List<CardInfo>()
            {

            };


            foreach (var item in listCardsData)
            {
            }
        }

        public void Run()
        {
            foreach (var buy in _filterBuy)
                foreach (var shop in _filterShop)
                {
                    ref var cShop = ref _filterShop.Get1(shop);
                    ref var cBuy = ref _filterBuy.Get1(buy);
                    ref var cCard = ref cBuy.Entity.Get<CardC>();

                    if (cShop.Money < 0)
                        cShop.Money = 0;

                    var res = cShop.Money - cCard.Info.Cost >= 0;
                    if (res)
                    {
                        cShop.Money -= cCard.Info.Cost;
                        cCard.CardItem.Button.enabled = false;
                        cCard.Info.IsBay = true;
                        cCard.CardItem.TextCost.text = "Есть!";

                        var cId = cCard.Info.Id;
                        var BuyCardId = cShop.Cards.FindIndex(i => i.Id == cId);

                        if (BuyCardId != -1)
                            cShop.Cards[BuyCardId] = cCard.Info;
                        else
                            cShop.Cards.Add(cCard.Info);
                    }
                }

            foreach (var money in _filterMoney)
            {
                foreach (var shop in _filterShop)
                {
                    var addMoney = _filterMoney.Get1(money).Money;

                    _filterShop.Get1(shop).Money += addMoney;

                    if (_filterShop.Get1(shop).Money > 9999)
                        _filterShop.Get1(shop).Money = 9999;
                }

                _filterMoney.GetEntity(money).Destroy();
            }
        }
    }
}