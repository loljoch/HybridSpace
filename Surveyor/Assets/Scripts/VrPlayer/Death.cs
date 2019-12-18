using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Death : MonoBehaviour
{
    public Transform spawnPoint;

    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private DrawGrid drawGrid;
    [SerializeField] private Transform playerTransform;

    public void Die()
    {
        audioSource.PlayOneShot(deathSound);
        SteamVR_Fade.Start(Color.clear, 0);
        Color lightRed = Color.red;
        lightRed.a = 0.5f;
        SteamVR_Fade.Start(lightRed, 1);
        drawGrid.gridParent.gameObject.SetActive(false);
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1f);
        SteamVR_Fade.Start(Color.black, 1);
        yield return new WaitForSeconds(1f);
        playerTransform.position = spawnPoint.position;
        SteamVR_Fade.Start(Color.clear, 1);
        yield return new WaitForSeconds(1f);
        drawGrid.gridParent.gameObject.SetActive(true);
        drawGrid.DrawCells();
    }
}
