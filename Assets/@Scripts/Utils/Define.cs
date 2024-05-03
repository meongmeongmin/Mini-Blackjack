using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum GameState   // 게임 상태
    {
        Ready,
        PlayerTurn,
        DealerTurn,
        Check,
        Ended,
    }

    public enum Role    // 역할
    {
        None,
        Player,
        Dealer,
    }

    public enum CardSuit    // 카드 종류
    {
        Spade,
        Heart,
        Diamond,
        Clover,
    }
}
