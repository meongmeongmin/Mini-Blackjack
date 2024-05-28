using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MenuPopup : MonoBehaviour
{
    GameManager _game; 
    public TMP_Text GameResultText;

    void Awake()
    {
        Init();
    }

    void Init()
    {
        _game = Managers.Game;
        GameResultText = GameObject.Find("GameResultText").GetComponent<TMP_Text>();
        _game.OnGameResultTextUpdated += HandleGameResultTextUpdated;
    }

    void HandleGameResultTextUpdated()
    {
        if (_game.Winner == Define.Role.None)
            GameResultText.text = $"Draw!";
        else
            GameResultText.text = $"{_game.Winner} Win!";
    }

    void OnDestroy()
    {
        _game.OnGameResultTextUpdated -= HandleGameResultTextUpdated;
    }

    public void OnClickRestartButton()
    {
        Debug.Log("Restart");
        Managers.Sound.Play(Define.Sound.Effect, "Click_CommonButton");
        SceneManager.LoadScene("GameScene");
    }

    public void OnClickExitButton()
    {
        Debug.Log("Game Exit");
        Managers.Sound.Play(Define.Sound.Effect, "Click_CommonButton");
        Application.Quit();
    }
}
