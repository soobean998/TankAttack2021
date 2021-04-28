using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    private new Rigidbody rigidbody;
    public GameObject ex;
    public string Shooter;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddRelativeForce(Vector3.forward * 3000.0f);
        
    
    }

    void OnCollisionEnter(Collision coll)
    {
        GameObject obj = Instantiate(ex, transform.position, Quaternion.identity);
        Destroy(obj, 3.0f);

        Destroy(this.gameObject);




    }

  
}
