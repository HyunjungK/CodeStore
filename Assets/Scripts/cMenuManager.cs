using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class cMenuManager : MonoBehaviour
{
    public Button playBtn;
    public Button exitBtn;
    public EventSystem eventSystem;

    //버튼 애니메이션 효과
    public void InitBtnState()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
