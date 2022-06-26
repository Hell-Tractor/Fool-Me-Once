using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Skill {
    public class ReviveImpact : IImpactEffect {
        public void Execute(SkillDeployer deployer) {
            PlayerController controller = deployer.SkillData.owner.GetComponent<PlayerController>();
            controller.isInvincible = true;

            Action task = async () => {
                await Task.Delay(Mathf.RoundToInt(deployer.SkillData.durationTime * 1000));
                controller.isInvincible = false;
                controller.Kill(null);
                deployer.Destroy();
            };
            task.Invoke();
        }
    }
}