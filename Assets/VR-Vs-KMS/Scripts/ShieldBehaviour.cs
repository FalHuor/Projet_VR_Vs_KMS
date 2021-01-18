using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehaviour : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Shield : " + collision.collider.tag);
        if (collision.collider.CompareTag("Antiviral"))
        {
            if(gameObject.transform.localScale.x <= 0.6f)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.transform.localScale += new Vector3(-0.1f, 0, -0.1f);
            }
        }
    }
}
