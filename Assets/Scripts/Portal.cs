using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    // Start is called before the first frame update
    // destination: Vector3
    // player
    void Start()
    {
        // player tagiyle bul
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
        }
    }

    // Teleport() plyaer.transform.position = destination
}
