using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int baseHealth = 10;
    [SerializeField] Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        healthText.text = baseHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        baseHealth--;
        print("THE BASE IS BEING ATTACKED!");
        if (baseHealth == 0)
        {
            print("THE BASE IS DOWN!");
        }

        if (baseHealth >= 0)
        {
            healthText.text = baseHealth.ToString();
        }
        else
        {
            healthText.text = "DEAD!";
        }
    }
}
