using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class UsernameDisplay : MonoBehaviour
{
    [SerializeField] PhotonView view;
    [SerializeField] Text usernameText;

    void Start()
    {
        usernameText.text = view.Owner.NickName;
    }
}
