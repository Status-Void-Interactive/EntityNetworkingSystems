﻿using EntityNetworkingSystems;
using System.Collections;
using System.Collections.Generic;
using Steamworks;
using Steamworks.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExampleClient : MonoBehaviour
{
    public NetClient netClient;
    public List<NetworkObject> owned = new List<NetworkObject>();

    private ulong targetSteamID = 0;
    
    private void Start()
    {
        SteamClient.Init(480, false);
        targetSteamID = SteamClient.SteamId;
    }

    public void ConnectToServer()
    {
        NetTools.onJoinServer.AddListener(InitializePlayer);
        NetTools.onLeaveServer.AddListener(delegate { SceneManager.LoadSceneAsync("TestScene", LoadSceneMode.Single); });

        netClient.Initialize();
        netClient.ConnectToServer(targetSteamID);
        
        //netClient.Initialize();
        //netClient = SteamNetworkingSockets.ConnectRelay<NetClient>(targetSteamID,432);
        //netClient.Initialize();
        
        //netClient.PostConnectStart();
    }
    
    public void ConnectToSingleplayer()
    {
        netClient.Initialize();
        
        NetTools.onJoinServer.AddListener(InitializePlayer);
        NetTools.onLeaveServer.AddListener(delegate { SceneManager.LoadSceneAsync("TestScene", LoadSceneMode.Single); });

        netClient.ConnectToSingleplayer();
        
        netClient.PostConnectStart();
    }

    public void Disconnect()
    {
        netClient.DisconnectFromServer();
        NetServer.serverInstance.StopServer();
    }

    //void Update()
    //{
    //    Debug.Log(NetTools.clientID);
    //}

    void InitializePlayer()
    {
        NetTools.NetInstantiate(0, 1, new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(-4.0f, 4.0f)),Quaternion.identity);
    }

    void Update()
    {
        SteamClient.RunCallbacks();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < 50; i++)
            {
                GameObject g = NetTools.NetInstantiate(0, 0, new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(-4.0f, 4.0f), 0), Quaternion.identity);
                owned.Add(g.GetComponent<NetworkObject>());
            }
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            int randIndex = Random.Range(0, NetworkObject.allNetObjs.Count);
            //print(randIndex);
            NetTools.NetDestroy(NetworkObject.allNetObjs[randIndex]);
            Debug.Log(NetworkData.usedNetworkObjectInstances[randIndex]);
        }



        if (Input.GetKeyDown(KeyCode.F11))
        {
            Screen.fullScreen = !Screen.fullScreen;
        }
    }

    public void UpdateTargetSteamID64(string steamID64)
    {
        targetSteamID = ulong.Parse(steamID64);
    }
    

    void OnDestroy()
    {
        netClient.DisconnectFromServer();
        
        SteamClient.Shutdown();
    }

}
