using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Zone : MonoBehaviour
{
    // Note: This does not yet track which game the events are being called from.
    //       If you have multiple games running, you won't be able to tell which game called each event.

    //[SerializeField] private float timer = 0f;
    //[SerializeField] private bool countingUp = false;
    
    //private void Update()
    //{
    //    if (countingUp == true)
    //    {
    //        timer += Time.deltaTime;
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.TryGetComponent(out Player otherZone))
    //    {
    //        countingUp = true;
    //        string newMessage = "[" + other.gameObject.name + "] entered [" + gameObject.name + "] with timer at [" + timer + "] seconds.";

    //        // enteredZone [Red Zone] (as example)
    //        Analytics.CustomEvent(newMessage);
    //        Debug.Log(newMessage);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.TryGetComponent(out Player otherZone))
    //    {
    //        countingUp = false;
    //        string newMessage = "[" + other.gameObject.name + "] exited [" + gameObject.name + "]. Total time spent in [" + gameObject.name + "] was [" + timer + "] seconds.";

    //        // enteredZone [Red Zone] (as example)
    //        Analytics.CustomEvent(newMessage);
    //        Debug.Log(newMessage);
    //   }
    //}
}
