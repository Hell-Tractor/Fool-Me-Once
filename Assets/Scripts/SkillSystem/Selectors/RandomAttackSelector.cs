using UnityEngine;
using System.Linq;

namespace Skill {
    public class RandomAttackSelector : IAttackSelector {
        protected override Transform[] _filteringTargets(Transform[] allTargets, SkillData data, Transform skillTF) {
            int randomValue = Random.Range(0, 12);
            data.value = randomValue;
            string name = "Player" + ((randomValue % 2) + 1);
            return allTargets.Where(t => t.name == name).ToArray();
        }
    }
}