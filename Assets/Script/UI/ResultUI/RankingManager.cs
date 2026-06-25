using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ランキングに登録される1件分の情報（チーム名とタイム）
/// </summary>
[System.Serializable]
public class RankingEntry
{
    public string teamName;  // チーム名
    public float time;       // タイム（秒）

    public RankingEntry(string name, float t)
    {
        teamName = name;
        time = t;
    }
}

public class RankingManager : MonoBehaviour
{
    [Header("最大登録数")]
    public int maxRank = 3;  // ランキング上位の保存数（デフォルト3位まで）

    private List<RankingEntry> rankingList = new List<RankingEntry>(); // 実際のランキングデータ

    // 保存用のPlayerPrefsキー
    private const string NAME_KEY = "Rank_Name_";
    private const string TIME_KEY = "Rank_Time_";

    void Awake()
    {
        LoadRanking(); // ゲーム起動時に保存データを読み込む
    }

    /// <summary>
    /// 現在のランキングにこのタイムが入るかを判定する
    /// ランクインするなら挿入位置（0〜2）を返し、しないなら -1
    /// </summary>
    public int CheckRank(float time)
    {
        for (int i = 0; i < rankingList.Count; i++)
        {
            if (time < rankingList[i].time)
                return i; // タイムが良ければここに挿入
        }

        // ランキングにまだ空きがある場合は末尾に追加可能
        if (rankingList.Count < maxRank)
            return rankingList.Count;

        return -1; // ランクイン不可
    }

    /// <summary>
    /// 指定のランク位置に、チーム名とタイムを登録して保存する
    /// </summary>
    public void RegisterRanking(string name, float time, int rankIndex)
    {
        Debug.Log("ランキングを登録しました");
        // 指定位置に挿入
        rankingList.Insert(rankIndex, new RankingEntry(name, time));

        // 上限を超えたら末尾を削除
        if (rankingList.Count > maxRank)
            rankingList.RemoveAt(rankingList.Count - 1);

        SaveRanking(); // 保存処理
    }

    /// <summary>
    /// 現在のランキングリストを外部に渡す（読み取り専用）
    /// </summary>
    public List<RankingEntry> GetRankingList()
    {
        return rankingList;
    }

    /// <summary>
    /// 保存されたランキングデータを読み込む
    /// </summary>
    private void LoadRanking()
    {
        rankingList.Clear(); // 初期化

        for (int i = 0; i < maxRank; i++)
        {
            // 保存されていなければデフォルト（名前は "---"、タイムは最大値）
            string name = PlayerPrefs.GetString(NAME_KEY + i, "---");
            float time = PlayerPrefs.GetFloat(TIME_KEY + i, float.MaxValue);
            // 有効なタイムなら追加（未設定タイムは除外）
            if (time < float.MaxValue)
            {
                rankingList.Add(new RankingEntry(name, time));
            }
        }
    }

    /// <summary>
    /// 現在のランキングリストを PlayerPrefs に保存する
    /// </summary>
    private void SaveRanking()
    {
        PlayerPrefs.Save();
        for (int i = 0; i < rankingList.Count; i++)
        {
            PlayerPrefs.SetString(NAME_KEY + i, rankingList[i].teamName);
            PlayerPrefs.SetFloat(TIME_KEY + i, rankingList[i].time);
        }
    }
}
