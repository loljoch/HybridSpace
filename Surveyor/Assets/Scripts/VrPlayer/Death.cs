using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Death : MonoBehaviour
{
    public SteamVR_Fade fade;

    private void Start()
    {
        fade.OnStartFade(Color.black, 1, true);
    }

    private void Die()
    {
        fade.OnStartFade(Color.black, 1, true);
        //Teleport player

    }
}
