using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playertest : MonoBehaviour
{

    private testOnDestroy scriptDestroy;
    public GameObject Truc;

    // Start is called before the first frame update
    void Start()
    {
        scriptDestroy = Truc.GetComponent<testOnDestroy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            gameObject.transform.position = new Vector3(0, 0, -5);
        }
    }

    void RemoveFromEveryWhere()
    {
        scriptDestroy.RemovePlayer(this.gameObject);
        Destroy(gameObject);
        Debug.Log(scriptDestroy.listObject.Count);
    }
}
