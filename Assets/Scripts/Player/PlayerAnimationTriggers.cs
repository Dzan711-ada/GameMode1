using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position,player.attackeCheckRadius);

        foreach(var hit  in colliders)
        {
            if(hit.GetComponent<Boss>() != null)
            {
                hit.GetComponent<Boss>().Damage();
            }
        }
    }
}
