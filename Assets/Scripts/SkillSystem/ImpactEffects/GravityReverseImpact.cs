using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Skill {
    public class GravityReverseImpact : IImpactEffect {
        public void Execute(SkillDeployer deployer) {
            _reverse(deployer);

            Action task = async () => {
                await Task.Delay(Mathf.RoundToInt(deployer.SkillData.durationTime * 1000));
                _reverse(deployer);
            };
            task.Invoke();
        }

        private void _reverse(SkillDeployer deployer) {
            foreach (Transform target in deployer.SkillData.attackTargets) {
                // 反转目标重力方向
                target.GetComponent<Rigidbody2D>().gravityScale *= -1;
                // 反转目标跳跃方向
                target.GetComponent<PlayerController>().jumpForce *= -1;
            }
            // 反转平台
            GameObject[] plaforms = GameObject.FindGameObjectsWithTag("Platform");
            foreach (GameObject platform in plaforms) {
                platform.transform.localScale = new Vector3(
                    platform.transform.localScale.x,
                    platform.transform.localScale.y * -1,
                    platform.transform.localScale.z
                );
            }
        }
    }
}