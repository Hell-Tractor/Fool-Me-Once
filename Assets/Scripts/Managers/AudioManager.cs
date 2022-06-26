using UnityEngine;

public class AudioManager : MonoBehaviour {
    public AudioSource BgmSource;
    public AudioSource SfxSource;
    public AudioClip StartBgm;
    public AudioClip GameBgm;
    
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }

    private void Awake() {
        if (_instance != null) {
            Debug.LogError("AudioManager: Instance already exists!");
            Destroy(gameObject);
        } else {
            _instance = this;
        }
    }

    public void PlaySFX(AudioClip clip) {
        SfxSource.clip = clip;
        SfxSource.Play();
    }

    private void Start() {
        SfxSource.loop = false;
        BgmSource.loop = true;
        BgmSource.clip = StartBgm;
        BgmSource.Play();

        UnityEngine.SceneManagement.SceneManager.sceneLoaded += (scene, mode) => {
            if (scene.name == "GameScene") {
                BgmSource.clip = GameBgm;
                BgmSource.Play();
            }
        };
        
        DontDestroyOnLoad(gameObject);
    }
}