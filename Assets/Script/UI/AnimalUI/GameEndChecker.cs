using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndChecker : MonoBehaviour
{
    [Header("監視する動物のタグ")]
    public string[] animalTags = { "Cow", "Sheep" }; // 対象の動物タグ（複数対応）

    [Header("参照するスクリプト")]
    public GameTimerUI timer;                         // 経過時間を取得するタイマー

    private bool isGameEnded = false;                 // ゲーム終了フラグ（二重実行防止）
    void Update()
    {
        if (isGameEnded) return;

        bool allGone = true;

        // すべてのタグに対応するオブジェクトが消えているか確認
        foreach (string tag in animalTags)
        {
            Debug.Log(1);
            if (GameObject.FindGameObjectsWithTag(tag).Length > 0)
            {
                
                allGone = false;
                break;
            }
        }

        // 全ていなくなっていたらゲーム終了処理を実行
        if (allGone)
        {
            Debug.Log("EndGameを呼びました");
            EndGame();
        }
    }

    /// <summary>
    /// ゲーム終了時に呼ばれる処理。
    /// クリアタイムを取得し、ランキング判定またはリザルト表示を行う。
    /// </summary>
    void EndGame()
    {
        isGameEnded = true;
        
        //ここGameResultDataを作成して値を保存
        GameObject go = new GameObject("GameResultData");
        GameResultData data = go.AddComponent<GameResultData>();
        data.clearTime = timer.GetElapsedTime();

        //ResultSceneへ移動
        SceneManager.LoadScene("ResultScene");
    }
}
