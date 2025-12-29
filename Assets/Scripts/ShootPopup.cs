using UnityEngine;

/// <summary>
/// 射击弹出效果类，用于创建向上移动并自动销毁的弹出对象
/// </summary>
public class ShootPopup : MonoBehaviour
{
    private float destroyTimer = 1f;

    /// <summary>
    /// Unity更新方法，处理弹出对象的移动和销毁逻辑
    /// </summary>
    private void Update()
    {
        // 向上移动弹出对象
        float moveSpeed = 2f;
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        
        // 倒计时并检查是否需要销毁对象
        destroyTimer -= Time.deltaTime;
        if (destroyTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }
}