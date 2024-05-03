using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    GameManager _game;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        _game = Managers.Game;
        _game.Init();
    }

    public void OnClickHitButton()
    {
        Debug.Log("Hit");
        _game.Hit();
    }

    public void OnClickStandButton()
    {
        Debug.Log("Stand");
        _game.Stand();
    }
}
