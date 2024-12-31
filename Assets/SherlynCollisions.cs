using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SherlynCollisions : TagCollisionHandler
{
    public PlayerHandler playerHandler;
    protected override void InitializeTagActions()
    {
        tagActions.Add("Peach", (obj) => { });
        tagActions.Add("Peanut", (obj) => {
            playerHandler?.Die();
         });
    }
}
