using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSkill : MonoBehaviour
{
    public bool hit = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Timer());
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (hit)
            {
                other.transform.GetComponent<Player>().TakeDamage(10);
                hit = false;
            }
        }
    }

    private IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            hit = true;
        }
    }
}
