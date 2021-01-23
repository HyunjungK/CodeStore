using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.UI;
using System;

[System.Serializable]
public class State
{
    public int money, level;
    public State(int _money, int _level)
    {
        money = _money;
        level = _level;
    }
}
public class cPlayerClicker : MonoBehaviour
{
    BoxCollider2D boxCollider;

    public List<State> stateList;
    public int autoMoney; //자동으로 증가하는 재화
    public int plusMoney; //레벨이 증가할 수록 탭 시 더해지는 재화 증가
    public int count; //탭 클릭 수

    public GameObject pftext; //플로팅 텍스트
    GameObject tabtext;
    public Animator anim;

    public string tabSound;
    private cAudioManager theAudio;

    private void Awake()
    {
        theAudio = FindObjectOfType<cAudioManager>();
        if (!File.Exists(Application.persistentDataPath + "/Resources/States.json"))
        {
            stateList.Add(new State(1000, 1));

            playerSave();
        }
        playerLoad();
    }

    // Start is called before the first frame update
    void Start()
    {
        autoMoney = 1;
        count = 0;
        plusMoney = 50;
        boxCollider = GetComponent<BoxCollider2D>();
        StartCoroutine("AutoMoney");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //재화 표시
        Text moneyText = GameObject.Find("MoneyText").GetComponentInChildren<Text>();
        moneyText.text = string.Format("{0:n0}", stateList[0].money);

        //레벨 표시
        Text levelText = GameObject.Find("LevelText").GetComponentInChildren<Text>();
        levelText.text = stateList[0].level.ToString();

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D rayHit = Physics2D.Raycast(mousePos, Vector2.zero);

        if(rayHit.collider!=null && Input.GetMouseButtonDown(0))
        {
            if(rayHit.collider == boxCollider)
            {
                theAudio.Play(tabSound);
                tabtext = Instantiate(pftext);
                tabtext.transform.position = mousePos;
                tabtext.GetComponent<cTabText>().tab = plusMoney;
                stateList[0].money += plusMoney; //플레이어 클릭 시 재화 증가
                count += 1;
                anim.SetTrigger("TabClick");
                switch(count)
                {
                    case 50:
                        stateList[0].level += 1;
                        plusMoney = 100;
                        autoMoney = 3;
                        tabtext.GetComponent<cTabText>().tab = plusMoney;
                        break;
                    case 100:
                        stateList[0].level += 1;
                        plusMoney = 200;
                        autoMoney = 5;
                        tabtext.GetComponent<cTabText>().tab = plusMoney;
                        break;
                    case 150:
                        stateList[0].level += 1;
                        plusMoney = 300;
                        autoMoney = 7;
                        tabtext.GetComponent<cTabText>().tab = plusMoney;
                        break;
                }
                
                playerSave();
            }
        }
    }

    IEnumerator AutoMoney()
    {
        yield return new WaitForSeconds(3f);
        stateList[0].money += autoMoney;
        StartCoroutine("AutoMoney");
    }

    public void playerSave()
    {
        string jdata = JsonConvert.SerializeObject(stateList);

        File.WriteAllText(Application.persistentDataPath + "/Resources/States.json", jdata);
    }

    public void playerLoad()
    {
        string jdata = File.ReadAllText(Application.persistentDataPath + "/Resources/States.json");

        stateList = JsonConvert.DeserializeObject<List<State>>(jdata);
    }

    public void dataReset()
    {
        stateList = new List<State>();
        stateList.Add(new State(1000, 1));
        playerSave();
    }
  
}
