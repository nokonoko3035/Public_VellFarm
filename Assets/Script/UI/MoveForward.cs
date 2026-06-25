using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public Vector2 direction = Vector2.right; //  進む方向（デフォルトは右）
    public float speed = 5f; // 進む速度

    void Update()
    {
        transform.position += (Vector3)direction.normalized * speed * Time.deltaTime;
        // 指定方向へ進み続ける！
    }
}
