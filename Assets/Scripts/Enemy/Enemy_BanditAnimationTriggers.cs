using UnityEngine;

public class Enemy_BanditAnimationTriggers : MonoBehaviour
{
    private Enemy_Bandit enemy => GetComponentInParent<Enemy_Bandit>();

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
                hit.GetComponent<Player>().Damage();
        }
    }
}
