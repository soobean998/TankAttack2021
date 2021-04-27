using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{

    private readonly string gameVersion = "v1.0";
    private string userId = "Sbean";
    // Start is called before the first frame update
    void Awake()
    {
        PhotonNetwork.GameVersion = gameVersion;

        PhotonNetwork.NickName = userId;

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {   
        Debug.Log("Connectec to Photon Server!!!");
        PhotonNetwork.JoinRandomRoom(); //랜덤한 룽에 접속 시도
    
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // 룸 속성을 설정
        Debug.Log($"code ={returnCode}, msg ={message}");
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 30;

        //룸생성
        PhotonNetwork.CreateRoom("MyRoom", ro);
    }

    //룸생성 완료 콜백
    public override void OnCreatedRoom()
    {
        Debug.Log("방 생성 완료");
    }

    //룽에 입장했을 때 호출되는 콜백함수
    public override void OnJoinedRoom()
    {
        Debug.Log("방 입장 완료");
        Debug.Log(PhotonNetwork.CurrentRoom.Name);

        //통신이 가능한 주인공 캐릭터 생성
        PhotonNetwork.Instantiate("Tank",new Vector3(0, 5.0f, 0), Quaternion.identity, 0);
    }
}
