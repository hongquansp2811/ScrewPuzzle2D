using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleBoard : MonoBehaviour
{
    public bool IsHoleFree = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Caching.CACHE_TAG_BAR)) 
        {
            IsHoleFree = true;
            Debug.Log("Bar vs Hole");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Caching.CACHE_TAG_BAR))
        {
            IsHoleFree = false;
        }
    }
}
