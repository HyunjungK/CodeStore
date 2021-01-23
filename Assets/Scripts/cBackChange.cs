using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cBackChange : MonoBehaviour
{
    Sprite BackImage;
    public int BackNum; //배경 이미지 넘버
    // Start is called before the first frame update
    void Start()
    {
        BackNum = 0;      
    }

    // Update is called once per frame
    void Update()
    {
        BackImage = Resources.Load("BG/Back_" + BackNum.ToString(), typeof(Sprite)) as Sprite;
        this.GetComponent<SpriteRenderer>().sprite = BackImage;
    }
}
