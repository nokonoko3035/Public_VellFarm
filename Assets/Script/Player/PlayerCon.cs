using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System;

public class PlayerCon : MonoBehaviour
{
    private PlayerInput playerInput;

    [Header("移動用Vector")]
    public Vector2 moveInput;               // 移動入力の値
    [Header("移動Speed")]
    public float moveSpeed = 5f;            // プレイヤーの移動速度
    private Rigidbody2D rb;
    private AudioSource sound;              // 音を鳴らすためのコンポーネント

    [Header("音の聞こえる範囲")]
    public GameObject soundSpace;           // 音の範囲を示すオブジェクト
    private CircleCollider2D soundCollider; // 音の当たり判定
    private Transform soundTransform;       // 音の影響範囲（画像の大きさ調整用）

    [Header("拡大設定")]
    public float expandTime = 3f;           // 拡大する時間
    public float maxRadiusMultiplier = 2f;  // 最大の大きさ（元の半径の何倍にするか）
    private Coroutine expandCoroutine;      // コルーチン管理用

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        sound = GetComponent<AudioSource>();

        if (soundSpace != null)
        {
            soundCollider = soundSpace.GetComponent<CircleCollider2D>();
            soundTransform = soundSpace.transform;
        }

        if (rb == null) Debug.LogError("Rigidbody2D が見つかりません: " + gameObject.name);
        if (playerInput == null) Debug.LogError("PlayerInput が見つかりません: " + gameObject.name);
    }

    private void OnEnable()
    {
        if (playerInput == null) return;

        // 左スティックの移動
        var moveAction = playerInput.actions["Move"];
        moveAction.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        moveAction.canceled += ctx => moveInput = Vector2.zero;

        // 音を鳴らす
        var soundAction = playerInput.actions["Sound"];
        soundAction.performed += OnSound;
    }

    private void Update()
    {
        if (rb == null) return;

        MovePlayer();
    }

    private void MovePlayer()
    {
        if (rb == null) return;

        // **左スティックでプレイヤーを移動（上下左右）**
        rb.velocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
    }

    /// <summary>
    /// 音を鳴らす処理
    /// </summary>
    public void OnSound(InputAction.CallbackContext context)
    {
        Debug.Log("Soundが呼ばれました");
        if (context.performed)
        {
            PlaySound();
        }
    }

    /// <summary>
    /// 音を鳴らし、音の範囲を拡大する処理を実行
    /// </summary>
    private void PlaySound()
    { 
        if (sound == null || sound.clip == null) return;
       
        if (sound.isPlaying) return;

        sound.Play();
        Debug.Log("音を鳴らした！");

        if (expandCoroutine != null)
        {
            StopCoroutine(expandCoroutine);
        }

        if (soundSpace != null)
        {
            soundSpace.SetActive(true);
            expandCoroutine = StartCoroutine(ExpandSound(sound.clip.length));
        }
    }

    /// <summary>
    /// 音の聞こえる範囲と画像を拡大する
    /// </summary>
    IEnumerator ExpandSound(float duration)
    {
        if (soundCollider == null || soundTransform == null) yield break;

        // 当たり判定
        float radius = soundCollider.radius;
        float targetRadius = radius * maxRadiusMultiplier;

        // 画像系
        Vector3 scale = soundTransform.localScale;
        Vector3 targetScale = scale * maxRadiusMultiplier;

        float elapsedTime = 0f;

        // 拡大処理
        while (elapsedTime < expandTime)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / expandTime;

            soundCollider.radius = Mathf.Lerp(radius, targetRadius, progress);
            soundTransform.localScale = Vector3.Lerp(scale, targetScale, progress);
            yield return null;
        }

        // 音の再生が終わるまで待機
        yield return new WaitWhile(() => sound.isPlaying);
        soundCollider.radius = radius;
        soundTransform.localScale = scale;
        soundSpace.SetActive(false);
    }

}

