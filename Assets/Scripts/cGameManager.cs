using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cGameManager : MonoBehaviour
{
    private cDBManager data;

    public GameObject pokemonScroll, itemScroll , BackScroll, SoundScroll;
    public string curType = "Pokemon"; //현재 선택된 탭 이름
    public GameObject[] SlotPokemon, SlotItem, SlotBack, SlotSound; //슬롯
    public Image[] TabImage, PokemonImage, ItemImage, BackImage, SoundImage; //탭, 아이템, 배경 이미지 저장할 변수
    public Sprite TabIdleSprite, TabSelectSprite; //탭 활성/비활성 이미지
    public Sprite[] PokemonSprite, ItemSprite, BackSprite, SoundSprite; //포켓몬, 아이템, 배경 이미지
    public Text[] PokemonPrice, ItemPrice, BackLevel, SoundLevel; //포켓몬, 아이템, 배경 가격
    public Text[] BackBtnText, SoundBtnText; //배경, 음악 사용 중인지 확인

    public string buyBtnSound;
    private cAudioManager theAudio;

    public cBackChange backChange;

    public cBGMManager bgm;

    private void Awake()
    {
        data = FindObjectOfType<cDBManager>();
        theAudio = FindObjectOfType<cAudioManager>();

    }
   //Start is called before the first frame update
    void Start()
    {
        pokemonScroll.SetActive(true);
        itemScroll.SetActive(false);
        BackScroll.SetActive(false);
        SoundScroll.SetActive(false);

        backChange.BackNum = data.stateList[0].curBackIdx;
        bgm.Play(data.stateList[0].curSoundIdx);
        TabClick(curType);
    }

    public void TabClick(string tabName)
    {
        //현재 아이템 리스트에 클릭한 타입만 추가
        curType = tabName;
        //tabName과 일치하는 항목 가져오기
        if(curType=="Pokemon")
        {
            pokemonScroll.SetActive(true);
            itemScroll.SetActive(false);
            BackScroll.SetActive(false);
            SoundScroll.SetActive(false);

            //포켓몬 슬롯과 텍스트 보이기
            for (int i = 0; i < SlotPokemon.Length; i++)
            {
                bool isExist = i < data.PokemonList.Count;
                SlotPokemon[i].SetActive(isExist);
                //슬롯의 각 항목의 설명 불러오기
                SlotPokemon[i].GetComponentInChildren<Text>().text = isExist ? data.PokemonList[i].Name : "";
                //슬롯의 각 항목의 가격 불러오기
                PokemonPrice[i].text = isExist ? data.PokemonList[i].Price.ToString() : "";

                if (isExist)
                {
                    //슬롯의 각 항목에 해당하는 이미지 불러오기
                    PokemonImage[i].sprite = PokemonSprite[data.PokemonList.FindIndex(x=>x.Name==data.PokemonList[i].Name)];
                }
            }
        }
            
        else if (curType == "Equip")
        {
            pokemonScroll.SetActive(false);
            itemScroll.SetActive(true);
            BackScroll.SetActive(false);
            SoundScroll.SetActive(false);

            //아이템 슬롯과 텍스트 보이기
            for (int i = 0; i < SlotItem.Length; i++)
            {
                bool isExist = i < data.ItemList.Count;
                SlotItem[i].SetActive(isExist);
                //슬롯의 각 항목의 설명 불러오기
                SlotItem[i].GetComponentInChildren<Text>().text = isExist ? data.ItemList[i].Name : "";
                //슬롯의 각 항목의 가격 불러오기
                ItemPrice[i].text = isExist ? data.ItemList[i].Price.ToString() : "";

                if (isExist)
                {
                    //슬롯의 각 항목에 해당하는 이미지 불러오기
                    ItemImage[i].sprite = ItemSprite[data.ItemList.FindIndex(x => x.Name == data.ItemList[i].Name)];
                }
            }
        }

        else if (curType == "Back")
        {
            pokemonScroll.SetActive(false);
            itemScroll.SetActive(false);
            BackScroll.SetActive(true);
            SoundScroll.SetActive(false);

            //아이템 슬롯과 텍스트 보이기
            for (int i = 0; i < SlotBack.Length; i++)
            {
                bool isExist = i < data.BackList.Count;
                SlotBack[i].SetActive(isExist);
                //슬롯의 각 항목의 설명 불러오기
                SlotBack[i].GetComponentInChildren<Text>().text = isExist ? data.BackList[i].Name : "";
                //슬롯의 각 항목의 가격 불러오기
                BackLevel[i].text = isExist ? data.BackList[i].Level.ToString() : "";

                if (data.BackList[i].isActive)
                    BackBtnText[i].text = "사용 중";
                else
                    BackBtnText[i].text = "사용";

                if (isExist)
                {
                    //슬롯의 각 항목에 해당하는 이미지 불러오기
                    BackImage[i].sprite = BackSprite[data.BackList.FindIndex(x => x.Name == data.BackList[i].Name)];
                }
            }
        }

        else if (curType == "Sound")
        {
            pokemonScroll.SetActive(false);
            itemScroll.SetActive(false);
            BackScroll.SetActive(false);
            SoundScroll.SetActive(true);

            //아이템 슬롯과 텍스트 보이기
            for (int i = 0; i < SlotSound.Length; i++)
            {
                bool isExist = i < data.SoundList.Count;
                SlotSound[i].SetActive(isExist);
                //슬롯의 각 항목의 설명 불러오기
                SlotSound[i].GetComponentInChildren<Text>().text = isExist ? data.SoundList[i].Name : "";
                //슬롯의 각 항목의 가격 불러오기
                SoundLevel[i].text = isExist ? data.SoundList[i].Level.ToString() : "";

                if (data.SoundList[i].isActive)
                    SoundBtnText[i].text = "사용 중";
                else
                    SoundBtnText[i].text = "사용";

                if (isExist)
                {
                    //슬롯의 각 항목에 해당하는 이미지 불러오기
                    SoundImage[i].sprite = SoundSprite[data.SoundList.FindIndex(x => x.Name == data.SoundList[i].Name)];
                }
            }
        }

        int tabNum = 0;
        switch(tabName)
        {
            case "Pokemon":
                tabNum = 0;
                break;
            case "Equip":
                tabNum = 1;
                break;
            case "Back":
                tabNum = 2;
                break;
            case "Sound":
                tabNum = 3;
                break;
        }
        for(int i=0; i<TabImage.Length; i++)
        {
            TabImage[i].sprite = i == tabNum ? TabSelectSprite : TabIdleSprite;
        }
    }

    public void GetItemClick(int _btnIdx)
    {
        theAudio.Play(buyBtnSound);

        switch (_btnIdx)
        {
            case 0:
                if(curType=="Pokemon" && data.PokemonList[_btnIdx]!=null)
                {
                    data.PokemonList[_btnIdx].Price += 10;
                    data.stateList[0].money -= data.PokemonList[_btnIdx].Price;
                }
                else if(curType=="Equip" && data.ItemList[_btnIdx]!=null)
                {
                    data.ItemList[_btnIdx].Price += 5;
                    data.stateList[0].money -= data.ItemList[_btnIdx].Price;
                }
                else if(curType=="Back"&& data.BackList[_btnIdx]!=null)
                {
                    data.stateList[0].curBackIdx = _btnIdx;
                }
                break;
            case 1:
                if (curType == "Pokemon" && data.PokemonList[_btnIdx] != null)
                {
                    data.PokemonList[_btnIdx].Price += 20;
                    data.stateList[0].money -= data.PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && data.ItemList[_btnIdx] != null)
                {
                    data.ItemList[_btnIdx].Price += 15;
                    data.stateList[0].money -= data.ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && data.BackList[_btnIdx] != null)
                {
                    data.stateList[0].curBackIdx = _btnIdx;
                }
                break;
            case 2:
                if(curType=="Pokemon" && data.PokemonList[_btnIdx]!=null)
                {
                    data.PokemonList[_btnIdx].Price += 30;
                    data.stateList[0].money -= data.PokemonList[_btnIdx].Price;
                }
                else if(curType=="Equip" && data.ItemList[_btnIdx]!=null)
                {
                    data.ItemList[_btnIdx].Price += 25;
                    data.stateList[0].money -= data.ItemList[_btnIdx].Price;
                }
                else if(curType=="Back" && data.BackList[_btnIdx]!=null)
                {
                    data.stateList[0].curBackIdx = _btnIdx;
                }   
                break;
            case 3:
                if(curType=="Pokemon" && data.PokemonList[_btnIdx]!=null)
                {
                    data.PokemonList[_btnIdx].Price += 40;
                    data.stateList[0].money -= data.PokemonList[_btnIdx].Price;
                }
                else if(curType=="Equip" && data.ItemList[_btnIdx]!=null)
                {
                    data.ItemList[_btnIdx].Price += 35;
                    data.stateList[0].money -= data.ItemList[_btnIdx].Price;
                }
                else if(curType=="Back" && data.BackList[_btnIdx]!=null)
                {
                    data.stateList[0].curBackIdx = _btnIdx;
                }
                break;
            case 4:
                if (curType == "Pokemon" && data.PokemonList[_btnIdx] != null)
                {
                    data.PokemonList[_btnIdx].Price += 40;
                    data.stateList[0].money -= data.PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && data.ItemList[_btnIdx] != null)
                {
                    data.ItemList[_btnIdx].Price += 35;
                    data.stateList[0].money -= data.ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && data.BackList[_btnIdx] != null)
                {
                    data.stateList[0].curBackIdx = _btnIdx;
                }
                break;
            case 5:
                if (curType == "Pokemon" && data.PokemonList[_btnIdx] != null)
                {
                    data.PokemonList[_btnIdx].Price += 40;
                    data.stateList[0].money -= data.PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && data.ItemList[_btnIdx] != null)
                {
                    data.ItemList[_btnIdx].Price += 35;
                    data.stateList[0].money -= data.ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && data.BackList[_btnIdx] != null)
                {
                    data.stateList[0].curBackIdx = _btnIdx;
                }
                break;
            case 6:
                if (curType == "Pokemon" && data.PokemonList[_btnIdx] != null)
                {
                    data.PokemonList[_btnIdx].Price += 40;
                    data.stateList[0].money -= data.PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && data.ItemList[_btnIdx] != null)
                {
                    data.ItemList[_btnIdx].Price += 35;
                    data.stateList[0].money -= data.ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && data.BackList[_btnIdx] != null)
                {
                    data.stateList[0].curBackIdx = _btnIdx;
                }
                break;
            case 7:
                if (curType == "Pokemon" && data.PokemonList[_btnIdx] != null)
                {
                    data.PokemonList[_btnIdx].Price += 40;
                    data.stateList[0].money -= data.PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && data.ItemList[_btnIdx] != null)
                {
                    data.ItemList[_btnIdx].Price += 35;
                    data.stateList[0].money -= data.ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && data.BackList[_btnIdx] != null)
                {
                    data.stateList[0].curBackIdx = _btnIdx;
                }
                break;
            case 8:
                if (curType == "Pokemon" && data.PokemonList[_btnIdx] != null)
                {
                    data.PokemonList[_btnIdx].Price += 40;
                    data.stateList[0].money -= data.PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && data.ItemList[_btnIdx] != null)
                {
                    data.ItemList[_btnIdx].Price += 35;
                    data.stateList[0].money -= data.ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && data.BackList[_btnIdx] != null)
                {
                    data.stateList[0].curBackIdx = _btnIdx;
                }
                break;
            case 9:
                if (curType == "Pokemon" && data.PokemonList[_btnIdx] != null)
                {
                    data.PokemonList[_btnIdx].Price += 40;
                    data.stateList[0].money -= data.PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && data.ItemList[_btnIdx] != null)
                {
                    data.ItemList[_btnIdx].Price += 35;
                    data.stateList[0].money -= data.ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && data.BackList[_btnIdx] != null)
                {
                    data.stateList[0].curBackIdx = _btnIdx;
                }
                break;
            case 10:
                if (curType == "Pokemon" && data.PokemonList[_btnIdx] != null)
                {
                    data.PokemonList[_btnIdx].Price += 40;
                    data.stateList[0].money -= data.PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && data.ItemList[_btnIdx] != null)
                {
                    data.ItemList[_btnIdx].Price += 35;
                    data.stateList[0].money -= data.ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && data.BackList[_btnIdx] != null)
                {
                    data.stateList[0].curBackIdx = _btnIdx;
                }
                break;
            case 11:
                if (curType == "Pokemon" && data.PokemonList[_btnIdx] != null)
                {
                    data.PokemonList[_btnIdx].Price += 40;
                    data.stateList[0].money -= data.PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && data.ItemList[_btnIdx] != null)
                {
                    data.ItemList[_btnIdx].Price += 35;
                    data.stateList[0].money -= data.ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && data.BackList[_btnIdx] != null)
                {
                    data.stateList[0].curBackIdx = _btnIdx;
                }
                break;
            case 12:
                if (curType == "Pokemon" && data.PokemonList[_btnIdx] != null)
                {
                    data.PokemonList[_btnIdx].Price += 40;
                    data.stateList[0].money -= data.PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && data.ItemList[_btnIdx] != null)
                {
                    data.ItemList[_btnIdx].Price += 35;
                    data.stateList[0].money -= data.ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && data.BackList[_btnIdx] != null)
                {
                    data.stateList[0].curBackIdx = _btnIdx;
                }
                break;
            case 13:
                if (curType == "Pokemon" && data.PokemonList[_btnIdx] != null)
                {
                    data.PokemonList[_btnIdx].Price += 40;
                    data.stateList[0].money -= data.PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && data.ItemList[_btnIdx] != null)
                {
                    data.ItemList[_btnIdx].Price += 35;
                    data.stateList[0].money -= data.ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && data.BackList[_btnIdx] != null)
                {
                    data.stateList[0].curBackIdx = _btnIdx;
                }
                break;
            case 14:
                if (curType == "Pokemon" && data.PokemonList[_btnIdx] != null)
                {
                    data.PokemonList[_btnIdx].Price += 40;
                    data.stateList[0].money -= data.PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && data.ItemList[_btnIdx] != null)
                {
                    data.ItemList[_btnIdx].Price += 35;
                    data.stateList[0].money -= data.ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && data.BackList[_btnIdx] != null)
                {
                    data.stateList[0].curBackIdx = _btnIdx;
                }
                break;
            case 15:
                if (curType == "Pokemon" && data.PokemonList[_btnIdx] != null)
                {
                    data.PokemonList[_btnIdx].Price += 40;
                    data.stateList[0].money -= data.PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && data.ItemList[_btnIdx] != null)
                {
                    data.ItemList[_btnIdx].Price += 35;
                    data.stateList[0].money -= data.ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && data.BackList[_btnIdx] != null)
                {
                    data.stateList[0].curBackIdx = _btnIdx;
                }
                break;
            case 16:
                if (curType == "Pokemon" && data.PokemonList[_btnIdx] != null)
                {
                    data.PokemonList[_btnIdx].Price += 40;
                    data.stateList[0].money -= data.PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && data.ItemList[_btnIdx] != null)
                {
                    data.ItemList[_btnIdx].Price += 35;
                    data.stateList[0].money -= data.ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && data.BackList[_btnIdx] != null)
                {
                    data.stateList[0].curBackIdx = _btnIdx;
                }
                break;
            case 17:
                if (curType == "Pokemon" && data.PokemonList[_btnIdx] != null)
                {
                    data.PokemonList[_btnIdx].Price += 40;
                    data.stateList[0].money -= data.PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && data.ItemList[_btnIdx] != null)
                {
                    data.ItemList[_btnIdx].Price += 35;
                    data.stateList[0].money -= data.ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && data.BackList[_btnIdx] != null)
                {
                    data.stateList[0].curBackIdx = _btnIdx;
                }
                break;
            case 18:
                if (curType == "Pokemon" && data.PokemonList[_btnIdx] != null)
                {
                    data.PokemonList[_btnIdx].Price += 40;
                    data.stateList[0].money -= data.PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && data.ItemList[_btnIdx] != null)
                {
                    data.ItemList[_btnIdx].Price += 35;
                    data.stateList[0].money -= data.ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && data.BackList[_btnIdx] != null)
                {
                    data.stateList[0].curBackIdx = _btnIdx;
                }
                break;
            case 19:
                if (curType == "Pokemon" && data.PokemonList[_btnIdx] != null)
                {
                    data.PokemonList[_btnIdx].Price += 40;
                    data.stateList[0].money -= data.PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && data.ItemList[_btnIdx] != null)
                {
                    data.ItemList[_btnIdx].Price += 35;
                    data.stateList[0].money -= data.ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && data.BackList[_btnIdx] != null)
                {
                    data.stateList[0].curBackIdx = _btnIdx;
                }
                break;
            case 20:
                if (curType == "Pokemon" && data.PokemonList[_btnIdx] != null)
                {
                    data.PokemonList[_btnIdx].Price += 40;
                    data.stateList[0].money -= data.PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && data.ItemList[_btnIdx] != null)
                {
                    data.ItemList[_btnIdx].Price += 35;
                    data.stateList[0].money -= data.ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && data.BackList[_btnIdx] != null)
                {
                    data.stateList[0].curBackIdx = _btnIdx;
                }
                break;
            case 21:
                if (curType == "Pokemon" && data.PokemonList[_btnIdx] != null)
                {
                    data.PokemonList[_btnIdx].Price += 40;
                    data.stateList[0].money -= data.PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && data.ItemList[_btnIdx] != null)
                {
                    data.ItemList[_btnIdx].Price += 35;
                    data.stateList[0].money -= data.ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && data.BackList[_btnIdx] != null)
                {
                    data.stateList[0].curBackIdx = _btnIdx;
                }
                break;
            case 22:
                if (curType == "Pokemon" && data.PokemonList[_btnIdx] != null)
                {
                    data.PokemonList[_btnIdx].Price += 40;
                    data.stateList[0].money -= data.PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && data.ItemList[_btnIdx] != null)
                {
                    data.ItemList[_btnIdx].Price += 35;
                    data.stateList[0].money -= data.ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && data.BackList[_btnIdx] != null)
                {
                    data.stateList[0].curBackIdx = _btnIdx;
                }
                break;
            case 23:
                if (curType == "Pokemon" && data.PokemonList[_btnIdx] != null)
                {
                    data.PokemonList[_btnIdx].Price += 40;
                    data.stateList[0].money -= data.PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && data.ItemList[_btnIdx] != null)
                {
                    data.ItemList[_btnIdx].Price += 35;
                    data.stateList[0].money -= data.ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && data.BackList[_btnIdx] != null)
                {
                    data.stateList[0].curBackIdx = _btnIdx;
                }
                break;
            case 24:
                if (curType == "Pokemon" && data.PokemonList[_btnIdx] != null)
                {
                    data.PokemonList[_btnIdx].Price += 40;
                    data.stateList[0].money -= data.PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && data.ItemList[_btnIdx] != null)
                {
                    data.ItemList[_btnIdx].Price += 35;
                    data.stateList[0].money -= data.ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && data.BackList[_btnIdx] != null)
                {
                    data.stateList[0].curBackIdx = _btnIdx;
                }
                break;
            case 25:
                if (curType == "Pokemon" && data.PokemonList[_btnIdx] != null)
                {
                    data.PokemonList[_btnIdx].Price += 40;
                    data.stateList[0].money -= data.PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && data.ItemList[_btnIdx] != null)
                {
                    data.ItemList[_btnIdx].Price += 35;
                    data.stateList[0].money -= data.ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && data.BackList[_btnIdx] != null)
                {
                    data.stateList[0].curBackIdx = _btnIdx;
                }
                break;
            case 26:
                if (curType == "Pokemon" && data.PokemonList[_btnIdx] != null)
                {
                    data.PokemonList[_btnIdx].Price += 40;
                    data.stateList[0].money -= data.PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && data.ItemList[_btnIdx] != null)
                {
                    data.ItemList[_btnIdx].Price += 35;
                    data.stateList[0].money -= data.ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && data.BackList[_btnIdx] != null)
                {
                    data.stateList[0].curBackIdx = _btnIdx;
                }
                break;
            case 27:
                if (curType == "Pokemon" && data.PokemonList[_btnIdx] != null)
                {
                    data.PokemonList[_btnIdx].Price += 40;
                    data.stateList[0].money -= data.PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && data.ItemList[_btnIdx] != null)
                {
                    data.ItemList[_btnIdx].Price += 35;
                    data.stateList[0].money -= data.ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && data.BackList[_btnIdx] != null)
                {
                    data.stateList[0].curBackIdx = _btnIdx;
                }
                break;
            case 28:
                if (curType == "Pokemon" && data.PokemonList[_btnIdx] != null)
                {
                    data.PokemonList[_btnIdx].Price += 40;
                    data.stateList[0].money -= data.PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && data.ItemList[_btnIdx] != null)
                {
                    data.ItemList[_btnIdx].Price += 35;
                    data.stateList[0].money -= data.ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && data.BackList[_btnIdx] != null)
                {
                    data.stateList[0].curBackIdx = _btnIdx;
                }
                break;
            case 29:
                if (curType == "Pokemon" && data.PokemonList[_btnIdx] != null)
                {
                    data.PokemonList[_btnIdx].Price += 40;
                    data.stateList[0].money -= data.PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && data.ItemList[_btnIdx] != null)
                {
                    data.ItemList[_btnIdx].Price += 35;
                    data.stateList[0].money -= data.ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && data.BackList[_btnIdx] != null)
                {
                    data.stateList[0].curBackIdx = _btnIdx;
                }
                break;

        }

        for (int i = 0; i < SlotBack.Length; i++)
        {
            data.BackList[i].isActive = false;
            if (i == data.stateList[0].curBackIdx)
                data.BackList[i].isActive = true;
        }

        data.Save();
    }

    public void GetSoundClick(int _btnIdx)
    {
        theAudio.Play(buyBtnSound);

        switch (_btnIdx)
        {
            case 0:
                if (curType == "Sound" && data.SoundList[_btnIdx] != null)
                {
                    data.stateList[0].curSoundIdx = _btnIdx;
                }
                break;
            case 1:
                if (curType == "Sound" && data.SoundList[_btnIdx] != null)
                {
                    data.stateList[0].curSoundIdx = _btnIdx;
                }
                break;
            case 2:
                if (curType == "Sound" && data.SoundList[_btnIdx] != null)
                {
                    data.stateList[0].curSoundIdx = _btnIdx;
                }
                break;
            case 3:
                if (curType == "Sound" && data.SoundList[_btnIdx] != null)
                {
                    data.stateList[0].curSoundIdx = _btnIdx;
                }
                break;
            case 4:
                if (curType == "Sound" && data.SoundList[_btnIdx] != null)
                {
                    data.stateList[0].curSoundIdx = _btnIdx;
                }
                break;
            case 5:
                if (curType == "Sound" && data.SoundList[_btnIdx] != null)
                {
                    data.stateList[0].curSoundIdx = _btnIdx;
                }
                break;
            case 6:
                if (curType == "Sound" && data.SoundList[_btnIdx] != null)
                {
                    data.stateList[0].curSoundIdx = _btnIdx;
                }
                break;
            case 7:
                if (curType == "Sound" && data.SoundList[_btnIdx] != null)
                {
                    data.stateList[0].curSoundIdx = _btnIdx;
                }
                break;
            case 8:
                if (curType == "Sound" && data.SoundList[_btnIdx] != null)
                {
                    data.stateList[0].curSoundIdx = _btnIdx;
                }
                break;
            case 9:
                if (curType == "Sound" && data.SoundList[_btnIdx] != null)
                {
                    data.stateList[0].curSoundIdx = _btnIdx;
                }
                break;
            case 10:
                if (curType == "Sound" && data.SoundList[_btnIdx] != null)
                {
                    data.stateList[0].curSoundIdx = _btnIdx;
                }
                break;
            case 11:
                if (curType == "Sound" && data.SoundList[_btnIdx] != null)
                {
                    data.stateList[0].curSoundIdx = _btnIdx;
                }
                break;
            case 12:
                if (curType == "Sound" && data.SoundList[_btnIdx] != null)
                {
                    data.stateList[0].curSoundIdx = _btnIdx;
                }
                break;
            case 13:
                if (curType == "Sound" && data.SoundList[_btnIdx] != null)
                {
                    data.stateList[0].curSoundIdx = _btnIdx;
                }
                break;
            case 14:
                if (curType == "Sound" && data.SoundList[_btnIdx] != null)
                {
                    data.stateList[0].curSoundIdx = _btnIdx;
                }
                break;
            case 15:
                if (curType == "Sound" && data.SoundList[_btnIdx] != null)
                {
                    data.stateList[0].curSoundIdx = _btnIdx;
                }
                break;
            case 16:
                if (curType == "Sound" && data.SoundList[_btnIdx] != null)
                {
                    data.stateList[0].curSoundIdx = _btnIdx;
                }
                break;
            case 17:
                if (curType == "Sound" && data.SoundList[_btnIdx] != null)
                {
                    data.stateList[0].curSoundIdx = _btnIdx;
                }
                break;
            case 18:
                if (curType == "Sound" && data.SoundList[_btnIdx] != null)
                {
                    data.stateList[0].curSoundIdx = _btnIdx;
                }
                break;
            case 19:
                if (curType == "Sound" && data.SoundList[_btnIdx] != null)
                {
                    data.stateList[0].curSoundIdx = _btnIdx;
                }
                break;
            case 20:
                if (curType == "Sound" && data.SoundList[_btnIdx] != null)
                {
                    data.stateList[0].curSoundIdx = _btnIdx;
                }
                break;
            case 21:
                if (curType == "Sound" && data.SoundList[_btnIdx] != null)
                {
                    data.stateList[0].curSoundIdx = _btnIdx;
                }
                break;
            case 22:
                if (curType == "Sound" && data.SoundList[_btnIdx] != null)
                {
                    data.stateList[0].curSoundIdx = _btnIdx;
                }
                break;
            case 23:
                if (curType == "Sound" && data.SoundList[_btnIdx] != null)
                {
                    data.stateList[0].curSoundIdx = _btnIdx;
                }
                break;
            case 24:
                if (curType == "Sound" && data.SoundList[_btnIdx] != null)
                {
                    data.stateList[0].curSoundIdx = _btnIdx;
                }
                break;
            case 25:
                if (curType == "Sound" && data.SoundList[_btnIdx] != null)
                {
                    data.stateList[0].curSoundIdx = _btnIdx;
                }
                break;
            case 26:
                if (curType == "Sound" && data.SoundList[_btnIdx] != null)
                {
                    data.stateList[0].curSoundIdx = _btnIdx;
                }
                break;
            case 27:
                if (curType == "Sound" && data.SoundList[_btnIdx] != null)
                {
                    data.stateList[0].curSoundIdx = _btnIdx;
                }
                break;
            case 28:
                if (curType == "Sound" && data.SoundList[_btnIdx] != null)
                {
                    data.stateList[0].curSoundIdx = _btnIdx;
                }
                break;
            case 29:
                if (curType == "Sound" && data.SoundList[_btnIdx] != null)
                {
                    data.stateList[0].curSoundIdx = _btnIdx;
                }
                break;

        }
        bgm.Play(data.stateList[0].curSoundIdx);
        for (int i = 0; i < SlotSound.Length; i++)
        {
            data.SoundList[i].isActive = false;
            if (i == data.stateList[0].curSoundIdx)
                data.SoundList[i].isActive = true;
        }
        data.Save();
    }

    private void Update()
    {
        backChange.BackNum = data.stateList[0].curBackIdx;
        for (int i = 0; i < SlotPokemon.Length; i++)
            PokemonPrice[i].text = i < data.PokemonList.Count ? data.PokemonList[i].Price.ToString() : "";
        for (int i = 0; i < SlotItem.Length; i++)
            ItemPrice[i].text = i < data.ItemList.Count ? data.ItemList[i].Price.ToString() : "";
        for (int i = 0; i < SlotBack.Length; i++)
        {
            BackLevel[i].text = i < data.BackList.Count ? data.BackList[i].Level.ToString() : "";
            if (data.BackList[i].isActive)
                BackBtnText[i].text = "사용 중";
            else
                BackBtnText[i].text = "사용";
        }
        for (int i = 0; i < SlotSound.Length; i++)
        {
            SoundLevel[i].text = i < data.SoundList.Count ? data.SoundList[i].Level.ToString() : "";
            if (data.SoundList[i].isActive)
                SoundBtnText[i].text = "사용 중";
            else
                SoundBtnText[i].text = "사용";
        }         

    }
}
