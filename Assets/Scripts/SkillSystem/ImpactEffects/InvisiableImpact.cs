using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Skill
{
    public class InvisiableImpact : IImpactEffect
    {
        private float DeltaAlpha=0.1f;
        public void Execute(SkillDeployer deployer)
        {

            GameObject player = deployer.SkillData.owner;
            Debug.Log(1);
            SpriteRenderer playerrender=player.GetComponent<SpriteRenderer>();
            SpriteRenderer temp=playerrender;

            float r = temp.color.r; float g = temp.color.g; float b = temp.color.b;
            float alpha=1.0f;
      
            Action task = async () =>
            {
                for(int i = 0; i < 10; i++)
                {
                    await Task.Delay(Mathf.RoundToInt(deployer.SkillData.durationTime * 10));
                    alpha -= DeltaAlpha;
                    playerrender.color = new Color(r, g, b, alpha);
                }
                for (int i = 0; i < 80; i++)
                {
                    await Task.Delay(Mathf.RoundToInt(deployer.SkillData.durationTime * 10));
                   
                }
                for (int i = 0; i < 10; i++)
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