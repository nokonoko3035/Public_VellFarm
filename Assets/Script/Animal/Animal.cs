using System.Collections;
using UnityEngine;

[System.Serializable]
public class SoundReaction
{
    public string playerTag;  // どのプレイヤーの音に反応するか
    public bool isAttracting; // 引き寄せるなら true, 逃げるなら false
}

public class Animal : MonoBehaviour
{
    [Header("動物の移動設定")]
    public float moveSpeed = 3f; // 移動速度
    private Rigidbody2D rb;

    [Header("スプライト設定")]
    public Sprite nomalSprite;              // 停止時のスプライト
    public Sprite moveSprite;               // 引き寄せられるときのスプライト
    public Sprite removeSprite;             // 逃げるときのスプライト
    private SpriteRenderer spriteRenderer;

    [Header("音への反応設定")]
    public SoundReaction[] soundReactions;  // どのプレイヤーの音にどう反応するか
    private bool isHearingSound = false;    // 音の範囲内にいるか
    private bool isMovingToPen = false;     // 小屋へ移動中かどうか
    private Vector3 targetSoundPos;         // 音の位置を保存
    private bool isAttracting;              // どちらの動作をするか記録

    private Transform target;               // 目標地点（小屋）

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRendererを取得
    }

    /// <summary>
    /// 音の範囲に入ったときの動作
    /// </summary>
    private void ReactToSound(Vector3 soundPosition, bool isAttracting)
    {
        if (isMovingToPen) return; // すでに小屋へ移動中なら無視

        isHearingSound = true;
        targetSoundPos = soundPosition; // 音が鳴った地点を記録
        this.isAttracting = isAttracting; // どちらの動作をするか記録

        // スプライトを変更（移動アニメーション）
        spriteRenderer.sprite = isAttracting ? moveSprite : removeSprite;

        StopAllCoroutines(); // もし他の移動コルーチンが動いていたら止める
        StartCoroutine(MoveToSoundPosition());
    }

    /// <summary>
    /// 音の鳴った位置に向かって移動する
    /// </summary>
    private IEnumerator MoveToSoundPosition()
    {
        while (isHearingSound)
        {
            Vector3 direction = (targetSoundPos - transform.position).normalized;
            rb.velocity = direction * moveSpeed * (isAttracting ? 1 : -1); // 逃げる or 寄る

            // 目標地点にほぼ到達したら停止
            if (Vector3.Distance(transform.position, targetSoundPos) < 0.1f)
            {
                StopMoving();
                yield break; // コルーチン終了
            }

            yield return null;
        }
    }

    /// <summary>
    /// 音の影響範囲から出たときに停止
    /// </summary>
    public void StopMoving()
    {
        isHearingSound = false;
        rb.velocity = Vector2.zero;

        // 停止時のスプライトに戻す
        spriteRenderer.sprite = nomalSprite;
    }

    /// <summary>
    /// 小屋に移動する
    /// </summary>
    public void MoveToPen(Transform pen)
    {
        target = pen;
        isMovingToPen = true;
    }

    /// <summary>
    /// 小屋に入ったら削除 & 音の範囲で動作開始
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(this.CompareTag(other.tag));
        // 小屋に入ったら削除
        if (other.tag == this.tag + "Pen") 
        {
            Destroy(gameObject);
            return;
        }

        // 小屋に入っていない場合、プレイヤーのタグを確認
        foreach (var reaction in soundReactions)
        {
            if (other.CompareTag(reaction.playerTag)) // プレイヤーのタグと一致するか
            {
                Vector3 soundPosition = other.transform.parent != null ? other.transform.parent.position : other.transform.position;
                ReactToSound(soundPosition, reaction.isAttracting);
                break;
            }
        }
    }

    /// <summary>
    /// 音の範囲から出たら停止
    /// </summary>
    private void OnTriggerExit2D(Collider2D other)
    {
        foreach (var reaction in soundReactions)
        {
            if (other.CompareTag(reaction.playerTag)) // プレイヤーのタグを確認
            {
                StopMoving();
                break;
            }
        }
    }
}
