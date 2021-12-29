using Photon.Pun;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] Spawns;

    public GameObject Player;

    private void Awake()
    {
        Vector3 randomPosition = Spawns[Random.Range(0, Spawns.Length)].transform.localPosition;

        PhotonNetwork.Instantiate(Player.name, randomPosition, Quaternion.identity);
    }
}
