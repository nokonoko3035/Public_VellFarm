using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChangeButton : MonoBehaviour
{
    [SerializeField] private string targetScene; // 遷移先のシーン名
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ChangeScene);
    }

    private void ChangeScene()
    {
        if (!string.IsNullOrEmpty(targetScene))
        {
            Debug.Log($"シーン {targetScene} へ移動！");
            SceneManager.LoadScene(targetScene);
        }
        else
        {
            Debug.LogWarning("シーン名が設定されていません！");
        }
    }
}
