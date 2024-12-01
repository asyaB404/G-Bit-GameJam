using GameTools.MonoTool.Player;
using UnityEngine;

public class PlayerFallCheck2D : MonoBehaviour
{
    [SerializeField] private PlayerContronal player;  
    // 检测区域的大小（宽和高）
    public Vector2 boxSize = new Vector2(1.0f, 0.1f);

    // 检测区域的中心偏移量
    public Vector2 boxOffset = new Vector2(0, -0.5f);

    public bool isDie;
    public void Die()
    {
        player.Die();
        isDie = true;
    }

    private void Update()
    {
        CheckGround();
    }

    private void CheckGround()
    {
        Vector2 boxCenter = (Vector2)transform.position + boxOffset;
        Collider2D hit = Physics2D.OverlapBox(boxCenter, boxSize, 0f);
        if (hit == null && !isDie)
        {
            Die();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 boxCenter = (Vector2)transform.position + boxOffset;
        Gizmos.DrawWireCube(boxCenter, boxSize);
    }
}