using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public Skill.SkillData[] SkillPool;

    private void Awake() {
        if (_instance != null) {
            Debug.LogWarning("GameManager already exists");
            Destroy(gameObject);
        } else {
            _instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}