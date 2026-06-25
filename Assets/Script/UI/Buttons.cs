using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] GameObject desc;
    private AudioSource sound;
    private void Start()
    {
        sound = GetComponent<AudioSource>();
    }
    bool isDesc = false;
   public void Title()
    {
        SceneManager.LoadScene("Title");
    }

    public void Description()
    {
        sound.Play();
        isDesc = !isDesc;
        desc.SetActive(isDesc);
    }
}
