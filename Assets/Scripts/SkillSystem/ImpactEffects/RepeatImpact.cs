using UnityEngine;

namespace Skill {
    public class RepeatImpact : IImpactEffect {
        public void Execute(SkillDeployer deployer) {
            int? lastSkillId = deployer.SkillData.attackTargets[0].GetComponent<CharacterSkillManager>().LastSkillId;
            if (lastSkillId != null) {
                CharacterSkillManager manager = deployer.SkillData.owner.GetComponent<CharacterSkillManager>();
                SkillData data = manager.PrepareSkill(lastSkillId.Value);
                if (data != null) {
                    manager.GenerateSkill(data);
                }
            }
            deployer.Destroy();
        }
    }
}