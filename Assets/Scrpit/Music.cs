using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    AudioSource Sound;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnEnable()
    {
        Sound=gameObject.GetComponent<AudioSource>();
        Sound.clip = (AudioClip)Resources.Load("bgm", typeof(AudioClip));
        Sound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
