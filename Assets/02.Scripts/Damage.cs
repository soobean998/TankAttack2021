using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Damage : MonoBehaviour
{
    private List<MeshRenderer> renderers = new List<MeshRenderer>();

    public int hp = 100;
    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        GetComponentsInChildren<MeshRenderer>(renderers);
    }

    void OnCollisionEnter (Collision coll)
    {
        if (coll.collider.CompareTag("CANNON"))
        {
            string shooter = coll.gameObject.GetComponent<Cannon>().Shooter;
            hp -= 10;
            if (hp <= 0)
            {

                StartCoroutine(TankDestroy(shooter));
            }

        }
    }

    IEnumerator TankDestroy(string shooter)
    {
        string msg = $"\n<color=#00ff00>{photonView.Owner.NickName} </color> is killed by <color=#ff0000> {shooter}</color>";
        GameManager.instance.messageText.text += msg; 

        // 발사로직을 정지
        // 렌더러 컴포넌트를 비활성화
        
        GetComponent<BoxCollider>().enabled = false;
        
        if (photonView.IsMine)
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }    
        foreach(var mesh in renderers) mesh.enabled = false;
        //5초 waitting
        yield return new WaitForSeconds(5.0f);

        Vector3 pos = new Vector3(Random.Range(-150.0f, 150.0f), 5.0f, Random.Range(-150.0f, 150.0f));
        
        transform.position = pos;
        // 렌더러 컴포넌틀 활성화
        hp = 100;
        GetComponent<BoxCollider>().enabled = true;
        if (photonView.IsMine)
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }    
        foreach (var mesh in renderers) mesh.enabled = true;

    }
}
