using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cButtonController : MonoBehaviour
{
    public cGameManager manager;

    public Button[] PokemonBtn, ItemBtn, BackBtn, SoundBtn; //포켓몬, 아이템 구입, 배경, 사운드 사용 버튼
    public GameObject option;
    bool isActive = true;
    int btnidx = 0;
    public Scrollbar effect;

    private cAudioManager theAudio;
    public string BtnSound;

    private void Awake()
    {
        theAudio = FindObjectOfType<cAudioManager>();
        effect.GetComponent<Scrollbar>();
        
    }
    // Start is called before the first frame update
    void Start()
    {

        option.SetActive(false);
        for (int i = 0; i < manager.SlotPokemon.Length; i++)
        {
            if (manager.player.stateList[0].money < manager.PokemonList[i].Price)
                PokemonBtn[i].GetComponent<Button>().interactable = false;
        }
        for(int i=0; i<manager.SlotItem.Length; i++)
        {
            if (manager.player.stateList[0].money < manager.ItemList[i].Price)
                ItemBtn[i].GetComponent<Button>().interactable = false;
        }
        for(int i=0; i<manager.SlotBack.Length; i++)
        {
            if (manager.player.stateList[0].level < manager.BackList[i].Level)
                BackBtn[i].GetComponent<Button>().interactable = false;
        }
        for(int i=0; i<manager.SlotSound.Length; i++)
        {
            if (manager.player.stateList[0].level < manager.SoundList[i].Level)
                SoundBtn[i].GetComponent<Button>().interactable = false;
        }
    }

    public void EffectSet()
    {
        theAudio.SetVolume(effect.value);
    }

    public void OptionBtn()
    {
        theAudio.Play(BtnSound);
        if(!isActive)
        {
            option.SetActive(false);
            isActive = true;
        }
        else
        {
            option.SetActive(true);
            isActive = false;
        }
    }
    public void BuyPokemonBtn(int _btnIdx)
    {
        btnidx = _btnIdx;
    }

    public void BuyItemBtn(int _btnIdx)
    {
        btnidx = _btnIdx;
    }

    public void UseBackBtn(int _btnIdx)
    {
        btnidx = _btnIdx;
    }

    public void UseSoundBtn(int _btnIdx)
    {
        btnidx = _btnIdx;
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < manager.SlotPokemon.Length; i++)
        {
            if (manager.player.stateList[0].money < manager.PokemonList[i].Price)
                PokemonBtn[i].GetComponent<Button>().interactable = false;
            else
                PokemonBtn[i].GetComponent<Button>().interactable = true;
        }
        for(int i=0; i<manager.SlotItem.Length; i++)
        {
            if (manager.player.stateList[0].money < manager.ItemList[i].Price)
                ItemBtn[i].GetComponent<Button>().interactable = false;
            else
                ItemBtn[i].GetComponent<Button>().interactable = true;
        }
        for(int i=0; i<manager.SlotBack.Length; i++)
        {
            if (manager.player.stateList[0].level < manager.BackList[i].Level)
                BackBtn[i].GetComponent<Button>().interactable = false;               
            else
                BackBtn[i].GetComponent<Button>().interactable = true;
        }
        for(int i=0; i<manager.SlotSound.Length; i++)
        {
            if (manager.player.stateList[0].level < manager.SoundList[i].Level)
                SoundBtn[i].GetComponent<Button>().interactable = false;
            else
                SoundBtn[i].GetComponent<Button>().interactable = true;
        }
    }
}
