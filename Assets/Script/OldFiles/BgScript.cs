using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BgScript : MonoBehaviour
{
    public AudioMixer audiomixer;
    private void Awake()
    {
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("gamemusic");
        if (musicObj.Length>1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }

    public void SetVolume (float volume)
    {
        audiomixer.SetFloat("volume", volume);
    }
}
