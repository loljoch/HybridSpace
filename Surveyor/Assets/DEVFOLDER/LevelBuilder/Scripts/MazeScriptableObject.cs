using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeScriptableObject : ScriptableObject
{
    public List<string> names;
    public List<Vector3> positions;
    public List<Quaternion> rotations;
    public Vector3 midPoint;

    public MazeScriptableObject(List<string> names, List<Vector3> positions, List<Quaternion> rotations, Vector3 midPoint)
    {
        this.names = names;
        this.positions = positions;
        this.rotations = rotations;
        this.midPoint = midPoint;
    }
}
