using UnityEngine;
using UnityEngine.UI;

public class QuitGameButton : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(QuitGame);
    }

    private void QuitGame()
    {
        Debug.Log("ゲームを終了します。");
        Application.Quit(); // ゲームを終了

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // ?? エディタ内でも終了
#endif
    }
}
