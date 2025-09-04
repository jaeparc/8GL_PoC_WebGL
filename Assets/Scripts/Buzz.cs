using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using StarterAssets;
using UnityEngine;

public class Buzz : MonoBehaviour
{
    [Header("--- VALUES ---")]
    public string IDcampus = "DMO";

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ThirdPersonController>())
        {
            Debug.Log("Buzz!");
            other.GetComponentInParent<Interactable>().CanBuzz(this);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ThirdPersonController>())
        {
            Debug.Log("Stop Buzz!");
            other.GetComponentInParent<Interactable>().StopBuzz();
        }
    }
}
