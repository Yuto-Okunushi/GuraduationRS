using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControlls : MonoBehaviour
{
    public GameObject ActionEndButton;
    public GameObject ChoosePanel;
    public GameObject CheckPanel;


    public void PushButton1()
    {
        CheckPanel.SetActive(true);
    }

    public void PushButton2()
    {
        CheckPanel.SetActive(true);
    }

    public void PushButton3()
    {
        CheckPanel.SetActive(true);
    }

    public void PushButton4()
    {
        CheckPanel.SetActive(true);
    }

    public void PushYes()
    {
        ChoosePanel.SetActive(false);
        CheckPanel.SetActive(false);
    }

    public void PushNo()
    {
        CheckPanel.SetActive(false);
    }

    public void PushActionEnd()
    {
        ChoosePanel.SetActive(true);
    }

    public void PushX()
    {
        ChoosePanel.SetActive(false);
    }

}
