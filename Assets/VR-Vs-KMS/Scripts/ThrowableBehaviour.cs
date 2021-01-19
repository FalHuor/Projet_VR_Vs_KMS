using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using vr_vs_kms;

public class ThrowableBehaviour : MonoBehaviourPunCallbacks
{
    public bool Armed { get; set; } = false;
    public bool IsGrabbed { get; set; } = false;
    AudioSource[] audioSources;
    ParticleSystem particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        audioSources = gameObject.GetComponents<AudioSource>();
        particleSystem = gameObject.GetComponent<ParticleSystem>();
        //particleSystem.shape.radius = (float)AppConfig.Inst.RadiusExplosion;
        var radius = particleSystem.shape;
        radius.radius = AppConfig.Inst.RadiusExplosion;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("(Update)Armed : " + Armed);
        Debug.Log("(Update)IsGrabbed : " + IsGrabbed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Armed : " + Armed);
        Debug.Log("IsGrabbed : " + IsGrabbed);
        if (Armed && !IsGrabbed)
        {
            Debug.Log("colision : " + collision.gameObject);
            photonView.RPC("ExplosionDamage", RpcTarget.AllViaServer);
            
        }
        
    }
    /*
    public void UpdateArmed(bool boolean)
    {
        photonView.RPC("SetArmed", RpcTarget.AllViaServer, boolean);
    }

    public void UpdateGrabbed(bool boolean)
    {
        photonView.RPC("SetGrabbed", RpcTarget.AllViaServer, boolean);
    }

    [PunRPC]
    public void SetArmed(bool boolean)
    {
        Armed = boolean;
    }

    [PunRPC]
    public void SetGrabbed(bool boolean)
    {
        IsGrabbed = boolean;
    }*/

    [PunRPC]
    void ExplosionDamage()
    {
        Armed = false;
        IsGrabbed = false;
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, AppConfig.Inst.RadiusExplosion);
        audioSources[0].Play();
        particleSystem.Play();
        //AudioSource.PlayClipAtPoint(audioSources[0], gameObject.transform.position);
        Debug.Log("Length : " + hitColliders.Length);
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log("Name : " + hitCollider.name);
            if (hitCollider.CompareTag("Scientist"))
            {
                hitCollider.GetComponent<UserManager>().HitBySnowball("Viral");
                /*audioSources[1].Play();
                hitCollider.GetComponent<UserManager>().healthBar.UpdateHealth();*/
            }
            else if (hitCollider.CompareTag("Virus"))
            {
                hitCollider.GetComponentInParent<UserManager>().HitBySnowball("Antiviral");
                /*audioSources[1].Play();
                hitCollider.GetComponentInParent<UserManager>().healthBar.UpdateHealth();*/
            }
        }

        Destroy(gameObject, 4);

    }
}
