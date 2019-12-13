using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeLoader : MonoBehaviour
{
    public MazeScriptableObject mazeScriptable;

    [EasyButtons.Button]
    public void SpawnMaze()
    {
        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            if (child != transform)
            {
                DestroyImmediate(child.gameObject);
            }
        }

        List<string> rooms = mazeScriptable.names;
        List<Vector3> positions = mazeScriptable.positions;
        List<Quaternion> rotations = mazeScriptable.rotations;

        transform.position = mazeScriptable.midPoint;

        for (int i = 0; i < rooms.Count; i++)
        {
            GameObject room = Instantiate(Resources.Load<GameObject>(rooms[i]), transform, true);
            room.transform.position = positions[i];
            room.transform.rotation = rotations[i];
        }
    }
}
