using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Skill {
    public class ParryImpact : IImpactEffect {
        public void Execute(SkillDeployer deployer) {


            PlayerController controller = deployer.SkillData.owner.GetComponent<PlayerController>();
            controller.isParrying = true;

            Action task = async () =>
            {
                await Task.Delay(Mathf.RoundToInt(deployer.SkillData.durationTime * 1000));
                controller.isParrying = false;
                deployer.Destroy();
            };
            task.Invoke();
        }
    }
}