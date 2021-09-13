using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    static public bool goalMet = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Progectile")
        {
            Goal.goalMet = true;
            Debug.Log("”ра ты попал!!");
        }
        Material mat = GetComponent<Renderer>().material;
        Color c = mat.color;
        c.a = 124;
        mat.color = c;
    }
}
