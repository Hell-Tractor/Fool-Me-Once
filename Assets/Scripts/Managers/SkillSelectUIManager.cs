using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSelectUIManager : MonoBehaviour {
    public Text SkillHint;
    public Text Message;
    public GameObject[] Toggles;
    [Multiline]
    public string[] HintBeforeSelect;
    [Multiline]
    public string[] MessageForPlayer;
    public GameObject MaskGameObject;
    public float MaskHideDuration;
    public Button ConfirmButton;
    public AudioClip ConfirmButtonClickSound;
    private int[] _IdInPool;
    private int _selectedCount = 0;
    private int _currentPlayerId = 1;

    private void Start() {
        this.StartSkillSelect(1);
    }
    
    public void GenerateSkill() {
        _IdInPool = Utility.GetRandomArray(1, GameManager.Instance.SkillPool.Length - 1, Toggles.Length);
        for (int i = 0; i < Toggles.Length; ++i) {
            Toggles[i].GetComponent<Toggle>().isOn = false;
            SkillToggleManager manager = Toggles[i].GetComponent<SkillToggleManager>();
            manager.SkillId = GameManager.Instance.SkillPool[_IdInPool[i]].skillId;
            manager.Description = GameManager.Instance.SkillPool[_IdInPool[i]].description;
            foreach (Image img in Toggles[i].GetComponentsInChildren<Image>()) {
                img.sprite = Resources.Load<Sprite>("UI/UI" + manager.SkillId);
            }
            Toggles[i].GetComponentInChildren<Text>().text = GameManager.Instance.SkillPool[_IdInPool[i]].name;
        }
    }

    public async Task<bool> HideMask() {
        for (float timer = 0; timer < MaskHideDuration; timer += 0.01f) {
            if (!Input.GetKey(KeyCode.E)) {
                MaskGameObject.GetComponent<Image>().color = new Color(0, 0, 0, 1);
                return false;
            }
            MaskGameObject.GetComponent<Image>().color = new Color(0, 0, 0, Mathf.Lerp(1, 0.5f, timer / MaskHideDuration));
            await Task.Delay(10);
        }
        MaskGameObject.SetActive(false);
        return true;
    }

    public void ShowMask(string text) {
        MaskGameObject.GetComponent<Image>().color = new Color(0, 0, 0, 1);
        MaskGameObject.GetComponentInChildren<Text>().text = text;
        MaskGameObject.SetActive(true);
    }

    public async void StartSkillSelect(int playerId) {
        _currentPlayerId = playerId;
        SkillHint.text = "";
        this.ShowMask(HintBeforeSelect[playerId - 1]);
        Message.text = MessageForPlayer[playerId - 1];

        this.GenerateSkill();
        
        do {
            while (!Input.GetKey(KeyCode.E))
                await Task.Delay(10);
        } while (!await this.HideMask());
    }

    public void SubmitSkill() {
        AudioManager.Instance.PlaySFX(ConfirmButtonClickSound);
        int p = 1;
        for (int i = 0; i < Toggles.Length; ++i) {
            SkillToggleManager manager = Toggles[i].GetComponent<SkillToggleManager>();
            if (Toggles[i].GetComponent<Toggle>().isOn) {
                SkillSelectManager.Instance.PlayerSkill[_currentPlayerId - 1, p++] = manager.SkillId;
            }
        }
        if (_currentPlayerId == 1) {
            ++_currentPlayerId;
            this.StartSkillSelect(2);
        } else {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        }
    }
    
    private void OnGUI() {
        ConfirmButton.interactable = _selectedCount == 3;

        PointerEventData data = new PointerEventData(EventSystem.current);
        data.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(data, results);
        foreach (RaycastResult result in results) {
            SkillToggleManager manager = result.gameObject.transform.parent?.GetComponent<SkillToggleManager>();
            if (manager != null) {
                SkillHint.text = manager.Description;
                break;
            }
        }

        _selectedCount = 0;
        foreach (Toggle toggle in Toggles.Select(x => x.GetComponent<Toggle>())) {
            if (toggle.isOn) {
                ++_selectedCount;
            }
        }
    }
}