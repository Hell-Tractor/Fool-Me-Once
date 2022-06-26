using UnityEngine;
using UnityEngine.SceneManagement;

public class EndButton : MonoBehaviour {
    public AudioClip ConfirmButtonClickSound;
    public void Replay() {
        AudioManager.Instance.PlaySFX(ConfirmButtonClickSound);
        SceneManager.LoadScene("UI");
    }
    public void EndGame() {
        AudioManager.Instance.PlaySFX(ConfirmButtonClickSound);
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}