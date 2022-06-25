using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Skill
{
    public class InputKeyImpact : IImpactEffect
    {
        Sprite[,] spriteArray = new Sprite[2, 4];
        public void Execute(SkillDeployer deployer)
        {   
            GameObject player = deployer.SkillData.attackTargets[0].gameObject;
            int playernum = player.GetComponent<PlayerController>().PlayerNum;
            InputButton.PlayerNum = playernum;
            
        }
    }
}