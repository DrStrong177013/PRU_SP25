using UnityEngine;

public class Enemy_BoneWizardTriggers : Enemy_AnimationTriggers
{
    private Enemy_BoneWizard enemyBoneWizard => GetComponentInParent<Enemy_BoneWizard>();

    private void Relocate() => enemyBoneWizard.FindPosition();

    private void MakeInvisible() => enemyBoneWizard.fx.MakeTransprent(true);
    private void MakeVisible() => enemyBoneWizard.fx.MakeTransprent(false);
}
