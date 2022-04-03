using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CannonShopDisplay : MonoBehaviour, IHasChanged
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

    public void HasChanged()
    {
        throw new System.NotImplementedException();
    }
}
