using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peasant : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnEnable()
    {
        Wizard.Died += Celebrate;
    }

    private void OnDisable()
    {
        Wizard.Died -= Celebrate;
    }

    private void Celebrate()
    {
        GetComponent<Animator>().SetTrigger("end");
    }
}
