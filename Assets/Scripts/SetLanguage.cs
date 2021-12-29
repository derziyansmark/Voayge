using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SetLanguage : MonoBehaviour
{
    public Language[] languages;
    public Text CreateRoom;
    public Text Settings;
    public Text Aboutversion;
    public Text Back;
    public Text Exit;
    public Text Console;
    public Text JoinRoom;

    void Start()
    {
        if (PlayerPrefs.HasKey("lang")) 
        {
            int index = PlayerPrefs.GetInt("lang");
            CurrentLanguage(index);
        }
    }

    public void CurrentLanguage(int index)
    {
        CreateRoom.text = languages[index].createroom;
        Settings.text = languages[index].settings;
        Aboutversion.text = languages[index].aboutversion;
        Back.text = languages[index].back;
        Exit.text = languages[index].exit;
        JoinRoom.text = languages[index].joinroom;
        Console.text = languages[index].console;
        PlayerPrefs.SetInt("lang", index);
    }
}
