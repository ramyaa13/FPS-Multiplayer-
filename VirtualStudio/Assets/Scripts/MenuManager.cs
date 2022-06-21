using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MenuManager : MonoBehaviour
{
    public GameObject[] Panels;
    public TextMeshProUGUI RoomName;
    public TextMeshProUGUI ErrorMessage;

    private void Start()
    {
        for (int i = 0; i < Panels.Length; i++)
        {
            if (i == 0)
            {
                Panels[i].SetActive(true);
            }
            else
            {
                Panels[i].SetActive(false);
            }
        }
    }
    public void OpenPanel(int x)
    {
        for(int i = 0; i < Panels.Length; i++)
        {
            if(x==i)
            {
                Panels[x].SetActive(true);
            }
            else
            {
                Panels[i].SetActive(false);
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetRoomname(string x)
    {
        RoomName.text = x;
    }

    public void SetError(string x)
    {
        ErrorMessage.text = "Creating Room Failed: " + x;
    }
}
