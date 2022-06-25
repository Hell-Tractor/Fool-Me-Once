using System;
using UnityEngine;

namespace Skill {
    [Serializable]
    public class SkillData {
        public int skillId;
        public string name;
        public string description;
        public float coolTime;
        [HideInInspector]
        public float coolRemain;
        public float attackDistance;
        public float attackRange;
        public string[] attackTargetTags;
        [HideInInspector]
        public Transform[] attackTargets;
        [Tooltip("技能效果名称列表")]
        public string[] impactType;
        public int nextBatterId;
        public float durationTime;
        public float atkInterval;
        [HideInInspector]
        public GameObject owner;
        public string prefabName;
        [HideInInspector]
        public GameObject skillPrefab;
        public string animationName;
        public string hitFxName;
        [HideInInspector]
        public GameObject hitFxPrefab;
        public SelectorType selectorType;
    }
}