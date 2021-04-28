using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public TMP_Text roomNameText;
    public TMP_Text connectInfoText;
    public Button exitButton;
    public TMP_Text messageText;

    public static GameManager instance = null;

    //싱글턴 변수
    void Awake()
    {
        instance = this;
        Vector3 pos = new Vector3(Random.Range(-150.0f, 150.0f), 5.0f, Random.Range(-150.0f, 150.0f));

        //통신이 가능한 주인공 캐릭터 생성
        PhotonNetwork.Instantiate("Tank",pos, Quaternion.identity, 0);
    }
    void Start()
    {
        SetRoomInfo();
    }

    void SetRoomInfo()
    {
        Room currentRoom =  PhotonNetwork.CurrentRoom;
        roomNameText.text = currentRoom.Name;
        connectInfoText.text = $"{currentRoom.PlayerCount}/{currentRoom.MaxPlayers}";
    }

    public void OnExitClick()
    {
        PhotonNetwork.LeaveRoom(); //CleanUp
    }

    // CleanUp 끝난 후에 호출되는 콜백  
    public override void OnLeftRoom() //이미 Lobby에 있다
    {
       //Lobby 씬으로 되돌아 가기
       SceneManager.LoadScene("Lobby");

    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        SetRoomInfo();
        string msg = $"\n<color=#00ff00>{newPlayer.NickName}</color> is joined room";
        messageText.text += msg;

    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        SetRoomInfo();
        string msg = $"\n<color=#ff0000>{otherPlayer.NickName}</color> left room";
        messageText.text += msg;

    }
}
