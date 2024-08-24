using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CalAngleVector : MonoBehaviour
{
    public Transform vt1, vt2;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Cal()
    {
        //float angle = Vector2.Angle(vt1, vt2);

        Vector3 difference = vt2.position - vt1.position;

        // Tính góc từ trục x sử dụng Atan2
        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        // Đảm bảo góc là giá trị dương
        if (angle < 0)
        {
            angle += 360;
        }
        Vector3 lerp = Vector3.Lerp(vt1.position, vt2.position, 0.5f);
        Debug.Log("angle: " + angle + "  lerp 0.5 point: " + lerp);
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(CalAngleVector))]
public class CalAngleEditor : Editor
{
    public bool isCheck;
    private CalAngleVector myScript;

    private void OnSceneGUI()
    {
        myScript = (CalAngleVector)target;
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (myScript == null)
            myScript = (CalAngleVector)target;
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
                myScript.Cal();
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
