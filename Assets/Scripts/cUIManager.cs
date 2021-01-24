using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class cUIManager : MonoBehaviour
{
    public string BtnSound;

    private cAudioManager theAudio;
    private cDBManager data;
    // Start is called before the first frame update
    void Start()
    {
        data = FindObjectOfType<cDBManager>();
        theAudio = FindObjectOfType<cAudioManager>();
    }

    public void ButtonClick(string type)
    {
        theAudio.Play(BtnSound);
        switch(type)
        {
            case "NEW":
                if (File.Exists(Application.persistentDataPath + "/Resources/Pokemons.json"))
                    data._Reset();
                SceneManager.LoadScene("Main");
                break;
            case "CONTINUE":
                SceneManager.LoadScene("Main");
                break;
            case "EXIT":
                Application.Quit();
                break;

        }
    }
}
