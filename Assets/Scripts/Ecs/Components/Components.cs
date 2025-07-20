using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

public struct TimerC
{
    public float MaxTime;
    public float CurentTime;

    public void Init(float maxTime)
    {
        MaxTime = CurentTime = maxTime;
    }
}

[System.Serializable]
public struct ShopC
{
    public int Money;
    public List<CardInfo> Cards;
}
[System.Serializable]
public struct CardInfo
{
    public int Id;
    public int Cost;
    public bool IsBay;
}

public struct CardC
{
    public CardInfo Info;
    public CardItem CardItem;
}


public struct CubeC
{
    public Transform Transform;
    public int IdPlatformDirection;
    public int IdPlatformPosition;
}

public struct PlatformC
{
    public Transform Transform;
    public List<Vector3[]> ListPositions;
    public int CountCubesOnCentr;
    public bool IsInit;
    public int CountCubeInit;
}

public struct ScoreC
{
    public int BestCount;
    public int TempCount;
    public int Lvl;
    public int MoneyLvl;
}

public struct EAddMoneyC
{
    public int Money;
}
public struct EBuyCardC
{
    public EcsEntity Entity;
}
public struct EInputC
{
    public int Derection;
}
public struct ECubeOnTriggerEnterC
{
    public Collider other;
}
public struct EInitGameC { }
public struct EWinC { }
public struct ELoseC { }
public struct EDestroyC { }
public struct ECubeEndAnimationStartC { }
public struct ECubeEndMoveC { }
public struct EAddScoreC{}
public struct ECheckScoreC { }
public struct EClearScoreC { }
public struct ESaveC { }

public struct FTimerPlayC { }
public struct FWinC { }
public struct FCubeMoveC { }
public struct FCubeBlockC { }
public struct FGameMenuC { }
public struct FGamePlayC { }
