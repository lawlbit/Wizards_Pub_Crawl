﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DefenderController : NetworkBehaviour
{
    public IUnityService unityService;
    public GameObject DefenderCamera;
    public GameObject monster;
    public GameObject trap;

    // Use this for initialization
    void Start()
    {
        if (!isServer) return;
        SetUpDefenderCam();

        if (unityService == null)
        {
            unityService = new UnityService();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Dont think this is needed right now.
    }

    private void SetUpDefenderCam()
    {
        GameObject.FindGameObjectWithTag("MainCamera").SetActive(false);
        Instantiate(DefenderCamera);
    }

    public void SpawnMonster(Vector3 location, Quaternion rotation) {
        //Can do check here before sending it off to server.
        location.y = location.y + 0.5f;    //change to use monster height when that data is stored
                                           //monsterSpawn.y = monsterSpawn.y + monster.transform.y;                 
        CmdSpawnMonster(location, rotation);

    }
    public void SpawnTrap(Vector3 location, Quaternion rotation)
    {
        location.y = location.y + 0.05f;
        CmdSpawnTrap(location, rotation);
    }

    // Commands for communicating to the server.
    [Command]
    private void CmdSpawnMonster(Vector3 location, Quaternion rotation) {
        GameObject temp;
        temp = unityService.Instantiate(monster, location, rotation);
        NetworkServer.Spawn(temp);
    }
    [Command]
    private void CmdSpawnTrap(Vector3 location, Quaternion rotation) {

        GameObject tempTrap;
        //rotation.SetAxisAngle(new Vector3(1, 0, 0), 90);
        tempTrap = unityService.Instantiate(trap, location, rotation);
        NetworkServer.Spawn(tempTrap);
    }
}