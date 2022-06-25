using UnityEngine;
using System.Linq;

public class SkillSelectManager : MonoBehaviour {
    public Skill.CharacterSkillManager[] Players;
    public string[] player1Skills;
    public string[] player2Skills;

    void Start() {
        _copySkills(Players[0], player1Skills);
        _copySkills(Players[1], player2Skills);
    }

    public void _copySkills(Skill.CharacterSkillManager target, int[] skillIds) {
        target.skills = new Skill.SkillData[skillIds.Length];
        for (int i = 0; i < skillIds.Length; i++) {
            target.skills[i] = GameManager.Instance.SkillPool.First(x => x.skillId == skillIds[i]).Clone() as Skill.SkillData;
        }
    }

    public void _copySkills(Skill.CharacterSkillManager target, string[] skillNames) {
        target.skills = new Skill.SkillData[skillNames.Length];
        for (int i = 0; i < skillNames.Length; i++) {
            target.skills[i] = GameManager.Instance.SkillPool.First(x => x.name == skillNames[i]).Clone() as Skill.SkillData;
        }
    }
}