using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.UI;
using System;

[System.Serializable]
public class Pokemon
{
    public Pokemon(string _Type, string _Name, int _Price)
    {
        Type = _Type;
        Name = _Name;
        Price = _Price;
    }
    public string Type, Name;
    public int Price;
}

[System.Serializable]
public class Item
{
    public Item(string _Type, string _Name, int _Price)
    {
        Type = _Type;
        Name = _Name;
        Price = _Price;
    }
    public string Type, Name;
    public int Price;
}

[System.Serializable]
public class Back
{
    public Back(string _Type, string _Name, int _Level, bool _isActive)
    {
        Type = _Type;
        Name = _Name;
        Level = _Level;
        isActive = _isActive;
    }
    public string Type, Name;
    public int Level;
    public bool isActive;
}

[System.Serializable]
public class Sound
{
    public Sound(string _Type, string _Name, int _Level, bool _isActive)
    {
        Type = _Type;
        Name = _Name;
        Level = _Level;
        isActive = _isActive;
    }
    public string Type, Name;
    public int Level;
    public bool isActive;
}

public class cGameManager : MonoBehaviour
{
    public List<Pokemon> PokemonList; //포켓몬DB 저장
    public List<Item> ItemList; //아이템 DB저장
    public List<Back> BackList; //배경 DB저장
    public List<Sound> SoundList; //음악 DB저장

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

    public cPlayerClicker player;

    public cBackChange backChange;

    public cBGMManager bgm;


    private void Awake()
    {
        theAudio = FindObjectOfType<cAudioManager>();
        if (!File.Exists(Application.persistentDataPath + "/Resources/Pokemons.json"))
        {
            //포켓몬 DB 추가
            PokemonList.Add(new Pokemon("Pokemon", "이상해씨", 100));
            PokemonList.Add(new Pokemon("Pokemon", "파이리", 200));
            PokemonList.Add(new Pokemon("Pokemon", "꼬부기", 300));
            PokemonList.Add(new Pokemon("Pokemon", "캐터피", 400));
            PokemonList.Add(new Pokemon("Pokemon", "뿔충이", 500));
            PokemonList.Add(new Pokemon("Pokemon", "구구", 600));
            PokemonList.Add(new Pokemon("Pokemon", "꼬렛", 700));
            PokemonList.Add(new Pokemon("Pokemon", "깨비참", 800));
            PokemonList.Add(new Pokemon("Pokemon", "아보", 900));
            PokemonList.Add(new Pokemon("Pokemon", "피카츄", 1000));
            PokemonList.Add(new Pokemon("Pokemon", "모래두지", 1100));
            PokemonList.Add(new Pokemon("Pokemon", "삐삐", 1200));
            PokemonList.Add(new Pokemon("Pokemon", "식스테일", 1300));
            PokemonList.Add(new Pokemon("Pokemon", "푸린", 1400));
            PokemonList.Add(new Pokemon("Pokemon", "주뱃", 1500));
            PokemonList.Add(new Pokemon("Pokemon", "뚜벅쵸", 1600));
            PokemonList.Add(new Pokemon("Pokemon", "디그다", 1700));
            PokemonList.Add(new Pokemon("Pokemon", "나옹", 1800));
            //장비 DB 추가
            ItemList.Add(new Item("Equip", "검", 50));
            ItemList.Add(new Item("Equip", "카알대검", 100));
            ItemList.Add(new Item("Equip", "초급전사의검", 150));
            ItemList.Add(new Item("Equip", "사브르", 200));
            ItemList.Add(new Item("Equip", "바이킹소드", 250));
            ItemList.Add(new Item("Equip", "글라디우스", 300));
            ItemList.Add(new Item("Equip", "커틀러스", 350));
            ItemList.Add(new Item("Equip", "카타나", 400));
            ItemList.Add(new Item("Equip", "트라우스", 450));
            ItemList.Add(new Item("Equip", "베릴 브링어", 500));
            ItemList.Add(new Item("Equip", "칠성검", 550));
            ItemList.Add(new Item("Equip", "쥬얼 쿠아다라", 600));
            ItemList.Add(new Item("Equip", "글로리 소드", 650));
            ItemList.Add(new Item("Equip", "네오코라", 700));
            ItemList.Add(new Item("Equip", "래티넘 브링어", 750));
            ItemList.Add(new Item("Equip", "스텔라 글라디우스", 800));
            ItemList.Add(new Item("Equip", "파이롭 소드", 850));
            ItemList.Add(new Item("Equip", "아울렛 글라디우스", 900));
            ItemList.Add(new Item("Equip", "레전드 브링어", 950));
            ItemList.Add(new Item("Equip", "프라우테", 1000));
            ItemList.Add(new Item("Equip", "스파타", 1050));
            ItemList.Add(new Item("Equip", "세인트 소드", 1100));
            ItemList.Add(new Item("Equip", "레전드 스파타", 1150));
            ItemList.Add(new Item("Equip", "얼티밋 글라디우스", 1200));
            ItemList.Add(new Item("Equip", "템페스트 글라디우스", 1250));
            ItemList.Add(new Item("Equip", "블랙 소드", 1300));
            ItemList.Add(new Item("Equip", "라피스라소드", 1350));
            ItemList.Add(new Item("Equip", "레볼루션 소드", 1400));
            ItemList.Add(new Item("Equip", "드래곤 카라벨라", 1450));
            ItemList.Add(new Item("Equip", "네크로피어", 1500));
            //배경 DB 추가
            BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "1 증가", 1, true));
            BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "2 증가", 3, false));
            BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "3 증가", 5, false));
            BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "4 증가", 7, false));
            BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "5 증가", 9, false));
            BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "6 증가", 11, false));
            BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "7 증가", 13, false));
            BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "8 증가", 15, false));
            BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "9 증가", 17, false));
            BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "10 증가", 19, false));
            BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "11 증가", 21, false));
            BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "12 증가", 23, false));
            BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "13 증가", 25, false));
            BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "14 증가", 27, false));
            BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "15 증가", 29, false));
            BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "16 증가", 31, false));
            BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "17 증가", 33, false));
            BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "18 증가", 35, false));
            BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "19 증가", 37, false));
            BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "20 증가", 39, false));
            BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "21 증가", 41, false));
            BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "22 증가", 43, false));
            BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "23 증가", 45, false));
            BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "24 증가", 47, false));
            BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "25 증가", 49, false));
            //사운드 DB 추가
            SoundList.Add(new Sound("Sound", "Beneath the Moonlight\n" + "-Aaron Kenny-", 1, true));
            SoundList.Add(new Sound("Sound", "Bittersweet Waltz\n" + "-Sir Cubworth-", 4, false));
            SoundList.Add(new Sound("Sound", "Brass Chorale and Motet\n" + "-Sir Cubworth-", 6, false));
            SoundList.Add(new Sound("Sound", "Butterflies In Love\n" + "-Sir Cubworth-", 8, false));
            SoundList.Add(new Sound("Sound", "Ceremonial Prelude\n" + "-Sir Cubworth-", 10, false));
            SoundList.Add(new Sound("Sound", "Christmas Village\n" + "-Aaron Kenny-", 12, false));
            SoundList.Add(new Sound("Sound", "Dance for Wind Trio\n" + "-Sir Cubworth-", 14, false));
            SoundList.Add(new Sound("Sound", "English Country Garden\n" + "-Aaron Kenny-", 16, false));
            SoundList.Add(new Sound("Sound", "First Sleep\n" + "-Sir Cubworth-", 18, false));
            SoundList.Add(new Sound("Sound", "Heavenly\n" + "-Aakash Gandhi-", 20, false));
            SoundList.Add(new Sound("Sound", "Hopeful Freedom\n" + "-Asher Fulero-", 22, false));
            SoundList.Add(new Sound("Sound", "Hovering Thoughts\n" + "-Spence-", 24, false));
            SoundList.Add(new Sound("Sound", "Invitation\n" + "to the Castle Ball\n" + "-Doug Maxwell-", 26, false));
            SoundList.Add(new Sound("Sound", "Kiss the Sky\n" + "-Aakash Gandhi-", 28, false));
            SoundList.Add(new Sound("Sound", "Lifting Dreams\n" + "-Aakash Gandhi-", 30, false));
            SoundList.Add(new Sound("Sound", "Lullaby\n" + "-Cooper Cannell-", 32, false));
            SoundList.Add(new Sound("Sound", "Lullaby\n" + "-JVNA-", 34, false));
            SoundList.Add(new Sound("Sound", "Minyo San Kyoku\n" + "-Doug Maxwell_ Zac Zinger-", 36, false));
            SoundList.Add(new Sound("Sound", "No.6 In My Dreams\n" + "-Esther Abrami-", 38, false));
            SoundList.Add(new Sound("Sound", "No.9_Esther’s Waltz\n" + "-Esther Abrami-", 40, false));
            SoundList.Add(new Sound("Sound", "November\n" + "-Joey Pecoraro-", 42, false));
            SoundList.Add(new Sound("Sound", "Party Waltz\n" + "-Sir Cubworth-", 44, false));
            SoundList.Add(new Sound("Sound", "Requiem In Cello\n" + "-Hanu Dixit-", 46, false));
            SoundList.Add(new Sound("Sound", "Shattered Paths\n" + "-Aakash Gandhi-", 48, false));
            SoundList.Add(new Sound("Sound", "Star Spangled Banner\n" + "-Cooper Cannell-", 50, false));
            SoundList.Add(new Sound("Sound", "Summer Symphony Ball\n" + "-Sir Cubworth-", 52, false));
            SoundList.Add(new Sound("Sound", "The First Noel\n" + "-Quincas Moreira-", 54, false));
            SoundList.Add(new Sound("Sound", "Theme for a One-\n" + "Handed Piano Concerto\n" + "-Sir Cubworth-", 56, false));
            SoundList.Add(new Sound("Sound", "White River\n" + "-Aakash Gandhi-", 58, false));
            SoundList.Add(new Sound("Sound", "Wistful Harp\n" + "-Andrew Huang-", 60, false));

            Save();
        }
        Load();
    }
   //Start is called before the first frame update
    void Start()
    {
        pokemonScroll.SetActive(true);
        itemScroll.SetActive(false);
        BackScroll.SetActive(false);
        SoundScroll.SetActive(false);

        backChange.BackNum = 0;
        bgm.Play(0);
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
                bool isExist = i < PokemonList.Count;
                SlotPokemon[i].SetActive(isExist);
                //슬롯의 각 항목의 설명 불러오기
                SlotPokemon[i].GetComponentInChildren<Text>().text = isExist ? PokemonList[i].Name : "";
                //슬롯의 각 항목의 가격 불러오기
                PokemonPrice[i].text = isExist ? PokemonList[i].Price.ToString() : "";

                if (isExist)
                {
                    //슬롯의 각 항목에 해당하는 이미지 불러오기
                    PokemonImage[i].sprite = PokemonSprite[PokemonList.FindIndex(x=>x.Name==PokemonList[i].Name)];
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
                bool isExist = i < ItemList.Count;
                SlotItem[i].SetActive(isExist);
                //슬롯의 각 항목의 설명 불러오기
                SlotItem[i].GetComponentInChildren<Text>().text = isExist ? ItemList[i].Name : "";
                //슬롯의 각 항목의 가격 불러오기
                ItemPrice[i].text = isExist ? ItemList[i].Price.ToString() : "";

                if (isExist)
                {
                    //슬롯의 각 항목에 해당하는 이미지 불러오기
                    ItemImage[i].sprite = ItemSprite[ItemList.FindIndex(x => x.Name == ItemList[i].Name)];
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
                bool isExist = i < BackList.Count;
                SlotBack[i].SetActive(isExist);
                //슬롯의 각 항목의 설명 불러오기
                SlotBack[i].GetComponentInChildren<Text>().text = isExist ? BackList[i].Name : "";
                //슬롯의 각 항목의 가격 불러오기
                BackLevel[i].text = isExist ? BackList[i].Level.ToString() : "";

                if (BackList[i].isActive)
                    BackBtnText[i].text = "사용 중";
                else
                    BackBtnText[i].text = "사용";

                if (isExist)
                {
                    //슬롯의 각 항목에 해당하는 이미지 불러오기
                    BackImage[i].sprite = BackSprite[BackList.FindIndex(x => x.Name == BackList[i].Name)];
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
                bool isExist = i < SoundList.Count;
                SlotSound[i].SetActive(isExist);
                //슬롯의 각 항목의 설명 불러오기
                SlotSound[i].GetComponentInChildren<Text>().text = isExist ? SoundList[i].Name : "";
                //슬롯의 각 항목의 가격 불러오기
                SoundLevel[i].text = isExist ? SoundList[i].Level.ToString() : "";

                if (SoundList[i].isActive)
                    SoundBtnText[i].text = "사용 중";
                else
                    SoundBtnText[i].text = "사용";

                if (isExist)
                {
                    //슬롯의 각 항목에 해당하는 이미지 불러오기
                    SoundImage[i].sprite = SoundSprite[SoundList.FindIndex(x => x.Name == SoundList[i].Name)];
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
                if(curType=="Pokemon" && PokemonList[_btnIdx]!=null)
                {
                    PokemonList[_btnIdx].Price += 10;
                    player.stateList[0].money -= PokemonList[_btnIdx].Price;
                }
                else if(curType=="Equip" && ItemList[_btnIdx]!=null)
                {
                    ItemList[_btnIdx].Price += 5;
                    player.stateList[0].money -= ItemList[_btnIdx].Price;
                }
                else if(curType=="Back"&&BackList[_btnIdx]!=null)
                {
                    backChange.BackNum = _btnIdx;

                }
                else if(curType=="Sound"&&SoundList[_btnIdx]!=null)
                {
                    bgm.Play(_btnIdx);
                }
                break;
            case 1:
                if (curType == "Pokemon" && PokemonList[_btnIdx] != null)
                {
                    PokemonList[_btnIdx].Price += 20;
                    player.stateList[0].money -= PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && ItemList[_btnIdx] != null)
                {
                    ItemList[_btnIdx].Price += 15;
                    player.stateList[0].money -= ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && BackList[_btnIdx] != null)
                {
                    backChange.BackNum = _btnIdx;
                }
                else if (curType == "Sound" && SoundList[_btnIdx] != null)
                {
                    bgm.Play(_btnIdx);
                }
                break;
            case 2:
                if(curType=="Pokemon" && PokemonList[_btnIdx]!=null)
                {
                    PokemonList[_btnIdx].Price += 30;
                    player.stateList[0].money -= PokemonList[_btnIdx].Price;
                }
                else if(curType=="Equip" && ItemList[_btnIdx]!=null)
                {
                    ItemList[_btnIdx].Price += 25;
                    player.stateList[0].money -= ItemList[_btnIdx].Price;
                }
                else if(curType=="Back" && BackList[_btnIdx]!=null)
                {
                    backChange.BackNum = _btnIdx;
                }
                else if(curType=="Sound"&& SoundList[_btnIdx]!=null)
                {
                    bgm.Play(_btnIdx);
                }   
                break;
            case 3:
                if(curType=="Pokemon" && PokemonList[_btnIdx]!=null)
                {
                    PokemonList[_btnIdx].Price += 40;
                    player.stateList[0].money -= PokemonList[_btnIdx].Price;
                }
                else if(curType=="Equip" && ItemList[_btnIdx]!=null)
                {
                    ItemList[_btnIdx].Price += 35;
                    player.stateList[0].money -= ItemList[_btnIdx].Price;
                }
                else if(curType=="Back" && BackList[_btnIdx]!=null)
                {
                    backChange.BackNum = _btnIdx;
                }
                else if(curType=="Sound"&& SoundList[_btnIdx]!=null)
                {
                    bgm.Play(_btnIdx);
                }
                break;
            case 4:
                if (curType == "Pokemon" && PokemonList[_btnIdx] != null)
                {
                    PokemonList[_btnIdx].Price += 40;
                    player.stateList[0].money -= PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && ItemList[_btnIdx] != null)
                {
                    ItemList[_btnIdx].Price += 35;
                    player.stateList[0].money -= ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && BackList[_btnIdx] != null)
                {
                    backChange.BackNum = _btnIdx;
                }
                else if (curType == "Sound" && SoundList[_btnIdx] != null)
                {
                    bgm.Play(_btnIdx);
                }
                break;
            case 5:
                if (curType == "Pokemon" && PokemonList[_btnIdx] != null)
                {
                    PokemonList[_btnIdx].Price += 40;
                    player.stateList[0].money -= PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && ItemList[_btnIdx] != null)
                {
                    ItemList[_btnIdx].Price += 35;
                    player.stateList[0].money -= ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && BackList[_btnIdx] != null)
                {
                    backChange.BackNum = _btnIdx;
                }
                else if (curType == "Sound" && SoundList[_btnIdx] != null)
                {
                    bgm.Play(_btnIdx);
                }
                break;
            case 6:
                if (curType == "Pokemon" && PokemonList[_btnIdx] != null)
                {
                    PokemonList[_btnIdx].Price += 40;
                    player.stateList[0].money -= PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && ItemList[_btnIdx] != null)
                {
                    ItemList[_btnIdx].Price += 35;
                    player.stateList[0].money -= ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && BackList[_btnIdx] != null)
                {
                    backChange.BackNum = _btnIdx;
                }
                else if (curType == "Sound" && SoundList[_btnIdx] != null)
                {
                    bgm.Play(_btnIdx);
                }
                break;
            case 7:
                if (curType == "Pokemon" && PokemonList[_btnIdx] != null)
                {
                    PokemonList[_btnIdx].Price += 40;
                    player.stateList[0].money -= PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && ItemList[_btnIdx] != null)
                {
                    ItemList[_btnIdx].Price += 35;
                    player.stateList[0].money -= ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && BackList[_btnIdx] != null)
                {
                    backChange.BackNum = _btnIdx;
                }
                else if (curType == "Sound" && SoundList[_btnIdx] != null)
                {
                    bgm.Play(_btnIdx);
                }
                break;
            case 8:
                if (curType == "Pokemon" && PokemonList[_btnIdx] != null)
                {
                    PokemonList[_btnIdx].Price += 40;
                    player.stateList[0].money -= PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && ItemList[_btnIdx] != null)
                {
                    ItemList[_btnIdx].Price += 35;
                    player.stateList[0].money -= ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && BackList[_btnIdx] != null)
                {
                    backChange.BackNum = _btnIdx;
                }
                else if (curType == "Sound" && SoundList[_btnIdx] != null)
                {
                    bgm.Play(_btnIdx);
                }
                break;
            case 9:
                if (curType == "Pokemon" && PokemonList[_btnIdx] != null)
                {
                    PokemonList[_btnIdx].Price += 40;
                    player.stateList[0].money -= PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && ItemList[_btnIdx] != null)
                {
                    ItemList[_btnIdx].Price += 35;
                    player.stateList[0].money -= ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && BackList[_btnIdx] != null)
                {
                    backChange.BackNum = _btnIdx;
                }
                else if (curType == "Sound" && SoundList[_btnIdx] != null)
                {
                    bgm.Play(_btnIdx);
                }
                break;
            case 10:
                if (curType == "Pokemon" && PokemonList[_btnIdx] != null)
                {
                    PokemonList[_btnIdx].Price += 40;
                    player.stateList[0].money -= PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && ItemList[_btnIdx] != null)
                {
                    ItemList[_btnIdx].Price += 35;
                    player.stateList[0].money -= ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && BackList[_btnIdx] != null)
                {
                    backChange.BackNum = _btnIdx;
                }
                else if (curType == "Sound" && SoundList[_btnIdx] != null)
                {
                    bgm.Play(_btnIdx);
                }
                break;
            case 11:
                if (curType == "Pokemon" && PokemonList[_btnIdx] != null)
                {
                    PokemonList[_btnIdx].Price += 40;
                    player.stateList[0].money -= PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && ItemList[_btnIdx] != null)
                {
                    ItemList[_btnIdx].Price += 35;
                    player.stateList[0].money -= ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && BackList[_btnIdx] != null)
                {
                    backChange.BackNum = _btnIdx;
                }
                else if (curType == "Sound" && SoundList[_btnIdx] != null)
                {
                    bgm.Play(_btnIdx);
                }
                break;
            case 12:
                if (curType == "Pokemon" && PokemonList[_btnIdx] != null)
                {
                    PokemonList[_btnIdx].Price += 40;
                    player.stateList[0].money -= PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && ItemList[_btnIdx] != null)
                {
                    ItemList[_btnIdx].Price += 35;
                    player.stateList[0].money -= ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && BackList[_btnIdx] != null)
                {
                    backChange.BackNum = _btnIdx;
                }
                else if (curType == "Sound" && SoundList[_btnIdx] != null)
                {
                    bgm.Play(_btnIdx);
                }
                break;
            case 13:
                if (curType == "Pokemon" && PokemonList[_btnIdx] != null)
                {
                    PokemonList[_btnIdx].Price += 40;
                    player.stateList[0].money -= PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && ItemList[_btnIdx] != null)
                {
                    ItemList[_btnIdx].Price += 35;
                    player.stateList[0].money -= ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && BackList[_btnIdx] != null)
                {
                    backChange.BackNum = _btnIdx;
                }
                else if (curType == "Sound" && SoundList[_btnIdx] != null)
                {
                    bgm.Play(_btnIdx);
                }
                break;
            case 14:
                if (curType == "Pokemon" && PokemonList[_btnIdx] != null)
                {
                    PokemonList[_btnIdx].Price += 40;
                    player.stateList[0].money -= PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && ItemList[_btnIdx] != null)
                {
                    ItemList[_btnIdx].Price += 35;
                    player.stateList[0].money -= ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && BackList[_btnIdx] != null)
                {
                    backChange.BackNum = _btnIdx;
                }
                else if (curType == "Sound" && SoundList[_btnIdx] != null)
                {
                    bgm.Play(_btnIdx);
                }
                break;
            case 15:
                if (curType == "Pokemon" && PokemonList[_btnIdx] != null)
                {
                    PokemonList[_btnIdx].Price += 40;
                    player.stateList[0].money -= PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && ItemList[_btnIdx] != null)
                {
                    ItemList[_btnIdx].Price += 35;
                    player.stateList[0].money -= ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && BackList[_btnIdx] != null)
                {
                    backChange.BackNum = _btnIdx;
                }
                else if (curType == "Sound" && SoundList[_btnIdx] != null)
                {
                    bgm.Play(_btnIdx);
                }
                break;
            case 16:
                if (curType == "Pokemon" && PokemonList[_btnIdx] != null)
                {
                    PokemonList[_btnIdx].Price += 40;
                    player.stateList[0].money -= PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && ItemList[_btnIdx] != null)
                {
                    ItemList[_btnIdx].Price += 35;
                    player.stateList[0].money -= ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && BackList[_btnIdx] != null)
                {
                    backChange.BackNum = _btnIdx;
                }
                else if (curType == "Sound" && SoundList[_btnIdx] != null)
                {
                    bgm.Play(_btnIdx);
                }
                break;
            case 17:
                if (curType == "Pokemon" && PokemonList[_btnIdx] != null)
                {
                    PokemonList[_btnIdx].Price += 40;
                    player.stateList[0].money -= PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && ItemList[_btnIdx] != null)
                {
                    ItemList[_btnIdx].Price += 35;
                    player.stateList[0].money -= ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && BackList[_btnIdx] != null)
                {
                    backChange.BackNum = _btnIdx;
                }
                else if (curType == "Sound" && SoundList[_btnIdx] != null)
                {
                    bgm.Play(_btnIdx);
                }
                break;
            case 18:
                if (curType == "Pokemon" && PokemonList[_btnIdx] != null)
                {
                    PokemonList[_btnIdx].Price += 40;
                    player.stateList[0].money -= PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && ItemList[_btnIdx] != null)
                {
                    ItemList[_btnIdx].Price += 35;
                    player.stateList[0].money -= ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && BackList[_btnIdx] != null)
                {
                    backChange.BackNum = _btnIdx;
                }
                else if (curType == "Sound" && SoundList[_btnIdx] != null)
                {
                    bgm.Play(_btnIdx);
                }
                break;
            case 19:
                if (curType == "Pokemon" && PokemonList[_btnIdx] != null)
                {
                    PokemonList[_btnIdx].Price += 40;
                    player.stateList[0].money -= PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && ItemList[_btnIdx] != null)
                {
                    ItemList[_btnIdx].Price += 35;
                    player.stateList[0].money -= ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && BackList[_btnIdx] != null)
                {
                    backChange.BackNum = _btnIdx;
                }
                else if (curType == "Sound" && SoundList[_btnIdx] != null)
                {
                    bgm.Play(_btnIdx);
                }
                break;
            case 20:
                if (curType == "Pokemon" && PokemonList[_btnIdx] != null)
                {
                    PokemonList[_btnIdx].Price += 40;
                    player.stateList[0].money -= PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && ItemList[_btnIdx] != null)
                {
                    ItemList[_btnIdx].Price += 35;
                    player.stateList[0].money -= ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && BackList[_btnIdx] != null)
                {
                    backChange.BackNum = _btnIdx;
                }
                else if (curType == "Sound" && SoundList[_btnIdx] != null)
                {
                    bgm.Play(_btnIdx);
                }
                break;
            case 21:
                if (curType == "Pokemon" && PokemonList[_btnIdx] != null)
                {
                    PokemonList[_btnIdx].Price += 40;
                    player.stateList[0].money -= PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && ItemList[_btnIdx] != null)
                {
                    ItemList[_btnIdx].Price += 35;
                    player.stateList[0].money -= ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && BackList[_btnIdx] != null)
                {
                    backChange.BackNum = _btnIdx;
                }
                else if (curType == "Sound" && SoundList[_btnIdx] != null)
                {
                    bgm.Play(_btnIdx);
                }
                break;
            case 22:
                if (curType == "Pokemon" && PokemonList[_btnIdx] != null)
                {
                    PokemonList[_btnIdx].Price += 40;
                    player.stateList[0].money -= PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && ItemList[_btnIdx] != null)
                {
                    ItemList[_btnIdx].Price += 35;
                    player.stateList[0].money -= ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && BackList[_btnIdx] != null)
                {
                    backChange.BackNum = _btnIdx;
                }
                else if (curType == "Sound" && SoundList[_btnIdx] != null)
                {
                    bgm.Play(_btnIdx);
                }
                break;
            case 23:
                if (curType == "Pokemon" && PokemonList[_btnIdx] != null)
                {
                    PokemonList[_btnIdx].Price += 40;
                    player.stateList[0].money -= PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && ItemList[_btnIdx] != null)
                {
                    ItemList[_btnIdx].Price += 35;
                    player.stateList[0].money -= ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && BackList[_btnIdx] != null)
                {
                    backChange.BackNum = _btnIdx;
                }
                else if (curType == "Sound" && SoundList[_btnIdx] != null)
                {
                    bgm.Play(_btnIdx);
                }
                break;
            case 24:
                if (curType == "Pokemon" && PokemonList[_btnIdx] != null)
                {
                    PokemonList[_btnIdx].Price += 40;
                    player.stateList[0].money -= PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && ItemList[_btnIdx] != null)
                {
                    ItemList[_btnIdx].Price += 35;
                    player.stateList[0].money -= ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && BackList[_btnIdx] != null)
                {
                    backChange.BackNum = _btnIdx;
                }
                else if (curType == "Sound" && SoundList[_btnIdx] != null)
                {
                    bgm.Play(_btnIdx);
                }
                break;
            case 25:
                if (curType == "Pokemon" && PokemonList[_btnIdx] != null)
                {
                    PokemonList[_btnIdx].Price += 40;
                    player.stateList[0].money -= PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && ItemList[_btnIdx] != null)
                {
                    ItemList[_btnIdx].Price += 35;
                    player.stateList[0].money -= ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && BackList[_btnIdx] != null)
                {
                    backChange.BackNum = _btnIdx;
                }
                else if (curType == "Sound" && SoundList[_btnIdx] != null)
                {
                    bgm.Play(_btnIdx);
                }
                break;
            case 26:
                if (curType == "Pokemon" && PokemonList[_btnIdx] != null)
                {
                    PokemonList[_btnIdx].Price += 40;
                    player.stateList[0].money -= PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && ItemList[_btnIdx] != null)
                {
                    ItemList[_btnIdx].Price += 35;
                    player.stateList[0].money -= ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && BackList[_btnIdx] != null)
                {
                    backChange.BackNum = _btnIdx;
                }
                else if (curType == "Sound" && SoundList[_btnIdx] != null)
                {
                    bgm.Play(_btnIdx);
                }
                break;
            case 27:
                if (curType == "Pokemon" && PokemonList[_btnIdx] != null)
                {
                    PokemonList[_btnIdx].Price += 40;
                    player.stateList[0].money -= PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && ItemList[_btnIdx] != null)
                {
                    ItemList[_btnIdx].Price += 35;
                    player.stateList[0].money -= ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && BackList[_btnIdx] != null)
                {
                    backChange.BackNum = _btnIdx;
                }
                else if (curType == "Sound" && SoundList[_btnIdx] != null)
                {
                    bgm.Play(_btnIdx);
                }
                break;
            case 28:
                if (curType == "Pokemon" && PokemonList[_btnIdx] != null)
                {
                    PokemonList[_btnIdx].Price += 40;
                    player.stateList[0].money -= PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && ItemList[_btnIdx] != null)
                {
                    ItemList[_btnIdx].Price += 35;
                    player.stateList[0].money -= ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && BackList[_btnIdx] != null)
                {
                    backChange.BackNum = _btnIdx;
                }
                else if (curType == "Sound" && SoundList[_btnIdx] != null)
                {
                    bgm.Play(_btnIdx);
                }
                break;
            case 29:
                if (curType == "Pokemon" && PokemonList[_btnIdx] != null)
                {
                    PokemonList[_btnIdx].Price += 40;
                    player.stateList[0].money -= PokemonList[_btnIdx].Price;
                }
                else if (curType == "Equip" && ItemList[_btnIdx] != null)
                {
                    ItemList[_btnIdx].Price += 35;
                    player.stateList[0].money -= ItemList[_btnIdx].Price;
                }
                else if (curType == "Back" && BackList[_btnIdx] != null)
                {
                    backChange.BackNum = _btnIdx;
                }
                else if (curType == "Sound" && SoundList[_btnIdx] != null)
                {
                    bgm.Play(_btnIdx);
                }
                break;

        }
        for (int i = 0; i < SlotBack.Length; i++)
        {
            BackList[i].isActive = false;
            if (i == _btnIdx)
                BackList[i].isActive = true;
        }
        for (int i = 0; i < SlotSound.Length; i++)
        {
            SoundList[i].isActive = false;
            if (i == _btnIdx)
                SoundList[i].isActive = true;
        }
        Save();
    }

    ////Data Save&Load
    void Save()
    {
        string jdata_0 = JsonConvert.SerializeObject(PokemonList); //직렬화
        string jdata_1 = JsonConvert.SerializeObject(ItemList);
        string jdata_2 = JsonConvert.SerializeObject(BackList);
        string jdata_3 = JsonConvert.SerializeObject(SoundList);

        File.WriteAllText(Application.persistentDataPath + "/Resources/Pokemons.json", jdata_0);
        File.WriteAllText(Application.persistentDataPath + "/Resources/Items.json", jdata_1);
        File.WriteAllText(Application.persistentDataPath + "/Resources/Backs.json", jdata_2);
        File.WriteAllText(Application.persistentDataPath + "/Resources/Sounds.json", jdata_3);

        TabClick(curType);
    }

    void Load()
    {
        string jdata_0 = File.ReadAllText(Application.persistentDataPath + "/Resources/Pokemons.json"); //역직렬화
        string jdata_1 = File.ReadAllText(Application.persistentDataPath + "/Resources/Items.json");
        string jdata_2 = File.ReadAllText(Application.persistentDataPath + "/Resources/Backs.json");
        string jdata_3 = File.ReadAllText(Application.persistentDataPath + "/Resources/Sounds.json");

        PokemonList = JsonConvert.DeserializeObject<List<Pokemon>>(jdata_0);
        ItemList = JsonConvert.DeserializeObject<List<Item>>(jdata_1);
        BackList = JsonConvert.DeserializeObject<List<Back>>(jdata_2);
        SoundList = JsonConvert.DeserializeObject<List<Sound>>(jdata_3);

        TabClick(curType);
    }

    public void _Reset()
    {
        PokemonList = new List<Pokemon>();
        ItemList = new List<Item>();
        BackList = new List<Back>();
        SoundList = new List<Sound>();

        //포켓몬 DB 추가
        PokemonList.Add(new Pokemon("Pokemon", "이상해씨", 100));
        PokemonList.Add(new Pokemon("Pokemon", "파이리", 200));
        PokemonList.Add(new Pokemon("Pokemon", "꼬부기", 300));
        PokemonList.Add(new Pokemon("Pokemon", "캐터피", 400));
        PokemonList.Add(new Pokemon("Pokemon", "뿔충이", 500));
        PokemonList.Add(new Pokemon("Pokemon", "구구", 600));
        PokemonList.Add(new Pokemon("Pokemon", "꼬렛", 700));
        PokemonList.Add(new Pokemon("Pokemon", "깨비참", 800));
        PokemonList.Add(new Pokemon("Pokemon", "아보", 900));
        PokemonList.Add(new Pokemon("Pokemon", "피카츄", 1000));
        PokemonList.Add(new Pokemon("Pokemon", "모래두지", 1100));
        PokemonList.Add(new Pokemon("Pokemon", "삐삐", 1200));
        PokemonList.Add(new Pokemon("Pokemon", "식스테일", 1300));
        PokemonList.Add(new Pokemon("Pokemon", "푸린", 1400));
        PokemonList.Add(new Pokemon("Pokemon", "주뱃", 1500));
        PokemonList.Add(new Pokemon("Pokemon", "뚜벅쵸", 1600));
        PokemonList.Add(new Pokemon("Pokemon", "디그다", 1700));
        PokemonList.Add(new Pokemon("Pokemon", "나옹", 1800));
        //장비 DB 추가
        ItemList.Add(new Item("Equip", "검", 50));
        ItemList.Add(new Item("Equip", "카알대검", 100));
        ItemList.Add(new Item("Equip", "초급전사의검", 150));
        ItemList.Add(new Item("Equip", "사브르", 200));
        ItemList.Add(new Item("Equip", "바이킹소드", 250));
        ItemList.Add(new Item("Equip", "글라디우스", 300));
        ItemList.Add(new Item("Equip", "커틀러스", 350));
        ItemList.Add(new Item("Equip", "카타나", 400));
        ItemList.Add(new Item("Equip", "트라우스", 450));
        ItemList.Add(new Item("Equip", "베릴 브링어", 500));
        ItemList.Add(new Item("Equip", "칠성검", 550));
        ItemList.Add(new Item("Equip", "쥬얼 쿠아다라", 600));
        ItemList.Add(new Item("Equip", "글로리 소드", 650));
        ItemList.Add(new Item("Equip", "네오코라", 700));
        ItemList.Add(new Item("Equip", "래티넘 브링어", 750));
        ItemList.Add(new Item("Equip", "스텔라 글라디우스", 800));
        ItemList.Add(new Item("Equip", "파이롭 소드", 850));
        ItemList.Add(new Item("Equip", "아울렛 글라디우스", 900));
        ItemList.Add(new Item("Equip", "레전드 브링어", 950));
        ItemList.Add(new Item("Equip", "프라우테", 1000));
        ItemList.Add(new Item("Equip", "스파타", 1050));
        ItemList.Add(new Item("Equip", "세인트 소드", 1100));
        ItemList.Add(new Item("Equip", "레전드 스파타", 1150));
        ItemList.Add(new Item("Equip", "얼티밋 글라디우스", 1200));
        ItemList.Add(new Item("Equip", "템페스트 글라디우스", 1250));
        ItemList.Add(new Item("Equip", "블랙 소드", 1300));
        ItemList.Add(new Item("Equip", "라피스라소드", 1350));
        ItemList.Add(new Item("Equip", "레볼루션 소드", 1400));
        ItemList.Add(new Item("Equip", "드래곤 카라벨라", 1450));
        ItemList.Add(new Item("Equip", "네크로피어", 1500));
        //배경 DB 추가
        BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "1 증가", 1, true));
        BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "2 증가", 3, false));
        BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "3 증가", 5, false));
        BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "4 증가", 7, false));
        BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "5 증가", 9, false));
        BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "6 증가", 11, false));
        BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "7 증가", 13, false));
        BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "8 증가", 15, false));
        BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "9 증가", 17, false));
        BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "10 증가", 19, false));
        BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "11 증가", 21, false));
        BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "12 증가", 23, false));
        BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "13 증가", 25, false));
        BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "14 증가", 27, false));
        BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "15 증가", 29, false));
        BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "16 증가", 31, false));
        BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "17 증가", 33, false));
        BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "18 증가", 35, false));
        BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "19 증가", 37, false));
        BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "20 증가", 39, false));
        BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "21 증가", 41, false));
        BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "22 증가", 43, false));
        BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "23 증가", 45, false));
        BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "24 증가", 47, false));
        BackList.Add(new Back("Back", "탭당 클릭 횟수\n" + "25 증가", 49, false));
        //사운드 DB 추가
        SoundList.Add(new Sound("Sound", "Beneath the Moonlight\n" + "-Aaron Kenny-", 1, true));
        SoundList.Add(new Sound("Sound", "Bittersweet Waltz\n" + "-Sir Cubworth-", 4, false));
        SoundList.Add(new Sound("Sound", "Brass Chorale and Motet\n" + "-Sir Cubworth-", 6, false));
        SoundList.Add(new Sound("Sound", "Butterflies In Love\n" + "-Sir Cubworth-", 8, false));
        SoundList.Add(new Sound("Sound", "Ceremonial Prelude\n" + "-Sir Cubworth-", 10, false));
        SoundList.Add(new Sound("Sound", "Christmas Village\n" + "-Aaron Kenny-", 12, false));
        SoundList.Add(new Sound("Sound", "Dance for Wind Trio\n" + "-Sir Cubworth-", 14, false));
        SoundList.Add(new Sound("Sound", "English Country Garden\n" + "-Aaron Kenny-", 16, false));
        SoundList.Add(new Sound("Sound", "First Sleep\n" + "-Sir Cubworth-", 18, false));
        SoundList.Add(new Sound("Sound", "Heavenly\n" + "-Aakash Gandhi-", 20, false));
        SoundList.Add(new Sound("Sound", "Hopeful Freedom\n" + "-Asher Fulero-", 22, false));
        SoundList.Add(new Sound("Sound", "Hovering Thoughts\n" + "-Spence-", 24, false));
        SoundList.Add(new Sound("Sound", "Invitation\n" + "to the Castle Ball\n" + "-Doug Maxwell-", 26, false));
        SoundList.Add(new Sound("Sound", "Kiss the Sky\n" + "-Aakash Gandhi-", 28, false));
        SoundList.Add(new Sound("Sound", "Lifting Dreams\n" + "-Aakash Gandhi-", 30, false));
        SoundList.Add(new Sound("Sound", "Lullaby\n" + "-Cooper Cannell-", 32, false));
        SoundList.Add(new Sound("Sound", "Lullaby\n" + "-JVNA-", 34, false));
        SoundList.Add(new Sound("Sound", "Minyo San Kyoku\n" + "-Doug Maxwell_ Zac Zinger-", 36, false));
        SoundList.Add(new Sound("Sound", "No.6 In My Dreams\n" + "-Esther Abrami-", 38, false));
        SoundList.Add(new Sound("Sound", "No.9_Esther’s Waltz\n" + "-Esther Abrami-", 40, false));
        SoundList.Add(new Sound("Sound", "November\n" + "-Joey Pecoraro-", 42, false));
        SoundList.Add(new Sound("Sound", "Party Waltz\n" + "-Sir Cubworth-", 44, false));
        SoundList.Add(new Sound("Sound", "Requiem In Cello\n" + "-Hanu Dixit-", 46, false));
        SoundList.Add(new Sound("Sound", "Shattered Paths\n" + "-Aakash Gandhi-", 48, false));
        SoundList.Add(new Sound("Sound", "Star Spangled Banner\n" + "-Cooper Cannell-", 50, false));
        SoundList.Add(new Sound("Sound", "Summer Symphony Ball\n" + "-Sir Cubworth-", 52, false));
        SoundList.Add(new Sound("Sound", "The First Noel\n" + "-Quincas Moreira-", 54, false));
        SoundList.Add(new Sound("Sound", "Theme for a One-\n" + "Handed Piano Concerto\n" + "-Sir Cubworth-", 56, false));
        SoundList.Add(new Sound("Sound", "White River\n" + "-Aakash Gandhi-", 58, false));
        SoundList.Add(new Sound("Sound", "Wistful Harp\n" + "-Andrew Huang-", 60, false));

        Save();
    }
}
