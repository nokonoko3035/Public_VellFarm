using UnityEngine;

public class CameraFollowTwoTargets : MonoBehaviour
{
    public Transform targetA;  // 1つ目のターゲット
    public Transform targetB;  // 2つ目のターゲット
    public float smoothSpeed = 0.1f;  // カメラ移動のスムーズさ
    public float minZoom = 5f;  // 最小ズーム距離
    public float maxZoom = 15f; // 最大ズーム距離
    public float zoomFactor = 0.5f; // 距離に応じたズーム変化の強さ

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
        if (!cam) Debug.LogError("カメラコンポーネントが見つからない！");
    }

    private void LateUpdate()
    {
        if (!targetA || !targetB) return;

        // ① 二つのターゲットの中央を計算
        Vector3 middlePoint = (targetA.position + targetB.position) / 2f;

        // ② スムーズにカメラを移動
        Vector3 smoothPosition = Vector3.Lerp(transform.position, new Vector3(middlePoint.x, middlePoint.y, transform.position.z), smoothSpeed);
        transform.position = smoothPosition;

        // ③ 二つのターゲット間の距離を計算
        float distance = Vector3.Distance(targetA.position, targetB.position);

        // ④ 距離に応じたズーム調整
        float newZoom = Mathf.Clamp(minZoom + distance * zoomFactor, minZoom, maxZoom);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime * smoothSpeed * 10);
    }
}
