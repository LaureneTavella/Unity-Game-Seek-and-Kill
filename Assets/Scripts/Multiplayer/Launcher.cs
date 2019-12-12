using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Multiplayer
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        public static Launcher instance;
        public int maxPlayerCount = 2;

        private void Awake()
        {
            instance = this;
            PhotonNetwork.AutomaticallySyncScene = true;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            Connect();
        }

        public void Connect()
        {
            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.ConnectUsingSettings();
                Debug.Log("starting connection", this);
            }
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            Debug.Log("connected to master", this);
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);
            Debug.Log("disconnected from master", this);
        }
    
        public void JoinRandomRoom()
        {
            PhotonNetwork.JoinRandomRoom();
        }

        public void CreateRoom()
        {
            PhotonNetwork.CreateRoom(System.Guid.NewGuid().ToString());
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            Debug.Log($"joined room {PhotonNetwork.CurrentRoom.Name}", this);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            Debug.Log("new player joined room", this);
            if(PhotonNetwork.CurrentRoom.PlayerCount >= maxPlayerCount)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
                Debug.Log("Max players count reached");
                LaunchGame();
            }
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);
            Debug.Log("Player left room", this);
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            base.OnJoinRandomFailed(returnCode, message);
            Debug.Log("Can't join room, creating one instead", this);
            CreateRoom();
        }
    
        public void LaunchGame()
        {
            PhotonNetwork.LoadLevel(1);
        }
    }
}
