using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyDestroyer : MonoBehaviour
{
    public CandyManager candyManager;
    public int reward;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("接触");
        if (other.gameObject.tag == "Candy")
        {
            candyManager.AddCandy(reward);
            Destroy(other.gameObject);
        }
    }
}
