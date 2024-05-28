using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MenuScene : MonoBehaviour
{
    bool isPreload = false;

    void Awake()
    {
        Managers.Resource.LoadAllAsync<Object>("PreLoad", (key, count, totalCount) =>
        {
            if (count == totalCount)
                isPreload = true;
        });
    }

    public void OnClickStartButton()
    {
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
