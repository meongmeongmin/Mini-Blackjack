using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
    public enum GameState   // ���� ����
    {
        Ready,
        PlayerTurn,
        DealerTurn,
        Check,
        Ended,
    }

    public enum Role    // ����
    {
        None,
        Player,
        Dealer,
    }

    public enum CardSuit    // ī�� ����
    {
        Spade,
        Heart,
        Diamond,
        Clover,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        Max,
    }
}
