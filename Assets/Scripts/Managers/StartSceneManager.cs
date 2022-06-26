using UnityEngine;

public class StartSceneManager : MonoBehaviour {
    void Update() {
        // 按下回车跳转场景
        if (Input.GetKeyDown(KeyCode.Return)) {
            UnityEngine.SceneManagement.SceneManager.LoadScene("UI");
        }
    }
}
