using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShopDisplay : MonoBehaviour
{
    private Canvas cannonShopCanvas;

    // Start is called before the first frame update
    void Start()
    {
        cannonShopCanvas = GetComponentInChildren<Canvas>(); 
    }

    public void DisplayCannonShop()
    {
        cannonShopCanvas.enabled = true;
    }

    public void HideCannonShop()
    {
        cannonShopCanvas.enabled = false;
    }

}
