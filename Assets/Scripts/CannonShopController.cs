using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShopController : MonoBehaviour
{
    [SerializeField] CannonPart[] partsPool;

    private ShopDragAndDropSlot[] shopSlots;

    private void Start()
    {
        shopSlots = GetComponentsInChildren<ShopDragAndDropSlot>();
        RestockShop();
    }

    public void RestockShop()
    {
        foreach (var slot in shopSlots)
        {
            for (int i = slot.transform.childCount; i > 0; i--)
            {
                Destroy(slot.transform.GetChild(0).gameObject);
            }

            bool foundPart = false;
            while (!foundPart)
            {
                var partCandidate = partsPool[Random.Range(0, partsPool.Length)];    
                if (Random.value < partCandidate.rarity)
                {
                    Instantiate(partCandidate, slot.transform);
                    foundPart = true;
                }
            }
        }
    }
}
