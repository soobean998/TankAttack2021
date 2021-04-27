using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityStandardAssets.Utility;


public class TankCtrl : MonoBehaviour
{
    private new Transform transform;
    public float speed = 10.0f;
    private PhotonView pv;

    public Transform firePos;
    public GameObject cannon;


    // Start is called before the first frame update
    void Start()
    {
      
        transform = GetComponent<Transform>();
        pv = GetComponent<PhotonView>();

        if(pv.IsMine)
        {
            GetComponent<Rigidbody>().centerOfMass = new Vector3(0, -5.0f, 0);
            Camera.main.GetComponent<SmoothFollow>().target = transform.Find("CamPivot").transform;

        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if(pv.IsMine)
        {
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");
            
            transform.Translate(Vector3.forward * Time.deltaTime * speed * v);
            transform.Rotate(Vector3.up * Time.deltaTime * 100.0f * h);
            
            if(Input.GetMouseButtonDown(0))
            {
                Fire();
            }
        }
    }

    void Fire()
    {
        Instantiate(cannon, firePos.position, firePos.rotation);
    }
}
