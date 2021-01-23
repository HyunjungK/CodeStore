using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class cUIManager : MonoBehaviour
{
    public string BtnSound;

    private cAudioManager theAudio;
    // Start is called before the first frame update
    void Start()
    {
        theAudio = FindObjectOfType<cAudioManager>();
    }

    public void ButtonClick(string type)
    {
        theAudio.Play(BtnSound);
        switch(type)
        {
            case "PLAY":
                SceneManager.LoadScene("Main");
                break;
            case "EXIT":
                Application.Quit();
                break;

        }
    }
}
