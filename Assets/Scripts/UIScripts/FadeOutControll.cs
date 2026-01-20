using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutControll : MonoBehaviour
{
    [SerializeField] public GameObject FadeoutObject;
    [SerializeField] public GameObject FadeInObject;
    [SerializeField] public AudioClip Koukaon;
    private AudioSource audioSource;

    public void FadeOut()
    {
        FadeoutObject.SetActive(false);
    }

    public void FadeIn()
    {
        FadeInObject.SetActive(true);
    }

    public void PlayMusic()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = Koukaon;
        audioSource.Play();
    }
}
