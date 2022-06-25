using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Skill {
    public abstract class IAttackSelector {
        /// <summary>
        /// 搜索目标
        /// </summary>
        /// <param name="data">技能数据</param>
        /// <param name="skillTF">技能所在对象的Transform</param>
        /// <returns>搜索到目标Transform的数组</returns>
        public Transform[] SelectTarget(SkillData data, Transform skillTF) {
            Transform[] allTargets = this._getAllTargets(data);
            return this._filteringTargets(allTargets, data, skillTF);
        }

        /// <summary>
        /// 根据tag获取所有目标
        /// </summary>
        protected Transform[] _getAllTargets(SkillData data) {
            List<Transform> targets = new List<Transform>();
            foreach (string tag in data.attackTargetTags) {
                targets.AddRange(GameObject.FindGameObjectsWithTag(tag).Select(go => go.transform));
            }
            return targets.ToArray();
        }

        /// <summary>
        /// 筛选目标：检查是否在范围内，是否可以作为目标
        /// </summary>
        protected virtual Transform[] _filteringTargets(Transform[] allTargets, SkillData data, Transform skillTF) {
            return allTargets;
        }
    }
}