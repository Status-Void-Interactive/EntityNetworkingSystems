﻿using EntityNetworkingSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleMoving : MonoBehaviour
{

    NetworkObject net;

    void Start()
    {
        net = GetComponent<NetworkObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!net.initialized)
        {
            return;
        }
        //if(net.IsOwner() && Input.GetKey(KeyCode.J))
        //{
        //    net.UpdateField("ENS_Position",)
        //}
    }
}
