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
        #region ��巹���� ���� �ε�
        Addressables.LoadAssetAsync<Sprite>($"{_card.Suit}{_card.Number:D2}.sprite").Completed += handle =>
        {
            if (this == null || gameObject == null) // ������Ʈ �� ���� ������Ʈ�� �ı� ���� Ȯ��
            {
                Debug.LogWarning("UI_Card�� �ı��Ǿ��ִ�.");
                return;
            }

            if (handle.Status == AsyncOperationStatus.Succeeded)
                GetComponent<Image>().sprite = handle.Result;
        };
        #endregion
    }
}
