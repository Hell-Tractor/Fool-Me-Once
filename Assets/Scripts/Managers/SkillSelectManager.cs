using UnityEngine;
using System.Linq;

public class SkillSelectManager : MonoBehaviour {
    private static SkillSelectManager _instance;
    public static SkillSelectManager Instance { get { return _instance; } }
    public Skill.CharacterSkillManager[] Players;
    public int[,] PlayerSkill;

    private void Awake() {
        if (_instance != null) {
            Debug.LogWarning("SkillSelectManager already exists");
            Destroy(gameObject);
        } else {
            _instance = this;
        }

        UnityEngine.SceneManagement.SceneManager.sceneLoaded += (scene, mode) => {
            if (scene.name == "GameScene") {
                FindPlayers();
                SkillSelectManager.Instance._copySkills(1);
                SkillSelectManager.Instance._copySkills(2);
            }
        };
    }

    void Start() {
        PlayerSkill = new int[2, 4];
        PlayerSkill[0, 0] = PlayerSkill[1, 0] = 1;
        // _copySkills(Players[0], player1Skills);
        // _copySkills(Players[1], player2Skills);
    }

    public void FindPlayers() {
        Players = GameObject.FindGameObjectsWithTag("Player").OrderBy(x => x.name).Select(x => x.GetComponent<Skill.CharacterSkillManager>()).ToArray();
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

    public void _copySkills(int playerId) {
        --playerId;
        Players[playerId].skills = new Skill.SkillData[4];
        for (int i = 0; i < 4; i++) {
            Players[playerId].skills[i] = GameManager.Instance.SkillPool.First(x => x.skillId == PlayerSkill[playerId, i]).Clone() as Skill.SkillData;
        }
    }
}