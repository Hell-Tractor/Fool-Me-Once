using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Skill
{
    public class NoJumpImpact : IImpactEffect
    {
        public void Execute(SkillDeployer deployer)
        {
           
            GameObject player = deployer.SkillData.attackTargets[0].gameObject;
            player.GetComponent<PlayerController>().CanJump = false;

            if (InputButton.FinishInputButton == true)
            {
                Debug.Log("NoJumpImpact");

                player.GetComponent<PlayerController>().CanJump = true;
                    
                deployer.Destroy();

                InputButton.FinishInputButton = false;
            }
                
        }
    }
}