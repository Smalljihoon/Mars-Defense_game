using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnforceShop : MonoBehaviour
{
    private static EnforceShop instance;
    public static EnforceShop Instance
    { get { return instance; } }

    private void Start()
    {
        instance = this; 
    }
}
