namespace Skill {
    public class SingleDeployer : SkillDeployer {
        public override void DeploySkill() {
            this._calculateTargets();
            this._impactTargets();
        }
    }
}