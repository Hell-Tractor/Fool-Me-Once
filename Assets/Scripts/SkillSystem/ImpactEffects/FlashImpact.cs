using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Skill
{
    public class FlashImpact : IImpactEffect
    {
        private float DeltaAlpha = 0.1f;
        public void Execute(SkillDeployer deployer)
        {
            //λ������
            GameObject player = deployer.SkillData.owner;
           
            Transform playerTf = deployer.SkillData.owner.transform;

            int player1direction = player.GetComponent<PlayerController>().Direction;

            Vector3 flashVector = new Vector3(player1direction * deployer.SkillData.attackDistance, 0, 0);

            Vector3 tempVector = playerTf.position;

            SpriteRenderer playerrender = player.GetComponent<SpriteRenderer>();
            SpriteRenderer temp = playerrender;

            float r = temp.color.r; float g = temp.color.g; float b = temp.color.b;
            float alpha = 1.0f;
          
            Action task = async () =>
            {
            //    Debug.Log(1);
                for (int i = 0; i < 30; i++)
                {
                    await Task.Delay(Mathf.RoundToInt(deployer.SkillData.durationTime * 10));
                    alpha -= DeltaAlpha;
                    playerrender.color = new Color(r, g, b, alpha);
                    playerTf.position = tempVector;
                }

                playerTf.position = playerTf.position + flashVector;
             //   Debug.Log(playerTf.position);
                for (int i = 0; i < 40; i++)
                {
                    await Task.Delay(Mathf.RoundToInt(deployer.SkillData.durationTime * 10));

                }

                for (int i = 0; i < 30; i++)
                {
                    await Task.Delay(Mathf.RoundToInt(deployer.SkillData.durationTime * 10));
                    alpha += DeltaAlpha;
                    playerrender.color = new Color(r, g, b, alpha);
                    
                }
                deployer.Destroy();
            };
            task.Invoke();

        }
    }
}