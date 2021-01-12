using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vr_vs_kms
{
    public enum UserDeviceType
    {
        OCULUS,
        PC
    }
    public class UserDeviceManager : MonoBehaviour
    {
        public static UserDeviceType GetDeviceUsed()
        {
            // Server execution

            string deviceUsed = null;//AppConfig.Inst.DeviceUsed.ToLower();

            switch (deviceUsed)
            {
                case "htc":
                    // Si l'app config demande du HTC mais que le casque n'est pas branché
                    Debug.LogWarning("AppConfig asked for HTC, but not active, so use PC version");
                    return UnityEngine.XR.XRSettings.isDeviceActive ? UserDeviceType.OCULUS : UserDeviceType.PC;

                case "pc":
                    return UserDeviceType.PC;

                default: // "auto" and others
                    return UnityEngine.XR.XRSettings.isDeviceActive ? UserDeviceType.OCULUS : UserDeviceType.PC;
            }
        }

        public static GameObject GetPrefabToSpawnWithDeviceUsed(GameObject pcPrefab, GameObject HTCPrefab)
        {
            UserDeviceType userDeviceType = GetDeviceUsed();
            switch (userDeviceType)
            {
                case UserDeviceType.OCULUS:
                    return HTCPrefab;
                case UserDeviceType.PC:
                default:
                    return pcPrefab;
            }
        }
    }
}