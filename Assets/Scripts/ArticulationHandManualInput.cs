using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ArticulationHandManualInput : NetworkBehaviour
{
    public GameObject hand;

    void Update()
    {
        // manual input
        if (!IsOwner)
        {
            return;
        }

        PincherMovementServerRpc();
    }


    [ServerRpc(RequireOwnership = false)]
    private void PincherMovementServerRpc()
    {
        float input = Input.GetAxis("Fingers");
        PincherController pincherController = hand.GetComponent<PincherController>();
        pincherController.gripState = GripStateForInput(input);
    }

    // INPUT HELPERS

    static GripState GripStateForInput(float input)
    {
        if (input > 0)
        {
            return GripState.Closing;
        }
        else if (input < 0)
        {
            return GripState.Opening;
        }
        else
        {
            return GripState.Fixed;
        }
    }
}
