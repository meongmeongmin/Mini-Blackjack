using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    void Start()
    {
        Managers.Sound.Play(Define.Sound.Bgm, "Bgm_Game");
    }
}
