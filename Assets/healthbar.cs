using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{
    private float lerpSpeed = 3f;
    public Image Bar;
    private Player player;

    private void Start()
    {
        // 查找 Player 对象
        player = GameObject.FindObjectOfType<Player>();
        if (player == null)
        {
            Debug.LogError("Player not found in the scene!");
            return;
        }
        // 初始化血量为 Player 的初始血量
        // currentHealth = maxHealth; 不再需要，从 Player 获取
    }

    private void Barfiller()
    {
        if (player == null || Bar == null)
        {
            Debug.LogError("Player or Bar is not assigned!");
            return;
        }
        // 从 Player 获取血量信息
       // float currentHealth = player.currentHealth;
       // float maxHealth = player.maxHealth;
        //Bar.fillAmount = Mathf.Lerp(Bar.fillAmount, currentHealth / maxHealth, Time.deltaTime * lerpSpeed);
    }

    private void Update()
    {
        Barfiller();
    }

    // 受伤方法，减少 Player 的血量
    public void TakeDamage(float damageAmount)
    {
        if (player == null)
        {
            Debug.LogError("Player not found, cannot take damage.");
            return;
        }
       // player.TakeDamage(damageAmount);
    }

    // 恢复 Player 的血量
    public void Heal(float healAmount)
    {
        if (player == null)
        {
            Debug.LogError("Player not found, cannot heal.");
            return;
        }
       // player.Heal(healAmount);
    }
}