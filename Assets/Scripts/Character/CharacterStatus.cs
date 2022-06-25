using UnityEngine;

public class CharacterStatus : MonoBehaviour {
    public int Direction = 1;

    // private void Update() {
    //     Direction = Input.GetAxis("Horizontal") > 0 ? 1 : -1;
    //     if (Input.GetMouseButtonDown(0)) {
    //         Skill.CharacterSkillManager skillManager = GetComponent<Skill.CharacterSkillManager>();
    //         Skill.SkillData skill = skillManager.PrepareSkill(1);
    //         if (skill != null) {
    //             skillManager.GenerateSkill(skill);
    //         }
    //     }
    // }
    
    public void Kill() {
        // todo 杀死角色
        Debug.Log("KILL!!!");
    }
}
