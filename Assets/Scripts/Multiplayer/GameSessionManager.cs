using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSessionManager : MonoBehaviour
{
    public string playerPrefabPath;
    private GameObject player;

    void Start()
    {
        SpawnPlayer();
    }
    

    private void SpawnPlayer()
    {
        Vector3 pos = Random.insideUnitCircle.normalized;
        pos = new Vector3(pos.x, 0, pos.y);
        player = PhotonNetwork.Instantiate(playerPrefabPath, pos, Quaternion.identity);
    }
}
