using System.Collections.Generic;
using UnityEngine;

public class ResultSceneManager : MonoBehaviour
{
    public RankingManager rankingManager;
    public TeamNameInputUI teamNameInputUI;
    void Start()
    {
        // 結果データが存在しているか確認
        if (GameResultData.Instance != null)
        {
            Debug.Log("GameResultDataありましました");
            float time = GameResultData.Instance.clearTime;

            int inRanking = rankingManager.CheckRank(time);
            if (inRanking != -1) {
                teamNameInputUI.OpenInputPanel(time, inRanking);
            }
            else
            {
                teamNameInputUI.ShowResultOnly();
            }
            // シーン遷移後はもう使わないので削除
            Destroy(GameResultData.Instance.gameObject);
        }
        else
        {
            Debug.LogWarning("GameResultData が存在しません！スキップ扱いにします");
        }
    }
}
