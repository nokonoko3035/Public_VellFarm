using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResultData : MonoBehaviour
{
    public static GameResultData Instance;

    public float clearTime;
    public int cnt = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); // ÉVÅ[ÉìÇÇ‹ÇΩÇ¢Ç≈Ç‡è¡Ç≥Ç»Ç¢
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

