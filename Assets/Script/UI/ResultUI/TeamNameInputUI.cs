using UnityEngine;
using UnityEngine.UI;

public class TeamNameInputUI : MonoBehaviour
{
    [Header("名前入力UI")]
    public GameObject inputPanel;         // 名前入力用のパネル（InputField＋ボタン付き）
    public InputField nameInput;          // チーム名を入力するフィールド
    public Button submitButton;           // 「登録」ボタン

    [Header("リザルトUI")]
    public GameObject resultPanel;        // ランキング表示パネル
    public ResultUIManager resultUI;      // ランキング表示管理スクリプト

    private float pendingTime;            // 登録予定のクリアタイム
    private int pendingRankIndex;         // 登録予定のランクイン位置

    public RankingManager rankingManager; // ランキング管理スクリプト

    void Start()
    {
        // 初期状態ではUIを非表示にする
        inputPanel.SetActive(false);
        resultPanel.SetActive(false);
        // ボタンにイベント登録
        submitButton.onClick.AddListener(OnSubmit);
    }

    /// <summary>
    /// ランクインが確定したときに呼ばれ、入力UIを表示する
    /// </summary>
    public void OpenInputPanel(float time, int rankIndex)
    {
        pendingTime = time;
        pendingRankIndex = rankIndex;
        inputPanel.SetActive(true);
    }

    /// <summary>
    /// 登録ボタンが押されたときに実行される処理。
    /// 入力された名前でランキングに登録し、リザルト表示に進む。
    /// </summary>
    void OnSubmit()
    {
        string teamName = nameInput.text;

        // 空文字なら無視
        if (string.IsNullOrEmpty(teamName))
            return;

        // ランキングに登録
        rankingManager.RegisterRanking(teamName, pendingTime, pendingRankIndex);
        // 入力UIを閉じて、リザルトへ
        inputPanel.SetActive(false);
        ShowResultOnly();
    }

    /// <summary>
    /// リザルトパネルを表示し、ランキングを描画する
    /// </summary>
    public void ShowResultOnly()
    {
        resultPanel.SetActive(true);
        resultUI.DisplayRanking(rankingManager.GetRankingList());
    }
}
