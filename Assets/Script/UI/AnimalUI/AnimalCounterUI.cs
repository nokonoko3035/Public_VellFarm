using UnityEngine;
using UnityEngine.UI;

public class AnimalCounterUI : MonoBehaviour
{
    public AnimalCounterManager counterManager; // カウント元の参照
    public string tagToDisplay;                // 表示したいタグ（例：Cow / Sheep）
    public Text targetText;                    // 表示先のText

    void Update()
    {
        if (counterManager != null && targetText != null && !string.IsNullOrEmpty(tagToDisplay))
        {
            int count = counterManager.GetCountByTag(tagToDisplay);
            targetText.text = $"{tagToDisplay}: {count}";
        }
    }
}
