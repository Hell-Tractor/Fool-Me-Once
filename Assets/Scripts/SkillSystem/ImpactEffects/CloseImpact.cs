using System;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;

namespace Skill
{
    public class CloseImpact : IImpactEffect
    {
        public void Execute(SkillDeployer deployer)
        {
            
            Rigidbody2D player1, player2;
            player1 = deployer.SkillData.owner.transform.GetComponent<Rigidbody2D>();
            player2 = deployer.SkillData.attackTargets[0].GetComponent<Rigidbody2D>();
            player1.GetComponent<PlayerController>().lockDirection=true;
            player2.GetComponent<PlayerController>().lockDirection = false;
            Action task = async () =>
            {
                for(int i = 0; i < 1000; i++)
                {
                    await Task.Delay(Mathf.RoundToInt(deployer.SkillData.durationTime * 1f));
                    int player2direction = player2.GetComponent<PlayerController>().Direction;
                    int player1direction = player1.GetComponent<PlayerController>().Direction;
                    Vector2 vecDirection = new Vector2(-player2direction, 0);
               
                    player1.position = player2.position + vecDirection;
                    player1.GetComponent<PlayerController>().Direction = -player2.GetComponent<PlayerController>().Direction;
                }
                player1.GetComponent<PlayerController>().lockDirection = false;
                deployer.Destroy();
            };
            task.Invoke();
        }

        //private IEnumerator _close()
        //{
        //    yield return new wai
        //}
    }
}