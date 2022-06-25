using UnityEngine;
using System.Linq;

namespace Skill {
    public class GlobalAttackSelector : IAttackSelector {
        protected override Transform[] _filteringTargets(Transform[] allTargets, SkillData data, Transform skillTF) {
            return allTargets.Where(target => target.name != data.owner.name).ToArray();
        }
    }
}