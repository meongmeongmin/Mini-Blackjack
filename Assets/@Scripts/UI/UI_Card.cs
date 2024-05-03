using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class UI_Card : MonoBehaviour
{
    Card _card;

    public void SetInfo(Card card)
    {
        _card = card;
        #region 어드레서블 에셋 로드
        Addressables.LoadAssetAsync<Sprite>($"{_card.Suit}{_card.Number:D2}.sprite").Completed += handle =>
        {
            if (this == null || gameObject == null) // 컴포넌트 및 게임 오브젝트의 파괴 여부 확인
            {
                Debug.LogWarning("UI_Card가 파괴되어있다.");
                return;
            }

            if (handle.Status == AsyncOperationStatus.Succeeded)
                GetComponent<Image>().sprite = handle.Result;
        };
        #endregion
    }
}
