using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testOnDestroy : MonoBehaviour
{

    public List<GameObject> listObject = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in this.transform) {
            listObject.Add(child.gameObject);

            // Do things with obj
        }

        Debug.Log("Nombre de child : " + listObject.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Leave zone : " + other.gameObject.name);
        Destroy(other.gameObject);
    }

    public void RemovePlayer(GameObject player)
    {
        listObject.Remove(player);
    }
}
