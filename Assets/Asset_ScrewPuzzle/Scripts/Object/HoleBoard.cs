using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleBoard : MonoBehaviour
{
    public bool CheckHoleFree()
    {
        float radius = 0.2f;
        LayerMask layerMask = LayerMask.GetMask(
            "Bar", "Bar1", "Bar2", "Bar3", "Bar4", "Bar5",
            "Bar6", "Bar7", "Bar8", "Bar9", "Bar10",
            "Bar11", "Bar12", "Bar13", "Bar14", "Bar15"
        );

        Collider2D collider = Physics2D.OverlapCircle(transform.position, radius, layerMask);
        return collider == null;
    }
}
