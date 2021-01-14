using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusBodyBehaviour : MonoBehaviour
{
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(cam.transform.position.x, gameObject.transform.position.y, cam.transform.position.z);
        gameObject.transform.rotation = new Quaternion(gameObject.transform.rotation.x, cam.transform.rotation.y, gameObject.transform.rotation.z, cam.transform.rotation.w);
    }
}
