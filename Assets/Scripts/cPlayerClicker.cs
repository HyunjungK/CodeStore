using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cPlayerClicker : MonoBehaviour
{
    private cDBManager data;

    BoxCollider2D boxCollider;

    public GameObject pftext; //플로팅 텍스트
    GameObject tabtext;
    public Animator anim;

    public string tabSound;
    private cAudioManager theAudio;

    private void Awake()
    {
        data = FindObjectOfType<cDBManager>();
        theAudio = FindObjectOfType<cAudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        StartCoroutine("AutoMoney");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //재화 표시
        Text moneyText = GameObject.Find("MoneyText").GetComponentInChildren<Text>();
        moneyText.text = string.Format("{0:n0}", data.stateList[0].money);

        //레벨 표시
        Text levelText = GameObject.Find("LevelText").GetComponentInChildren<Text>();
        levelText.text = data.stateList[0].level.ToString();

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D rayHit = Physics2D.Raycast(mousePos, Vector2.zero);

        if(rayHit.collider!=null && Input.GetMouseButtonDown(0))
        {
            if(rayHit.collider == boxCollider)
            {
                theAudio.Play(tabSound);
                tabtext = Instantiate(pftext);
                tabtext.transform.position = mousePos;
                tabtext.GetComponent<cTabText>().tab = data.stateList[0].plusMoney;
                data.stateList[0].money += data.stateList[0].plusMoney; //플레이어 클릭 시 재화 증가
                data.stateList[0].count += 1;
                if(data.stateList[0].count==100)
                {
                    data.stateList[0].level += 1;
                    data.stateList[0].plusMoney += 50;
                    data.stateList[0].autoMoney += 2;
                    tabtext.GetComponent<cTabText>().tab = data.stateList[0].plusMoney;
                    data.stateList[0].count = 0;
                }
                anim.SetTrigger("TabClick");
                
                data.Save();
            }
        }
    }

    IEnumerator AutoMoney()
    {
        yield return new WaitForSeconds(3f);
        data.stateList[0].money += data.stateList[0].autoMoney;
        StartCoroutine("AutoMoney");
    }  
}
