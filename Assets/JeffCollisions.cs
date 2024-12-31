using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeffCollisions : TagCollisionHandler
{
    public PlayerHandler playerHandler;
    protected override void InitializeTagActions()
    {
        tagActions.Add("Peanut", (obj) =>
        {
            ObjectType objType = obj.GetComponent<ObjectType>();
            if (objType != null) // Ensure the object has an ObjectType component
            {
                switch (objType.type) // Adjusted to match the ObjectType field name
                {
                    case "Peanut":
                        Debug.Log("PEANUT");
                        break;
                    case "Projectile":
                        Debug.Log("Projectile");
                        break;
                    default:
                        Debug.Log($"Unknown type: {objType.type}");
                        break;
                }
            }
            else
            {
                Debug.LogWarning($"Object {obj.name} does not have an ObjectType component!");
            }
        });
        tagActions.Add("Peach", (obj) => {
            playerHandler?.Die();
            });
        
    }
}
