using UnityEngine;
using UnityEngine.UI;

public class GameTimerUI : MonoBehaviour
{
    public Text timeText;           // 表示先の Text UI
    private float elapsedTime = 0f; // 経過時間（秒）

    void Update()
    {
        elapsedTime += Time.deltaTime;
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        if (timeText != null)
        {
            int minutes = Mathf.FloorToInt(elapsedTime / 60f);
            int seconds = Mathf.FloorToInt(elapsedTime % 60f);
            timeText.text = string.Format("経過時間 {0:00}:{1:00}", minutes, seconds);
        }
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }
}
