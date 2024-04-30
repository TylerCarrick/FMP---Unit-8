using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound2 : MonoBehaviour
{
    public AudioSource soundPlayer;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    public void playThisSoundEffect()
    {
        soundPlayer.Play();
    }

}
