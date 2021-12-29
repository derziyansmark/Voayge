using System.ComponentModel.Design.Serialization;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine;

public class MainMenu : MonoBehaviourPunCallbacks
{
    public InputField InputFieldNameRoom;


    public GameObject ErrorPanel;

    public void CreateRoom()
    {
        if(InputFieldNameRoom.text.Length > 3)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 4;
            PhotonNetwork.CreateRoom(InputFieldNameRoom.text, roomOptions);
        }
        else ErrorPanel.SetActive(true);

    }
    public void JoinRoom()
    {
        if (InputFieldNameRoom.text.Length > 3) PhotonNetwork.JoinRoom(InputFieldNameRoom.text);
        else ErrorPanel.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("TestScene(Game)");
    }
}
