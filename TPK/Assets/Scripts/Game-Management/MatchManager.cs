﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Contains information about the game's current status, such as number of players in the match.
/// Should only run on the server.
/// </summary>
public class MatchManager : NetworkBehaviour
{
    public readonly int maxPlayers = 2;         // currently only accepting 2 players maximum
    [SyncVar] private int currentNumOfPlayers;  // the current number of players in the match
    public List<NetworkConnection> connections; // list of all connections in the match

    /// <summary>
    /// Initialize variables.
    /// </summary>
    void Start()
    {
        if (!isServer) return;
        DontDestroyOnLoad(transform);
    }

    /// <summary>
    /// If max number of players is not reached, increments the current number of players by 1.
    /// </summary>
    /// <returns>Returns true if a player has been successfully added to the match, else returns false.</returns>
    public bool AddPlayerToMatch(NetworkConnection conn)
    {
        if (!isServer) return false;

        // Add NetworkConnection is connections list
        if (connections == null)
        {
            connections = new List<NetworkConnection>();
        }
        connections.Add(conn);

        // Increment currentNumOfPlayers counter
        if (currentNumOfPlayers < maxPlayers)
        {
            currentNumOfPlayers++;
            return true;
        }

        return false;
    }

    /// <summary>
    /// If the current number of players is 1 or more, decrements the current number of players by 1.
    /// </summary>
    /// <returns>Returns true if a player has been successfully removed from the match, else returns false.</returns>
    public bool RemovePlayerFromMatch(NetworkConnection conn)
    {
        if (!isServer) return false;

        // Remove NetworkConnection from connections list
        if (connections != null)
        {
            connections.Remove(conn);
        }

        // Decrement currentNumOfPlayers counter
        if (currentNumOfPlayers > 1)
        {
            currentNumOfPlayers--;
            return true;
        }

        return false;
    }
    
    /// <returns>Returns the current number of players in the match.</returns>
    public int GetNumOfPlayers()
    {
        return currentNumOfPlayers;
    }
}
