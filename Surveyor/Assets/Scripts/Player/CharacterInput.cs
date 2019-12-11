using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterInput : MonoBehaviour
{
    #region CamVars
    private Transform camTransform;
    [Header("Camera")]
    [HideInInspector] public Vector3 mapRotation = Vector3.zero;
    [SerializeField] private float sensitivityX = 15;
    [SerializeField] private float sensitivityY = 15;
    private float rotationY = 0f;
    private float minimumY = -60f;
    private float maximumY = 60f;
    #endregion

    #region MovementVars
    [Header("Movement")]
    [SerializeField] private LayerMask platform;
    [SerializeField] private float movementSpeed;
    [SerializeField] private Color selectedCell, deselectedCell;
    private DrawGrid dGrid;
    private Vector3 currentPos, nextPos;
    private Vector3 amountMoved = new Vector3();
    private bool isMoving = false;
    private float startTime;
    private float distance;
    private LineRenderer cTile = null;
    #endregion

    private void Awake()
    {
        dGrid = GetComponent<DrawGrid>();
        camTransform = GetComponentInChildren<Camera>().transform;
        currentPos = transform.position;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        MoveView();
        if (cTile != GetTile())
        {
            SetTileColor(cTile, deselectedCell, 0);

            cTile = GetTile();
            SetTileColor(cTile, selectedCell, 2);
        }
        if (!isMoving && Input.GetMouseButtonDown(0))
        {
            if (CheckForTile(cTile))
            {
                CalcMoveVars();
            }
        }
        if (isMoving)
        {
            float coveredDist = (Time.time - startTime) * movementSpeed;

            float fractionOfDist = coveredDist / distance;

            transform.position = Vector3.Lerp(currentPos, nextPos, fractionOfDist);
            if (transform.position == nextPos)
            {
                isMoving = false;
                currentPos = nextPos;
                dGrid.DrawCells();
            }
        }
    }

    private void MoveView()
    {
        float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

        transform.localEulerAngles = new Vector3(0, rotationX, mapRotation.z);
        camTransform.localEulerAngles = new Vector3(-rotationY, 0, mapRotation.z);
    }

    #region TileFunctions
    private void SetTileColor(LineRenderer tile, Color color, int orderLayer)
    {
        if (tile != null)
        {
            cTile.material.SetColor("_BaseColor", color);
            cTile.sortingOrder = orderLayer;
        }
    }

    private LineRenderer GetTile()
    {
        RaycastHit hit;
        if (Physics.Raycast(camTransform.position, camTransform.forward, out hit, 40))
        {
            if (hit.transform.GetComponent<LineRenderer>())
            {
                return hit.transform.GetComponent<LineRenderer>();
            }
        }
        return null;
    }

    private bool CheckForTile(LineRenderer newTile)
    {
        if (newTile != null)
        {
            nextPos = newTile.transform.position;
            nextPos.x += 0.5f;
            nextPos.y = transform.position.y;
            nextPos.z += 0.5f;
            if (currentPos != nextPos)
            {
                isMoving = true;
                return true;
            }
        }
        return false;
    }

    private void CalcMoveVars()
    {
        startTime = Time.time;
        distance = Vector3.Distance(currentPos, nextPos);
        amountMoved = currentPos - nextPos;
    }
    #endregion

}
