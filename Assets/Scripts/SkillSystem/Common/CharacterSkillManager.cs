using System.Collections;
using System.Linq;
using UnityEngine;

namespace Skill {
    /// <summary>
    /// 技能管理器
    /// </summary>
    public class CharacterSkillManager : MonoBehaviour {
        public int? LastSkillId = null;
        // 技能列表
        [HideInInspector]
        public SkillData[] skills;
        public Animator Animator = null;

        private void Start() {
            foreach (SkillData skill in skills) {
                this._initSkill(skill);
            }
        }

        private void _initSkill(SkillData data) {
            data.skillPrefab = Resources.Load<GameObject>("Skill/" + data.prefabName);
            if (data.skillPrefab == null) {
                Debug.LogError("Skill prefab not found: " + data.prefabName);
                return;
            }
            data.owner = this.gameObject;
        }

        /// <summary>
        /// 根据技能ID获取技能数据，并检查技能是否可用
        /// </summary>
        /// <param name="skillId">技能ID</param>
        /// <returns>技能数据，不满足条件返回空</returns>
        public SkillData PrepareSkill(int skillId) {
            SkillData data = this.skills.First(x => x.skillId == skillId);
            if (data == null) {
                Debug.LogError("Skill not found: " + skillId);
                return null;
            }

            if (data.coolRemain <= 0)
                return data;
            return null;
        }

        /// <summary>
        /// 生成技能
        /// </summary>
        /// <param name="data">技能数据</param>
        public void GenerateSkill(SkillData data) {
            if (data == null) {
                Debug.LogError("Cannot generate null skill");
                return;
            }
            // 创建技能
            GameObject skillGO = Instantiate(data.skillPrefab, this.transform.position, this.transform.rotation);            

            // 传递技能数据
            SkillDeployer deployer = skillGO.GetComponent<SkillDeployer>();
            deployer.SkillData = data;
            
            // 播放技能动画
            if (data.animationName != "")
                Animator?.SetTrigger(data.animationName);

            // 执行技能            
            deployer.DeploySkill();
            
            // 销毁技能
            // Destroy(skillGO, data.durationTime);

            // 开启技能冷却
            StartCoroutine(this._coolTimeDown(data));
        }

        // 技能冷却
        private IEnumerator _coolTimeDown(SkillData data) {
            for (data.coolRemain = data.coolTime; data.coolRemain > 0; data.coolRemain -= 0.02f)
                yield return new WaitForSeconds(0.02f);
        }
    }
}