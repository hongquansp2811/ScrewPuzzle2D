using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caching : MonoBehaviour
{
    // Cache Tag
    public const string CACHE_TAG_BOLT = "Bolt";
    public const string CACHE_TAG_DEATHZONE = "DeathZone";
    public const string CACHE_TAG_BAR = "Bar";

    // Cache Anim
    public const string CACHE_ANIM_IDLE = "IsIdle";
    public const string CACHE_ANIM_SCREWOUT = "IsSelect";
    public const string CACHE_ANIM_SCREWIN = "IsScrewIn";

    //Cache Data
    public const string CACHE_DATA_CURRENTL_LEVEL = "CurrentLevel";
    public const string CACHE_DATA_HIGHTEST_LEVEL = "HightestLevel";

    private static Dictionary<Collider2D, object> cache2D = new Dictionary<Collider2D, object>();

    public static T GetComponentFromCache<T>(Collider2D collider) where T : Component
    {
        if (collider == null) return null;

        if (!cache2D.ContainsKey(collider))
        {
            T component = collider.GetComponent<T>();
            if (component != null)
            {
                cache2D.Add(collider, component);
            }
        }

        return cache2D.ContainsKey(collider) ? cache2D[collider] as T : null;
    }
}
