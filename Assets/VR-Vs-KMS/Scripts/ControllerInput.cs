using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerInput : MonoBehaviour
{
    private SteamVR_Input_Sources inputSource;
    private bool isGrabbingPinch;
    public GameObject SelectedObject = null;
    private ControllerPointer controllerPointer;

    public GameObject virus;
    public GameObject virusCamera;

    private bool canTeleport = true;

    private void Awake()
    {
        inputSource = GetComponent<SteamVR_Behaviour_Pose>().inputSource;
        //controllerPointer = gameObject.GetComponent<ControllerPointer>();
        //controllerPointer.UpdateColor(Color.green);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canTeleport)
        {
            if (SteamVR_Actions.default_Teleport.GetStateDown(inputSource))
            {
                TeleportPressed();
            }
            if (SteamVR_Actions.default_Teleport.GetStateUp(inputSource))
            {
                TeleportReleased();
            }
        }       
    }

    private void TeleportPressed()
    {
        controllerPointer = gameObject.AddComponent<ControllerPointer>();
        //controllerPointer.UpdateColor(Color.green);
    }

    private void TeleportReleased()
    {
        if(controllerPointer.CanTeleport)
        {
            virusCamera.transform.position = controllerPointer.TargetPosition;
            virus.transform.position = controllerPointer.TargetPosition;
            canTeleport = false;
            StartCoroutine(TeleportDelay());
        }
        controllerPointer.DesactivatePointer();
        Destroy(controllerPointer);        
    }

    IEnumerator TeleportDelay()
    {
        yield return new WaitForSeconds(AppConfig.Inst.DelayTeleport);
        canTeleport = true;
    }

}
