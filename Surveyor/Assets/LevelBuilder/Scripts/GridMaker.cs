using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMaker : MonoBehaviour
{
    [SerializeField] private Vector2 gridSize = new Vector2(5, 6);
    [SerializeField] private float gridStepSize = 3.5f;
    [SerializeField] private GameObject buildingPlatform;
    [SerializeField] private int buildingLayer = 9;

    private List<GameObject> currentGrid = new List<GameObject>();

    [EasyButtons.Button]
    private void MakeGrid()
    {
        if (currentGrid.Count != gridSize.x * gridSize.y)
        {
            DestroyOldGrid();
            SetPositionToMiddle();
            Vector3 spawnPos = Vector3.zero;
            for (int y = 0; y < gridSize.y; y++)
            {
                spawnPos = Vector3.zero;
                spawnPos.z = y * gridStepSize;
                currentGrid.Add(Instantiate(buildingPlatform, spawnPos, Quaternion.identity, transform));
                currentGrid[currentGrid.Count - 1].layer = buildingLayer;
                for (int x = 0; x < gridSize.x - 1; x++)
                {
                    spawnPos.x += gridStepSize;
                    currentGrid.Add(Instantiate(buildingPlatform, spawnPos, Quaternion.identity, transform));
                    currentGrid[currentGrid.Count - 1].layer = buildingLayer;
                }
            }
        }
    }

    private void DestroyOldGrid()
    {
        foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        {
            if (child != transform)
            {
                DestroyImmediate(child.gameObject);
            }
        }
        currentGrid.Clear();
    }

    private void SetPositionToMiddle()
    {
        Vector3 middlePos = Vector3.zero;

        Vector3 rightCornerPos = Vector3.zero;
        rightCornerPos.x += (gridSize.x * gridStepSize) - gridStepSize;
        rightCornerPos.z += (gridSize.y * gridStepSize) - gridStepSize;
        Debug.Log(rightCornerPos);

        transform.position = rightCornerPos/2;
    }
}
