using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // 公共变量  
    public GameObject explosionRange; // 爆炸范围的游戏对象引用  
    public Vector2 startSpeed; // 炸弹的初始速度  
    public float delayExplodeTime; // 炸弹爆炸前的延迟时间  
    public float hitBoxTime; // 生成爆炸范围碰撞箱的时间  
    public float destroyBombTime; // 炸弹对象被销毁的时间  

    // 私有变量  
    private Rigidbody2D rb2d; // Rigidbody2D 组件的引用  
    private Animator anim; // Animator 组件的引用  

    // 在首次帧更新时被调用  
    void Start()
    {
        // 获取 Rigidbody2D 和 Animator 组件的引用  
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // 设置炸弹的初始速度  
        rb2d.velocity = transform.up * startSpeed.y + transform.right * startSpeed.x;

        // 在 delayExplodeTime 后调用 Explode() 函数  
        Invoke("Explode", delayExplodeTime);
    }

    // 每帧被调用  
    void Update()
    {

    }

    void Explode()
    {
        // 触发爆炸动画  
        anim.SetTrigger("Explode");

        // 在 hitBoxTime 后生成爆炸范围  
        //Invoke("GenExplosionRange", hitBoxTime);

        // 在 destroyBombTime 后销毁炸弹对象  
        Invoke("DestroyThisBomb", destroyBombTime);
    }

    /*void GenExplosionRange()
    {
        // 在当前位置生成爆炸范围对象  
        Instantiate(explosionRange, transform.position, Quaternion.identity);
    }*/

    void DestroyThisBomb()
    {
        // 销毁当前炸弹对象  
        Destroy(gameObject);
    }
}