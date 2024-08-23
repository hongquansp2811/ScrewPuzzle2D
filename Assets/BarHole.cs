using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarHole : MonoBehaviour
{
    public LayerMask boltLayer;
    public bool HasBolt()
    {

        Collider2D[] results = Physics2D.OverlapCircleAll(transform.position, 0.2f, boltLayer);
        if (results.Length > 0)
        {
            foreach (Collider2D result in results)
            {
                if (Caching.GetComponentFromCache<Screw>(result) != null)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
