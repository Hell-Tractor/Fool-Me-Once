using System.Collections;
using UnityEngine;

namespace Skill
{
    public class QQBallDeployer : SkillDeployer
    {
        public float Speed = 3.0f;
        private Vector3 _startPosition;

        public override void DeploySkill()
        {
            Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
            GameObject owner = this.SkillData.owner;
            CharacterStatus status = owner.GetComponent<CharacterStatus>();

            // 在玩家前方生成
            rb.MovePosition(owner.transform.position + (new Vector3((owner.transform.localScale.x + transform.localScale.x) * status.Direction * 0.6f, 0, 0)));
            // 设置移动速度
            rb.velocity = (new Vector3(status.Direction, 0, 0)) * Speed;
            // 记录初始位置
            _startPosition = this.transform.position;

            StartCoroutine(_update());
        }

        private IEnumerator _update()
        {
            // 距离判定
            while (Vector3.Distance(_startPosition, this.transform.position) < this.SkillData.attackDistance)
            {
                _calculateTargets();
                _impactTargets();
                yield return new WaitForEndOfFrame();
            }
            Destroy(this.gameObject);
        }
    }
}