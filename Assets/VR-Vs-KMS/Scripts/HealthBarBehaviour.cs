using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using vr_vs_kms;

public class HealthBarBehaviour : MonoBehaviour
{
    private UnityEngine.UI.Text healthBar;
    public UserManager Player;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.Find("Scientist").GetComponent<UserManager>();
        healthBar = GetComponent<UnityEngine.UI.Text>();
        /*if(Player.CompareTag("Scientist"))
            healthBar.text = "Health : " + Player.Health;
        else if (Player.CompareTag("Virus"))
            healthBar.text = Player.Health.ToString();*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealth()
    {
        if (Player.CompareTag("Scientist"))
            healthBar.text = "Health : " + Player.Health;
        else if (Player.CompareTag("Virus"))
            healthBar.text = Player.Health.ToString();
    }
}
