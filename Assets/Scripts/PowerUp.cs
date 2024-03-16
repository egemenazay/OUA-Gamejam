using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // player
    // string ad
    int id; 
    // Start is called before the first frame update
    void Start()
    {
        // Player tagiyle playerı bulma
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (id == 0)
            {
                
            }
        }
    }
}
