using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSettings : MonoBehaviour
{
    public GameObject FacebookButton;
    public GameObject LoginButton;
    public GameObject emailPanel;

    public void CloseUI()
    {
        emailPanel.SetActive(false);
        FacebookButton.SetActive(true);
        LoginButton.SetActive(true);
    }

    public void LoginUI()
    {
        emailPanel.SetActive(true);
        FacebookButton.SetActive(false);
        LoginButton.SetActive(false);
    }
}
