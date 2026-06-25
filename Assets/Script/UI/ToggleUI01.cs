using UnityEngine;
using UnityEngine.UI;

public class ToggleUI01 : MonoBehaviour
{
    // 切り替える対象の UI（複数指定可）
    public GameObject[] uiElements;

    // ボタンが押されたときに音を鳴らすための AudioSource（任意）
    public AudioSource clickSound;

    // ボタンが押されたときに呼ばれるメソッド
    public void ToggleVisibility()
    {
        // すべての指定 UI を切り替える
        foreach (GameObject ui in uiElements)
        {
            if (ui != null)
            {
                ui.SetActive(!ui.activeSelf); // 表示状態を反転
            }
        }

        // 音を鳴らす（AudioSourceが設定されている場合）
        if (clickSound != null)
        {
            clickSound.Play();
        }
    }
}
