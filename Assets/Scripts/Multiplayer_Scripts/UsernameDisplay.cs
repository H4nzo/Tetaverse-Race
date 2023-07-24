using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class UsernameDisplay : MonoBehaviour
{
    [SerializeField] PhotonView view;
    [SerializeField] TextMeshProUGUI usernameText;

    void Start()
    {
        view = GetComponent<PhotonView>();
        usernameText.text = view.Owner.NickName;
    }
}
