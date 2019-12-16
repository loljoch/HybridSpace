using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;

public class LevelBuilder : MonoBehaviour
{
    enum CustomColor
    {
        Selected,
        Deselected,
        GreyedOut,
        Normal,
        Overlapping
    }

    private MeshRenderer previousRenderer;
    private Color selectedBaseColor = new Color(0.502f, 0.502f, 1, 0.314f);
    private Color deselectedBaseColor = new Color(1, 1, 0.502f, 0.314f);
    [SerializeField] private LayerMask buildingPlatformLayer, transparentLayer, defaultLayer;

    [SerializeField] private GameObject[] platforms;
    private int currentPlatformIndex = 0;
    private GameObject currentPlatform;
    private MeshRenderer currentPlatformRend;
    private MeshRenderer overlappedPlatformRend;
    private List<GameObject> maze = new List<GameObject>();
    [SerializeField] TMPro.TextMeshProUGUI inputField;
    private string filename;

    private void Start()
    {
        SpawnPlatform();
    }

    private void Update()
    {
        MeshRenderer cSelected = GetCurrentSelected();
        if (cSelected != previousRenderer && previousRenderer != null)
        {
            ChangeColor(previousRenderer.material, CustomColor.Deselected);
            if (overlappedPlatformRend != null)
            {
                ChangeColor(overlappedPlatformRend.material, CustomColor.Normal);
                overlappedPlatformRend = null;
            }
        }
        if (cSelected == null)
        {
            if (currentPlatform != null)
            {
                currentPlatform.SetActive(false);
            }
            return;
        }
        currentPlatform.SetActive(true);
        ChangeColor(cSelected.material, CustomColor.Selected);
        if (cSelected.transform.childCount > 0)
        {
            currentPlatform.SetActive(false);
            overlappedPlatformRend = cSelected.transform.GetChild(0).GetComponent<MeshRenderer>();
        } else
        {
            ChangeColor(currentPlatformRend.material, CustomColor.GreyedOut);
        }

        SwitchPlatform(Input.GetAxis("Mouse ScrollWheel"));
        currentPlatform.transform.position = cSelected.transform.position;

        if (Input.GetMouseButtonDown(1))
        {
            RotatePlatform(overlappedPlatformRend.transform);
        }

        //Setplatform
        if (Input.GetMouseButtonDown(0))
        {
            if (overlappedPlatformRend != null)
            {
                Destroy(overlappedPlatformRend.gameObject);
                overlappedPlatformRend = null;
            } else
            {
                SetPlatform(cSelected.transform);
            }
        }

        previousRenderer = cSelected;
    }

    public void SaveMaze()
    {
        List<string> roomNames = new List<string>();
        List<Vector3> roomPositions = new List<Vector3>();
        List<Quaternion> roomRotations = new List<Quaternion>();
        for (int i = 0; i < maze.Count; i++)
        {
            if (maze[i]!= null)
            {
                roomNames.Add(maze[i].name.Replace("(Clone)", ""));
                roomPositions.Add(maze[i].transform.position);
                roomRotations.Add(maze[i].transform.rotation);
            }
        }

        AssetDatabase.CreateAsset(
            new MazeScriptableObject(
                roomNames,
                roomPositions,
                roomRotations,
                FindObjectOfType<GridMaker>().transform.position
                ),
            "Assets/SavedMazes/" + filename + ".asset");
        AssetDatabase.SaveAssets();
    }

    private void RotatePlatform(Transform platform)
    {
        if (platform != null)
        {

            platform.Rotate(Vector3.up, 90);
        }
    }

    private void SetPlatform(Transform selectedTransform)
    {
        GameObject nPlatform = Instantiate(currentPlatform, selectedTransform, true);
        maze.Add(nPlatform);
        nPlatform.layer = defaultLayer - 1;
        MeshRenderer mRend = nPlatform.GetComponent<MeshRenderer>();
        ChangeColor(mRend.material, CustomColor.Normal);
        if (selectedTransform.childCount > 1)
        {
            maze.Remove(selectedTransform.gameObject);
            Destroy(selectedTransform.GetChild(0).gameObject);
        }
    }

    private void SwitchPlatform(float scrollDelta)
    {
        if (scrollDelta > 0f)
        {
            Destroy(currentPlatform);
            if (++currentPlatformIndex > platforms.Length - 1)
            {
                currentPlatformIndex = 0;
            }
            SpawnPlatform();
            EventSystem.current.SetSelectedGameObject(null);
        } else if (scrollDelta < 0f)
        {
            Destroy(currentPlatform);
            if (--currentPlatformIndex < 0)
            {
                currentPlatformIndex = platforms.Length - 1;
            }
            SpawnPlatform();
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void SetPlatformIndex(int index)
    {
        currentPlatformIndex = index;
        Destroy(currentPlatform);
        SpawnPlatform();
    }

    private void SpawnPlatform()
    {
        currentPlatform = Instantiate(platforms[currentPlatformIndex]);
        currentPlatform.layer = transparentLayer - 1;
        currentPlatformRend = currentPlatform.GetComponent<MeshRenderer>();
        ChangeColor(currentPlatform.GetComponent<MeshRenderer>().material, CustomColor.GreyedOut);
    }

    private MeshRenderer GetCurrentSelected()
    {
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 400f, buildingPlatformLayer))
        {
            if (hit.transform.GetComponent<MeshRenderer>() != null)
            {
                return hit.transform.GetComponent<MeshRenderer>();
            }
        }

        return null;
    }

    private void ChangeColor(Material mat, CustomColor color)
    {
        switch (color)
        {
            case CustomColor.Selected:
                mat.SetColor("_BaseColor", selectedBaseColor);
                break;
            case CustomColor.Deselected:
                mat.SetColor("_BaseColor", deselectedBaseColor);
                break;
            case CustomColor.GreyedOut:
                mat.SetColor("_BaseColor", Color.green);
                break;
            case CustomColor.Normal:
                mat.SetColor("_BaseColor", Color.gray);
                break;
            case CustomColor.Overlapping:
                mat.SetColor("_BaseColor", Color.red);
                break;
            default:
                break;
        }

    }

    public void SetFileName()
    {
        filename = inputField.text;
    }
}
