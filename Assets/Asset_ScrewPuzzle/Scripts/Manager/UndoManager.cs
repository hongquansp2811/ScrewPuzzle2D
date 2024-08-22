using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class BarState
{
    public Vector3 position;
    public Quaternion rotation;
    public bool hasFallen;
}

[System.Serializable]
public class UndoState
{
    public List<Vector3> ScrewPossiions = new List<Vector3>();
    public List<BarState> barStates = new List<BarState>();
    public int barCount;
}

public class UndoManager : Singleton<UndoManager>
{
    private Stack<UndoState> history = new Stack<UndoState>();

    public void SaveState(List<Screw> screws, List<Bar> bars)
    {
        UndoState newState = new UndoState()
        {
            barCount = LevelManager.Ins.currentMap.GetBarCount(),
        };

        foreach (Screw screw in screws)
        {
            if (screw != null) 
            {
                Vector3 posScrew = screw.transform.position;
                newState.ScrewPossiions.Add(posScrew);
            }
        }

        foreach (Bar bar in bars)
        {
            BarState barState = new BarState()
            {
                position = bar.transform.position,
                rotation = bar.transform.rotation,
                hasFallen = bar.HasFallen,
            };
            newState.barStates.Add(barState);
        }
        history.Push(newState);
    }

    public UndoState Undo()
    {
        if (history.Count > 0)
        {
            return history.Pop();
        }
        return null;
    }

    public void Clear() 
    {
        history.Clear();
    }
}
