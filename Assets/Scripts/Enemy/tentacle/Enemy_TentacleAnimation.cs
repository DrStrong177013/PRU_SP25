using UnityEngine;

public class Enemy_TentacleAnimation : MonoBehaviour
{
    private Enemy_Tentacle enemy => GetComponentInParent<Enemy_Tentacle>();

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }
    private void AttackTrigger(){}
    private void OpenCounterWindow() => enemy.OpenCounterAttackWindow();
    private void CloseCounterWindow() => enemy.CloseCounterAttackWindow();
}
