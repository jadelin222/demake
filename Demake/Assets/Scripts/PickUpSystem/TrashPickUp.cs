using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPickUp : PickUp
{
    public override string InteractionVerb => "Clean";

    public override void Interact()
    {
        Destroy(gameObject);
    }

}
