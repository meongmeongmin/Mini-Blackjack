using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class UI_GameScene : MonoBehaviour
{
    public Transform Player;
    public Transform Dealer;

    public GameObject HiddenCard;
    public GameObject UI_MenuPopup;

    public TMP_Text PlayerScoreText;
    public TMP_Text DealerScoreText;

    GameManager _game;

    void Awake()
    {
        Init();
    }

    void Update()
    {
        PlayerScoreText.text = $"{_game.Player.Score}";
        if (_game.GameState == Define.GameState.PlayerTurn)
            DealerScoreText.text = _game.Dealer.Cards[0].Number > 10 ? $"10" : $"{_game.Dealer.Cards[0].Number}";
        else
            DealerScoreText.text = $"{_game.Dealer.Score}";
    }

    public void Init()
    {
        Player = GameObject.Find("Player").transform;
        Dealer = GameObject.Find("Dealer").transform;
        UI_MenuPopup = GameObject.Find("UI_MenuPopup");
        PlayerScoreText = GameObject.Find("PlayerScoreText").GetComponent<TMP_Text>();
        DealerScoreText = GameObject.Find("DealerScoreText").GetComponent<TMP_Text>();

        UI_MenuPopup.SetActive(false);

        _game = Managers.Game;
        _game.OnCardCalled += HandleCardCalled;
        _game.OnHiddenCardOpened += HandleHiddenCardOpened;
        _game.OnGameEnded += HandleGameEnded;
    }

    void OnDestroy()
    {
        _game.OnCardCalled -= HandleCardCalled;
        _game.OnHiddenCardOpened -= HandleHiddenCardOpened;
        _game.OnGameEnded -= HandleGameEnded;
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

    void InstantiateCard(GameObject prefab, Transform parent, Card card, bool hidden)
    {
        if (hidden)
        {
            // HiddenCard = UnityEngine.Object.Instantiate(prefab, parent);
            HiddenCard = Managers.Pool.Pop(prefab);
            HiddenCard.transform.SetParent(parent);
            HiddenCard.transform.localScale = Vector3.one;
        }
        else
        {
            // GameObject go = UnityEngine.Object.Instantiate(prefab, parent);
            GameObject go = Managers.Pool.Pop(prefab);
            go.transform.SetParent(parent);
            UI_Card uc = go.GetOrAddComponent<UI_Card>();
            uc.SetInfo(card);
        }
    }

    void HandleCardCalled(Card card, Define.Role role, bool hidden = false)
    {
        //#region 어드레서블 에셋 로드
        //Addressables.LoadAssetAsync<GameObject>("Card").Completed += handle =>
        //{
        //    if (handle.Status == AsyncOperationStatus.Succeeded)
        //    {
        //        Transform parent = role == Define.Role.Player ? Player : Dealer;
        //        if (handle.Result == null || parent == null)
        //            return;

        //        InstantiateCard(handle.Result, parent, card, hidden);
        //    }
        //};
        //#endregion
        GameObject go = Managers.Resource.Load<GameObject>("Card");
        Transform parent = role == Define.Role.Player ? Player : Dealer;
        if (go != null && parent != null)
            InstantiateCard(go, parent, card, hidden);
    }

    void HandleHiddenCardOpened(Card card)
    {
        UI_Card uc = HiddenCard.GetOrAddComponent<UI_Card>();
        uc.SetInfo(card);
    }

    void HandleGameEnded()
    {
        UI_MenuPopup.SetActive(true);
    }
}