using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//указывай функцию CONSOLE в компоненте InputField в разделе End Edit (срабатывает по кнопке Enter)

public class Console : MonoBehaviour
{

    public InputField  consoleInputField;
    public GameObject FpsCounter;
    private bool FpsCounter_ON;

    [TextArea( 20, 10 )] public string hint = " reload - перезагрузка";


    void Start ()
    {

    } 


    void Update()
    {

    }

    public void CONSOLE() //функцию указываем в компоненте InputField в разделе End Edit (будет срабатывать при нажати клав. Enter)

     // Ввожу само название чита в ковычки
    {
        if(consoleInputField.text == "stop") Debug.Break();
        if(consoleInputField.text == "reload") Application.LoadLevel(Application.loadedLevel);
        if(consoleInputField.text == "s0") Application.LoadLevel(0); // в новых версиях через  SceneManager.LoadScene
        if(consoleInputField.text == "s1") Application.LoadLevel(1);
        if(consoleInputField.text == "s2") Application.LoadLevel(2);
        if(consoleInputField.text == "fps 1")
        {
            FpsCounter_ON = true;
            FpsCounter.SetActive(true);
        }
        if(consoleInputField.text == "fps 0")
        {
            FpsCounter_ON = false;
            FpsCounter.SetActive(false);
        }
    }
}
