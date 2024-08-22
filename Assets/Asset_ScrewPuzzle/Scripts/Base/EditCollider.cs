using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class EditCollider : MonoBehaviour
{
    public bool OnEdit;
    public PolygonCollider2D polygon_HolePointsCollider;
    public Vector2[] _HolePointsCollider;

    public PolygonCollider2D polygonCollider;

    public List<HoleInfor> lsHoleInfor;
    public List<Transform> lsHoleChild;

    public Transform TestPoint;
    public Transform TestPoint2;
    private void Start()
    {

    }
    public void SetPointForHoleCollider()
    {
        lsHoleChild.Clear();
        lsHoleInfor.Clear();

        _HolePointsCollider = polygon_HolePointsCollider.GetPath(0);
        PointAfterScale();

        //polygonCollider = GetComponent<PolygonCollider2D>();
        if (polygonCollider == null)
        {
            Debug.LogError("null polygonCollider, please reference it");
        }

        SetUpHoleInfo();

        SetPathForPolygonCollider();

        SetPathForPolygonCollider_Time2();

    }
    private void PointAfterScale()
    {
        // set vị trí các point theo scale của gameobject chứa polygon
        Vector2[] originalPoints = _HolePointsCollider;
        Vector2 scale = polygon_HolePointsCollider.transform.localScale;
        for (int i = 0; i < originalPoints.Length; i++)
        {
            originalPoints[i] = new Vector2(originalPoints[i].x * scale.x, originalPoints[i].y * scale.y);
        }
        _HolePointsCollider = originalPoints;
    }
    private void SetUpHoleInfo()
    {
        lsHoleInfor = new List<HoleInfor>();


        int totalHold = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.layer == 7 && transform.GetChild(i).gameObject.activeSelf) // BarHole
            {
                lsHoleChild.Add(transform.GetChild(i));
                totalHold++;
            }
        }

        for (int i = 0; i < lsHoleChild.Count; i++)
        {
            HoleInfor newHold = new HoleInfor();
            newHold.Hole = lsHoleChild[i];

            newHold.HolePointsCollider = new Vector2[_HolePointsCollider.Length];
            Vector2[] offsets = new Vector2[_HolePointsCollider.Length];
            for (int p = 0; p < offsets.Length; p++)
            {
                offsets[p] = _HolePointsCollider[p] - Vector2.zero;
                //Debug.Log(_HolePointsCollider[p] + "    " +  offsets[p]);
                newHold.HolePointsCollider[p] = (Vector2)newHold.Hole.position + offsets[p];
            }



            lsHoleInfor.Add(newHold);
        }
    }
    private void SetPathForPolygonCollider()
    {
        polygonCollider.pathCount = 1 + lsHoleInfor.Count;
        for (int i = 0; i < lsHoleInfor.Count; i++)
        {
            polygonCollider.SetPath(i + 1, lsHoleInfor[i].HolePointsCollider);
        }
    }

    private void SetPathForPolygonCollider_Time2()
    {

        for (int i = 0; i < lsHoleInfor.Count; i++)
        {
            Vector2[] offsets2 = new Vector2[_HolePointsCollider.Length];
            for (int p = 0; p < offsets2.Length; p++)
            {
                offsets2[p] = _HolePointsCollider[p] - (Vector2)transform.position;
            }

            Vector2 offset1_5 = (Vector2)lsHoleInfor[i].Hole.position - Vector2.zero;

            for (int p = 0; p < offsets2.Length; p++)
            {
                lsHoleInfor[i].HolePointsCollider[p] = offset1_5 + offsets2[p];
            }

            polygonCollider.SetPath(i + 1, lsHoleInfor[i].HolePointsCollider);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!OnEdit) return;

    }

    [Serializable]
    public class HoleInfor
    {
        public Transform Hole;
        public Vector2[] HolePointsCollider;
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(EditCollider))]
public class SaveDataEditor : Editor
{
    public bool isCheck;
    private EditCollider myScript;

    private void OnSceneGUI()
    {
        myScript = (EditCollider)target;
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (myScript == null)
            myScript = (EditCollider)target;
        GUIStyle SectionNameStyle = new GUIStyle();
        SectionNameStyle.fontSize = 16;
        SectionNameStyle.normal.textColor = Color.blue;

        if (myScript == null) return;

        EditorGUILayout.LabelField("----------\t----------\t----------\t----------\t----------", SectionNameStyle);
        EditorGUILayout.BeginVertical(GUI.skin.box);
        {
            if (GUILayout.Button("Edit", GUILayout.Height(50)))
            {
                isCheck = true;
                myScript.SetPointForHoleCollider();
            }
        }
        EditorGUILayout.EndVertical();
        if (isCheck)
        {
            EditorUtility.SetDirty(myScript);
            isCheck = false;
        }
        //
    }
}
#endif
