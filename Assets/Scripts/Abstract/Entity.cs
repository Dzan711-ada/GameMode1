using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion

    public int facingDir { get; private set; }
    protected bool facingRight = true;

    public void InitializeFacingDirection(int initialDirection)
    {
        facingDir = initialDirection;
        facingRight = (initialDirection == 1);
        transform.rotation = Quaternion.Euler(0, initialDirection == 1 ? 0 : 180, 0);
    }

    [Header("Health Info")]
    [SerializeField] private int maxHealth = 10; // 最大生命值
    [SerializeField] private int currentHealth; // 当前生命值


    [Header("Knockback info")]
    [SerializeField] protected Vector2 KnockbackDir;
    protected bool isKnocked;
    [SerializeField] protected float knockbackDuration;

    [Header("Damage Protection")]
    [SerializeField] private float damageCooldown = 1.5f; // 受击保护时间
    private bool isDamageProtected = false; // 是否处于受击保护状态



    [Header("Collision info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask WhatIsGround;
    [SerializeField] public Transform sp;

    [Header("Attack details")]
    public Transform attackCheck;
    public float attackeCheckRadius;
    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {

    }

    private IEnumerator DamageCooldown()
    {
        isDamageProtected = true;

        // 示例：闪烁效果
        SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
        for (int i = 0; i < 5; i++) // 闪烁 5 次
        {
            sprite.color = new Color(1, 0, 0, 0.5f); // 半透明红色
            yield return new WaitForSeconds(0.1f);
            sprite.color = Color.white; // 恢复原色
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(damageCooldown - 1f); // 剩余冷却时间
        isDamageProtected = false;
    }

    public virtual void Damage()
    {
        if (isDamageProtected) return; // 如果处于保护状态，直接返回

        currentHealth -= 2; // 每次受击扣除 2 点生命值
        Debug.Log($"Entity took damage! Current Health: {currentHealth}");
        StartCoroutine(HitKnockback());
        StartCoroutine(DamageCooldown());
        if (currentHealth <= 0)
        {
            Die(); // 调用死亡处理逻辑
            return;
        }
 // 启动受击保护协程
    }
    #region Flip
    public virtual void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    protected virtual void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
        {
            Flip();
        }
        if (_x < 0 && facingRight)
        {
            Flip();
        }
    }
    #endregion

    #region Collision
   
    protected virtual IEnumerator HitKnockback()
    {
        isKnocked = true;
        anim.SetBool("isKnocked", true); // 设置受击动画状态

        rb.velocity = new Vector2(KnockbackDir.x * -facingDir, KnockbackDir.y); // 应用击退力

        yield return new WaitForSeconds(knockbackDuration); // 等待击退时间

        isKnocked = false;
        anim.SetBool("isKnocked", false); // 恢复正常状态
    }
    public virtual bool isGroundedDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, WhatIsGround);
    public virtual bool isWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, WhatIsGround);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position,attackeCheckRadius);
    }

    #endregion

    public void SetZeroVelocity()
    {
        if(isKnocked)
            return;
        rb.velocity = new Vector2(0, 0);
    }
   
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        if (isKnocked)
            return;
        rb.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }
    protected virtual void Die()
    {
        Debug.Log("Entity has died!");
        anim.SetTrigger("Die"); // 触发死亡动画
        Destroy(gameObject, 2f); // 2 秒后销毁实体
    }

}
