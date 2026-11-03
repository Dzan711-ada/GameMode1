using UnityEngine;

public class Spike : MonoBehaviour
{
    public Transform bossTransform; // Boss 的 Transform 引用
    public Vector3 offset; // 偏移量，用于设置尖刺相对于 Boss 的生成位置
    public float activeTime = 3f; // 尖刺存在的时间，单位秒
    public float speed = 10f; // 尖刺的移动速度
    private Vector2 direction; // 尖刺的移动方向
    private float destroyTime; // 尖刺销毁的时间

    private bool isActive = false; // 尖刺是否生效

    void Start()
    {
        // 设置销毁时间
        
    }

    void Update()
    {
        if (isActive)
        {
            // 每帧更新尖刺的位置
            transform.Translate(direction * speed * Time.deltaTime);

        }
    }

    // 激活尖刺，并设定方向
    public void Activate(Vector2 spawnDirection)
    {
        isActive = true;
        direction = -spawnDirection; // 设置尖刺发射的方向
        destroyTime = Time.time + activeTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit by spike!");

            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage(); // 触发玩家受伤逻辑
            }

            Destroy(gameObject); // 销毁尖刺
        }
    }
}
