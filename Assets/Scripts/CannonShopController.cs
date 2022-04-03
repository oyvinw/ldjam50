using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CannonShopController : MonoBehaviour
{
    [SerializeField] CannonPart[] partsPool;

    private ShopDragAndDropSlot[] shopSlots;
    private TextMeshProUGUI[] shopDescriptions;

    private void Start()
    {
        shopSlots = GetComponentsInChildren<ShopDragAndDropSlot>();
        shopDescriptions = GetComponentsInChildren<TextMeshProUGUI>();
        RestockShop();
    }

    public void RestockShop()
    {
        for (int i = 0; i < shopSlots.Length; i++)
        {
            //for (int j = shopSlots[i].transform.childCount - 1; j > 0; j--)
            //{
            if (shopSlots[i].transform.childCount > 0)
                Destroy(shopSlots[i].transform.GetChild(0).gameObject);
            //}

            bool foundPart = false;
            while (!foundPart)
            {
                var partCandidate = partsPool[Random.Range(0, partsPool.Length)];    
                if (Random.value < partCandidate.rarity)
                {
                    Instantiate(partCandidate, shopSlots[i].transform);
                    shopDescriptions[i].text = partCandidate.GetDescription();
                    foundPart = true;
                }
            }
        }
    }
}
