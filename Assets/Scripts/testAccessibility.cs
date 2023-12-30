using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class testAccessibility : NetworkBehaviour
{
    private PlayerNetwork ownedObject;

    public void OnEnable()
    {
        ownedObject = FindObjectOfType<PlayerNetwork>();
    }

    private void Update()
    {
        if (!IsOwner)
        {
            //Debug.Log("I have no Ownership!");
            return;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            SwitchOwnerServerRPC();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void SwitchOwnerServerRPC()
    {
        Debug.Log("Actual Object OwnerId: " + ownedObject.OwnerClientId.ToString());
        List<ulong> clientsIds = NetworkManager.ConnectedClientsIds.ToList();
        
        if (ownedObject.OwnerClientId == 0 && clientsIds.Contains(1))
        {
            ownedObject.NetworkObject.ChangeOwnership(1);
        }
        else if (ownedObject.OwnerClientId == 1 && clientsIds.Contains(0))
        {
            ownedObject.NetworkObject.ChangeOwnership(0);
        }
            
        Debug.Log("New Object OwnerID: " + ownedObject.OwnerClientId.ToString());
        Debug.Log(ownedObject.IsOwner);
    }
    
}
