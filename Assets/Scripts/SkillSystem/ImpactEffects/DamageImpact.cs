using UnityEngine;

namespace Skill {
    public class DamageImpact : IImpactEffect {
        public void Execute(SkillDeployer deployer) {
            foreach (Transform target in deployer.SkillData.attackTargets) {
                target.GetComponent<CharacterStatus>().Kill();

                deployer.Destroy();
                return;
            }
        }
    }
}