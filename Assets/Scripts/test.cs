using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    void Start()
    {
        Debug.Log("test start");
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("test");
    }
}
