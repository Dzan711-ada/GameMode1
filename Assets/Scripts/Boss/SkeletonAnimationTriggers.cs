using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimationTriggers : MonoBehaviour
{
    private Boss_Skeleton boss => GetComponentInParent<Boss_Skeleton>();

    private void AnimationTrigger()
    {
        boss.AnimationFinishTriggers();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(boss.attackCheck.position, boss.attackeCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                hit.GetComponent<Player>().Damage();
            }
        }
    }
}
