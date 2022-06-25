using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputButton : MonoBehaviour
{
    // Start is called before the first frame update
    private int cnt=1;

    public static int PlayerNum;

    public static bool  FinishInputButton;

    int[] array = new int[8];

    public GameObject ImageW;
    public GameObject ImageA;
    public GameObject ImageS;
    public GameObject ImageD;
    public GameObject ImageUp;
    public GameObject ImageLeft;
    public GameObject ImageDown;
    public GameObject ImageRight;

    void Start()
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = Random.Range(1, 5);
        }
        for (int i = 0; i < array.Length; i++)
        {
           // Debug.Log(array[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
     //   Debug.Log(PlayerNum);
        ChooseButton(cnt);
        if (cnt == 5)
        {
            FinishInputButton=true;

        }
    }

    private void ChooseButton(int x)
    {
     //   Debug.Log(cnt);
        if (PlayerNum == 1)
        {
            if (x == 1)
            {
              
                ImageW.SetActive(true);
                if (Input.GetKeyDown(KeyCode.W))
                {
                    cnt++;
                    ImageW.SetActive(false);
                }
            }
            else if (x == 2)
            {
                ImageA.SetActive(true);
                if (Input.GetKeyDown(KeyCode.A))
                {
                    cnt++;
                    ImageA.SetActive(false);
                }
            }
            else if (x == 3)
            {
                ImageS.SetActive(true);
                if (Input.GetKeyDown(KeyCode.S))
                {
                    cnt++;
                    ImageS.SetActive(false);
                }
            }
            else if (x == 4)
            {
                ImageD.SetActive(true);
                if (Input.GetKeyDown(KeyCode.D))
                {
                    cnt++;
                    ImageD.SetActive(false);
                }
            }
        }

        else if(PlayerNum == 2)
        {
            if (x == 1)
            {
                
                ImageUp.SetActive(true);
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    cnt++;
                    ImageUp.SetActive(false);

                }
            }
            else if (x == 2)
            {
                
                ImageLeft.SetActive(true);
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    cnt++;
                    ImageLeft.SetActive(false);
                }
            }
            else if (x == 3)
            {
               
                ImageRight.SetActive(true);
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    cnt++;
                    ImageRight.SetActive(false);
                }
            }
            else if (x == 4)
            {
               
                ImageDown.SetActive(true);
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    cnt++;
                    ImageDown.SetActive(false);
                }
            }
        }
    }
}


