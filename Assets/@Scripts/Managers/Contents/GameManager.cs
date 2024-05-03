using System;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Card
{
    public CardSuit Suit;
    public int Number;

    public Card(CardSuit suit, int number)
    {
        Suit = suit;
        Number = number;
    }
}

public class Deck
{
    public List<Card> Cards = new List<Card>();
    public int Score
    {
        get 
        {
            int score = 0;
            int aceCount = 0;

            foreach (Card card in Cards)
            {
                if (card.Number == 1) // 에이스
                {
                    score += 11;
                    aceCount++;
                }
                else if (card.Number > 10)  // K, Q, J
                    score += 10;
                else
                    score += card.Number;
            }

            while (score > 21 && aceCount > 0)
            {
                score -= 10; // 에이스 11를 1로 계산
                aceCount--;
            }

            return score;
        }
    }
}

public class GameManager
{
    public GameState GameState = GameState.Ready;
    public Role Winner;

    public Deck Player;
    public Deck Dealer;

    public Action<Card, Role, bool> OnCardCalled;
    public Action<Card> OnHiddenCardOpened;
    public Action OnGameEnded;
    public Action OnGameResultTextUpdated;

    public void Init()  // 초기화
    {
        Player = new Deck();
        Dealer = new Deck();

        // 처음에는 플레이어와 딜러는 카드 두 장씩 받는다.
        for (int i = 0; i < 2; i++)
        {
            bool hidden = i == 1 ? true : false; 
            Player.Cards.Add(CallCard(Role.Player));
            Dealer.Cards.Add(CallCard(Role.Dealer, hidden));
        }

        ChangeState(GameState.PlayerTurn);
        CheckBust(Player);
    }

    void ChangeState(GameState state)
    {
        GameState = state;
        switch (GameState) 
        {
            case GameState.Ready:
                Init();
                break;  
            case GameState.PlayerTurn:
                break;
            case GameState.DealerTurn:
                HandleDealerTurn();
                break;
            case GameState.Check:
                CompareScore();
                break;
            case GameState.Ended:
                GameOver();
                break;
        }
    }

    Card CallCard(Role role, bool hidden = false) // 카드를 랜덤으로 한 장 꺼낸다.
    {
        System.Random random = new System.Random();
        
        CardSuit suit = (CardSuit)random.Next(0, 4);
        int num = random.Next(1, 14);
        Card card = new Card(suit, num);

        OnCardCalled?.Invoke(card, role, hidden);
        return card;
    }

    public void Hit()
    {
        Player.Cards.Add(CallCard(Role.Player));
        CheckBust(Player);
    }

    public void Stand()
    {
        ChangeState(GameState.DealerTurn);
    }

    public bool CheckBust(Deck deck)
    {
        if (deck.Score > 21)
        {
            Debug.Log("Bust!");
            Winner = GameState == GameState.PlayerTurn ? Role.Dealer : Role.Player;
            ChangeState(GameState.Ended);
            return true;
        }
        return false;
    }

    public void HandleDealerTurn()
    {
        while (true)
        {
            if (CheckBust(Dealer))
                return;
            if (Dealer.Score >= 17)
                break;

            Dealer.Cards.Add(CallCard(Role.Dealer));
        }

        ChangeState(GameState.Check);
    }

    public void CompareScore()
    {
        if (Player.Score == Dealer.Score)
            Winner = Role.None;
        else if (Player.Score > Dealer.Score)
            Winner = Role.Player;
        else
            Winner = Role.Dealer;
        
        ChangeState(GameState.Ended);
    }

    public void GameOver()
    {
        OnHiddenCardOpened?.Invoke(Dealer.Cards[1]);
        
        if (Winner == Role.None)
            Debug.Log("Draw!");
        else if (Winner == Role.Player)
            Debug.Log("You Win!");
        else
            Debug.Log("You Lose!");

        OnGameEnded?.Invoke();
        OnGameResultTextUpdated?.Invoke();
    }
}