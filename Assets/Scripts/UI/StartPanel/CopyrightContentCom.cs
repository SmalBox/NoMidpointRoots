using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopyrightContentCom : MonoBehaviour
{
    public List<Button> personBtnList;
    public List<string> personLinkList;
    
    private void OnEnable()
    {
        for (int i = 0; i < personBtnList.Count; i++)
        {
            int index = i;
            personBtnList[i].onClick.AddListener(() =>
            {
                OnPersonBtn(index);
            });
        }
    }

    private void OnDisable()
    {
        foreach (Button button in personBtnList)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    private void OnPersonBtn(int index)
    {
        if (!string.IsNullOrEmpty(personLinkList[index]))
            Application.OpenURL(personLinkList[index]);
    }
}
