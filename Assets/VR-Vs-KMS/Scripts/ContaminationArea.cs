using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace vr_vs_kms
{
    public class ContaminationArea : MonoBehaviour
    {
        [System.Serializable]
        public struct BelongToProperties
        {
            public Color mainColor;
            public Color secondColor;
            
        }

        public BelongToProperties nobody;
        public BelongToProperties virus;
        public BelongToProperties scientist;

        private float faerieSpeed;
        public float cullRadius = 5f;

        private float radius = 1f;
        private ParticleSystem pSystem;
        private WindZone windZone;
        private int remainingGrenades;
        public float inTimer = 0f;
        private CullingGroup cullGroup;


        private List<GameObject> listPlayerInZone = new List<GameObject>();
        private bool canCaptured = true;
        private bool isOnCaptured = false;
        private string onCaptureBy;
        private string capturedBy;


        void Start()
        {
            populateParticleSystemCache();
            setupCullingGroup();

            BelongsToNobody();
        }

        private void populateParticleSystemCache()
        {
            pSystem = this.GetComponentInChildren<ParticleSystem>();
        }


        /// <summary>
        /// This manage visibility of particle for the camera to optimize the rendering.
        /// </summary>
        private void setupCullingGroup()
        {
            Debug.Log($"setupCullingGroup {Camera.main}");
            cullGroup = new CullingGroup();
            cullGroup.targetCamera = Camera.main;
            cullGroup.SetBoundingSpheres(new BoundingSphere[] { new BoundingSphere(transform.position, cullRadius) });
            cullGroup.SetBoundingSphereCount(1);
            cullGroup.onStateChanged += OnStateChanged;
        }

        void OnStateChanged(CullingGroupEvent cullEvent)
        {
            Debug.Log($"cullEvent {cullEvent.isVisible}");
            if (cullEvent.isVisible)
            {
                pSystem.Play(true);
            }
            else
            {
                pSystem.Pause();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Virus") || other.CompareTag("Scientist"))
            {
                listPlayerInZone.Add(other.gameObject);
                if (listPlayerInZone.Count > 1)
                {
                    foreach (GameObject player in listPlayerInZone)
                    {
                        if (player.CompareTag(listPlayerInZone[0].tag))
                        {

                        }
                    }
                } 
                else
                {
                    canCaptured = true;
                }

            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Virus") || other.CompareTag("Scientist"))
            {
                listPlayerInZone.Remove(other.gameObject);


            }
        }

        private void OnTriggerStay(Collider other)
        {
            
        }

        void Update()
        {
            
        }

        private void ColorParticle(ParticleSystem pSys, Color mainColor, Color accentColor)
        {
            var myParticle = pSys.main;
            myParticle.startColor = new ParticleSystem.MinMaxGradient(mainColor, accentColor);
        }

        public void BelongsToNobody()
        {
            ColorParticle(pSystem, nobody.mainColor, nobody.secondColor);
        }

        public void BelongsToVirus()
        {
            ColorParticle(pSystem, virus.mainColor, virus.secondColor);
        }

        public void BelongsToScientists()
        {
            ColorParticle(pSystem, scientist.mainColor, scientist.secondColor);
        }

        void OnDestroy()
        {
            if (cullGroup != null)
                cullGroup.Dispose();
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, cullRadius);
        }
    }
}