using System.Collections;
using System;

using System.Collections.Generic;
using UnityEngine;

public abstract class TagCollisionHandler : MonoBehaviour
{
    
    protected Dictionary<string, Action<GameObject>> tagActions;

    protected abstract void InitializeTagActions(); // Abstract method for child classes

    void Start()
    {
        tagActions = new Dictionary<string, Action<GameObject>>();
        InitializeTagActions(); // Initialize tag actions in derived classes
        /**
        protected override void InitializeTagActions()
    {
        tagActions.Add("A", (obj) => {});
        tagActions.Add("BB", (obj) => {});
    }
        **/
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        string otherTag = collision.gameObject.tag;

        if (tagActions.TryGetValue(otherTag, out Action<GameObject> action))
        {
            action.Invoke(collision.gameObject);
        }
        else
        {
            
        }
    }
}
