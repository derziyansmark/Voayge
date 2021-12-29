using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class LangSystem : MonoBehaviour
{
    public Image langBttnImg;
    public Sprite[] flags;

    private string json;
    public static lang lng = new lang();
    private int langIndex = 1;
    private string[] langArray = {"ru_RU","en_US"};

    void Awake()
    {
        if (!PlayerPrefs.HasKey("Language"))
        {
            if (Application.systemLanguage == SystemLanguage.Russian || Application.systemLanguage == SystemLanguage.Ukrainian || Application.systemLanguage == SystemLanguage.Belarusian)
                PlayerPrefs.SetString("Language", "ru_RU");
            else PlayerPrefs.SetString("Language", "en_US");
        }
        LangLoad();
    }
    void Start()
    {
        for (int i = 0; i < langArray.Length; i++)
        {
            if (PlayerPrefs.GetString("Language") == langArray[i])
            {
                langIndex = i + 1;
                langBttnImg.sprite = flags[i];
                break;
            }
        }
    }

    void LangLoad()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        string path = Path.Combine(Application.streamingAssetsPath, "Languages/" + PlayerPrefs.GetString("Language") + ".json");
        WWW reader = new WWW(path);
        while (!reader.isDone) { }
        json = reader.text;
#else
        json = File.ReadAllText(Application.streamingAssetsPath + "/Languages/" + PlayerPrefs.GetString("Language") + ".json");
#endif
        lng = JsonUtility.FromJson<lang>(json);
    }
    public void switchBttn()
    {
        if (langIndex != langArray.Length) langIndex++;
        else langIndex = 1;
        PlayerPrefs.SetString("Language", langArray[langIndex - 1]);
        langBttnImg.sprite = flags[langIndex - 1];
        LangLoad();
    }

}
public class lang
{
    public string[] questions = new string[5];
    public answers[] answers = new answers[5];
}
[System.Serializable]
public class answers
{
    public string[] answersS = new string[3];
}