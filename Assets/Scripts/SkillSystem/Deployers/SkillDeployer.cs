using System;
using System.Collections.Generic;
using UnityEngine;

namespace Skill {
    /// <summary>
    /// 技能释放器
    /// </summary>
    public abstract class SkillDeployer : MonoBehaviour {
        private SkillData _skillData;
        public SkillData SkillData {
            get {
                return _skillData;
            }
            set {
                _skillData = value;
                this._initDeployer();
            }
        }

        private IAttackSelector _selector;
        private List<IImpactEffect> _impactArray;

        private void _initDeployer() {
            _selector = DeployerConfigFactory.CreateAttackSelector(_skillData);

            _impactArray = DeployerConfigFactory.CreateImpactEffect(_skillData);
        }

        /// <summary>
        /// 计算目标
        /// </summary>
        protected void _calculateTargets() {
            _skillData.attackTargets = _selector.SelectTarget(_skillData, this.transform);
        }

        /// <summary>
        /// 应用效果
        /// </summary>
        protected void _impactTargets() {
            foreach (IImpactEffect impact in _impactArray) {
                impact.Execute(this);
            }
        }

        /// <summary>
        /// 释放技能
        /// </summary>
        public abstract void DeploySkill();

        public void Destroy() {
            Destroy(this.gameObject);
        }
    }
}