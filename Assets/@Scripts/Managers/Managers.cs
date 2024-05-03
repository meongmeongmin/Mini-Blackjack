using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }
    static bool s_init = false;

    GameManager _game = new GameManager();
    public static GameManager Game { get { return Instance?._game; } }

    static void Init()
    {
        if (s_init == false)
        {
            s_init = true;

            GameObject go = GameObject.Find("@Managers");
            if (go == null)
                go = new GameObject() { name = "@Managers" };

            s_instance = go.GetOrAddComponent<Managers>();
            DontDestroyOnLoad(go);
        }
    }
}
