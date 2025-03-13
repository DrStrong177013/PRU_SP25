using UnityEngine;

public class BoneWizardSpellCastState : EnemyState
{
    private Enemy_BoneWizard enemy;
    public BoneWizardSpellCastState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_BoneWizard _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }
}
