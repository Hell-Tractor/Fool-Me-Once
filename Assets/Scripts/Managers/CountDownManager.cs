using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CountDownManager : MonoBehaviour {
    public int Duration = 3;
    public Text Text;

    private void Start() {
        this.StartCountDown();
    }
    
    private async void StartCountDown() {
        Time.timeScale = 0;
        for (int i = Duration; i > 0; i--) {
            Text.text = i.ToString();
            await Task.Delay(1000);
        }
        Text.text = "GO";
        await Task.Delay(1000);
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }
}
