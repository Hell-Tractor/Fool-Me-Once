using UnityEngine;
using UnityEngine.SceneManagement;

public class EndButton : MonoBehaviour {
    public void Replay() {
        SceneManager.LoadScene("UI");
    }
    public void EndGame() {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}