using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject pairedPortal;

    private bool exitingPortal = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!exitingPortal)
        {
            col.gameObject.transform.position = pairedPortal.transform.position;
            pairedPortal.GetComponent<Portal>().exitingPortal = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        exitingPortal = false;
    }

    void Start()
    {
    }

    void Update()
    {
    }
}
