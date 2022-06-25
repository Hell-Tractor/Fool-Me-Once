using UnityEngine;

namespace Skill
{
    public class CloseImpact : IImpactEffect
    {
        public void Execute(SkillDeployer deployer)
        {

            Transform player1, player2;
            player1 = deployer.SkillData.owner.transform;
            player2 = deployer.SkillData.attackTargets[0];
            player1.position = player2.position;
           
            deployer.Destroy();
        }
    }
}