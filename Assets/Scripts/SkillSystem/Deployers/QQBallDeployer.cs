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

            // �����ǰ������
            rb.MovePosition(owner.transform.position + (new Vector3((owner.transform.localScale.x + transform.localScale.x) * status.Direction * 0.6f, 0, 0)));
            // �����ƶ��ٶ�
            rb.velocity = (new Vector3(status.Direction, 0, 0)) * Speed;
            // ��¼��ʼλ��
            _startPosition = this.transform.position;

            StartCoroutine(_update());
        }

        private IEnumerator _update()
        {
            // �����ж�
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