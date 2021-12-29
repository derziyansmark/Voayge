using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
 [SerializeField] Text ChatMessage;
 [SerializeField] InputField inputField;

private PhotonView PhotonView;
private void Start() 
{
    PhotonView = GetComponent<PhotonView>();
}
 public void SendButton () 
 {
     PhotonView.RPC("Send_Data", RpcTarget.AllBuffered, PhotonNetwork.NickName,inputField.text);
 }

[PunRPC]
private void Send_Data (string nick, string message) 
 {
        ChatMessage.text = nick + " " + message;
 }
}
