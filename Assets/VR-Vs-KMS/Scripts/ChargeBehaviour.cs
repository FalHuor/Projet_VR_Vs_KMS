using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using vr_vs_kms;

public class ChargeBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.tag.Equals("Antiviral"))
            gameObject.GetComponent<Renderer>().material.color = AppConfig.Inst.ColorShotKMS;
        else
            gameObject.GetComponent<Renderer>().material.color = AppConfig.Inst.ColorShotVirus;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject;
        Debug.Log("Snowball hit something:" + hit);
        UserManager um = hit.GetComponent<UserManager>();
        if (hit.CompareTag("Virus"))
            um = hit.GetComponentInParent<UserManager>();
        else if (hit.CompareTag("Scientist"))
            um = hit.GetComponent<UserManager>();
        Debug.Log(um);
        if (um != null)
        {
            Debug.Log("  It is a player !!");
            um.HitBySnowball(gameObject.tag);
        }
        Destroy(gameObject);
    }
}
