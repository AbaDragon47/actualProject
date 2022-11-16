using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUi : MonoBehaviour
{
    [SerializeField] private Button svrButton;
    [SerializeField] private Button cltButton;
    [SerializeField] private Button hstButton;

    public void Awake()
    {
        svrButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
        });
        cltButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });
        hstButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        });
    }
}
