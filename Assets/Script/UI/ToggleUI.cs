using UnityEngine;
using UnityEngine.UI;

public class ToggleUI : MonoBehaviour
{
    public Button myButton; //  操作するボタン
    public GameObject targetUI; //  表示 / 非表示を切り替える UI

    void Start()
    {
        if (myButton != null && targetUI != null)
        {
            myButton.onClick.AddListener(ToggleVisibility); //  ボタンが押されたら切り替え！
        }
        else
        {
            Debug.LogError(" ボタンまたは UI が設定されてないぞ！");
        }
    }

    void ToggleVisibility()
    {
        if (targetUI != null)
        {
            targetUI.SetActive(!targetUI.activeSelf); //  現在の状態を反転！
        }
    }
}
