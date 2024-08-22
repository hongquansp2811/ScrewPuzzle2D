using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public List<Bar> listBar = new List<Bar>();
    public List<Screw> screws = new List<Screw>();
    private int barCount;

    private void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        listBar.AddRange(GetComponentsInChildren<Bar>());
        screws.AddRange(GetComponentsInChildren<Screw>());
        barCount = listBar.Count;
    }

    public void RemoveBarInMap() {  barCount--; }

    public int GetBarCount() { return barCount; }

    public void SetBarCount(int barCount) { this.barCount = barCount; }
}
