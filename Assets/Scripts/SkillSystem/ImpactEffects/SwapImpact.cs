using UnityEngine;

namespace Skill {
    public class SwapImpact : IImpactEffect {
        public void Execute(SkillDeployer deployer) {
            Transform player1, player2;
            player1 = deployer.SkillData.owner.transform;
            player2 = deployer.SkillData.attackTargets[0];
            Vector3 temp = player1.position;
            player1.position = player2.position;
            player2.position = temp;
            
            deployer.Destroy();
        }
    }
}