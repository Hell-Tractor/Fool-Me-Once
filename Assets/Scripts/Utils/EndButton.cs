using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndButton : MonoBehaviour
{
    //�ٵ�button��Ҫ���� 

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
        Debug.Log("Button");
     }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space)){
            SceneManager.LoadScene("Start");
        }
    }
    public void OnClick()
    {
        Debug.Log(111);
       SceneManager.LoadScene("Start");  
    }
    
}