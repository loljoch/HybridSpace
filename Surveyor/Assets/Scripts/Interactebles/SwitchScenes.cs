using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
    [SerializeField] private AudioClip wooshSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private DrawGrid gridDraw;

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            SwitchToDarkScene();
            gridDraw.DrawCells();
        }
    }
    
    public void SwitchToDarkScene()
    {
        audioSource.PlayOneShot(wooshSound);
        SceneManager.LoadScene("DocentenLevel_Phase2", LoadSceneMode.Single);
    }
}
