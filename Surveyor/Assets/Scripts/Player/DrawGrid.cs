using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGrid : MonoBehaviour
{
    [SerializeField] private Transform cellPrefab;
    [SerializeField] private Vector2 gridSize = new Vector2(5, 5);
    [SerializeField] private float cellSize = 1;
    [SerializeField] private float yPos = 1.01f;
    private Transform gridParent;
    private List<Transform> cellPool = new List<Transform>();

    private void Start()
    {
        gridParent = new GameObject("Grid").transform;
        DrawCells();
    }

    [EasyButtons.Button]
    public void DrawCells()
    {
        if (cellPool.Count == 0)
        {
            FillPool();
        }
        PoolCells();
        Vector3 position = transform.position;
        position.y = yPos;
        position.x -= (gridSize.x / 2) * cellSize;
        position.z -= (gridSize.y / 2) * cellSize;

        for (int y = 0; y < gridSize.y; y++)
        {
            if (CellAvailable(position))
            {
                GetCell().position = position;
            }
            for (int x = 0; x < gridSize.x; x++)
            {
                if (CellAvailable(position))
                {
                    GetCell().position = position;
                }
                position.z += cellSize;
            }
            position.z -= gridSize.x;
            position.x += cellSize;
        }
    }

    private bool CellAvailable(Vector3 pos)
    {
        pos.y += 0.5f;
        pos.x += cellSize/2;
        pos.z += cellSize/2;
        if (Physics.Raycast(pos, Vector3.down, 1))
        {
            return true;
        }

        return false;
    }

    private void PoolCells()
    {
        for (int i = 0; i < cellPool.Count; i++)
        {
            cellPool[i].gameObject.SetActive(false);
        }
    }

    private Transform GetCell()
    {
        for (int i = 0; i < cellPool.Count; i++)
        {
            if (!cellPool[i].gameObject.activeSelf)
            {
                cellPool[i].gameObject.SetActive(true);
                return cellPool[i];
            }
        }

        return null;
    }

    private void FillPool()
    {
        for (int i = 0; i < gridSize.x * gridSize.y; i++)
        {
            cellPool.Add(Instantiate(cellPrefab, gridParent));
            cellPool[cellPool.Count - 1].gameObject.SetActive(false);
        }
    }
}
