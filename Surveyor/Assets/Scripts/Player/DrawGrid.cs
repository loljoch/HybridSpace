using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGrid : MonoBehaviour
{
    [SerializeField] private Transform cellPrefab;
    [SerializeField] private Transform camTransform;
    [SerializeField] private Vector2 gridSize = new Vector2(5, 5);
    [SerializeField] private float cellSize = 1;
    [SerializeField] private float yPos = -1.01f;
    [SerializeField] private float xGridOffset, zGridOffset = 0;
    [SerializeField] private float yPosRay = 2f;
    [SerializeField] private LayerMask platformLayer = 10;
    private Transform gridParent;
    private List<Transform> cellPool = new List<Transform>();

    private void Start()
    {
        gridParent = new GameObject("Grid").transform;
        DrawCells();
        gridParent.transform.position = new Vector3(xGridOffset, 0, zGridOffset);
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
        position.x = Mathf.RoundToInt(position.x);
        position.x -= (gridSize.x / 2) * cellSize;
        position.x -= 0.5f;
        position.z = Mathf.RoundToInt(position.z);
        position.z -= (gridSize.y / 2) * cellSize;
        position.z -= 0.5f;

        for (int y = 0; y < gridSize.y; y++)
        {
            SetCell(position);

            for (int x = 0; x < gridSize.x; x++)
            {
                SetCell(position);
                position.z += cellSize;
            }
            position.z -= gridSize.x;
            position.x += cellSize;
        }
    }

    [EasyButtons.Button]
    public void RedrawCell()
    {
        for (int i = 0; i < cellPool.Count; i++)
        {
            DestroyImmediate(cellPool[i].gameObject);
        }
        cellPool.Clear();
        DrawCells();
    }

    private bool SetCell(Vector3 pos)
    {
        pos.y += yPosRay;
        pos.x += cellSize / 1;
        pos.z += cellSize / 1;
        RaycastHit hit;
        if (Physics.Raycast(pos, Vector3.down * 1.5f, out hit))
        {
            Vector3 hitPos = hit.point;
            if (!Physics.Linecast(camTransform.position, hitPos, platformLayer))
            {
                hitPos.y = yPos;
                GetCell().position = hitPos;
                return true;
            }
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

