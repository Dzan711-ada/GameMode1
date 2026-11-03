using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Sickle : MonoBehaviour
{
    public float speed; // 镰刀的移动速度  
    public float rotateSpeed; // 镰刀的旋转速度  
    public int damage; // 镰刀对敌人造成的伤害  
    public float tuning; // 镰刀移动的调整因子  

    private Rigidbody2D rb2d; // 镰刀的 Rigidbody2D 组件的引用  
    private Transform playerTransform; // 玩家 Transform 组件的引用  
    private Transform sickleTransform; // 镰刀自身 Transform 组件的引用  
    private Vector2 startSpeed; // 镰刀的初始速度  

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); // 获取镰刀的 Rigidbody2D 组件  
        rb2d.velocity = transform.right * speed; // 设置镰刀的初始速度  
        startSpeed = rb2d.velocity; // 存储初始速度  
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); // 获取玩家的 Transform 组件  
        sickleTransform = GetComponent<Transform>(); // 获取镰刀自身的 Transform 组件  

    }

    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed); // 旋转镰刀  
        float y = Mathf.Lerp(transform.position.y, playerTransform.position.y, tuning); // 根据调整因子，将镰刀的 y 坐标插值到玩家的 y 坐标  
        transform.position = new Vector3(transform.position.x, y, 0.0f); // 更新镰刀的位置  
        rb2d.velocity = rb2d.velocity - startSpeed * Time.deltaTime; // 减慢镰刀的速度  
        if (Mathf.Abs(transform.position.x - playerTransform.position.x) < 0.5f) // 如果镰刀靠近玩家  
        {
           Destroy(gameObject); // 销毁镰刀  
        }
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Boss>() != null)
            {
                hit.GetComponent<Boss>().Damage();
            }
        }
    }

    /*private void AttackTrigger() 
    {
        Collider2D other;
        Debug.Log($"case0");
        if (other.GetComponent<Boss>() != null)
        {
            Debug.Log($"case1");
        }
        if (other.gameObject.CompareTag("Boss")) // 如果镰刀碰撞到敌人  
        {
            Debug.Log($"case2");
            other.GetComponent<Boss>().Damage();
        }
    }*/
}
