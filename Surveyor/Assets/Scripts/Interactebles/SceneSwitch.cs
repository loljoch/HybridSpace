using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    [SerializeField] private AudioClip wooshSound;
    [SerializeField] private AudioSource audioSource;

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            SwitchToDarkScene();
        }
    }
    
    public void SwitchToDarkScene()
    {
        audioSource.PlayOneShot(wooshSound);
        SceneManager.LoadScene("DocentenLevel_Phase2", LoadSceneMode.Single);
    }
}
