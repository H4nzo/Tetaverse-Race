using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{
    public void RedeemToken()
    {

    }

    public void Home()
    {
        SceneManager.LoadScene("LoginScene");
        Time.timeScale = 1f;
    }


}
