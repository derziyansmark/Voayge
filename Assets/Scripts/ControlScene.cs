using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlScene : MonoBehaviour
{
   public GameObject StartGame;
   public GameObject Lobby;
   public GameObject QuitGame;


    public void ToLobby()           { SceneManager.LoadScene("LoadingScene to menu");}
    public void NewGame()       { SceneManager.LoadScene("LoadingScene");    }
    public void ExitGame()        { Application.Quit();                }

}
