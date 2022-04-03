using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CannonBuilder : MonoBehaviour, IHasChanged
{
    [SerializeField] Transform slots;
    [SerializeField] CannonPart emptyCannonPart;
    [SerializeField] Sprite emptyCannonSprite;

    private List<(CannonPart, Sprite, Color)> cannonParts;
    private CannonController cannon;
    // Start is called before the first frame update
    void Start()
    {
        cannonParts = new List<(CannonPart, Sprite, Color)>();
        cannon = FindObjectOfType<CannonController>();
        HasChanged();    
    }

    public void HasChanged()
    {
        cannonParts = new List<(CannonPart, Sprite, Color)>();
        foreach (Transform slotTransform in slots)
        {
            GameObject cannonPart = slotTransform.GetComponent<DragAndDropSlot>().item;
            if (cannonPart)
            {
                var image = cannonPart.GetComponent<Image>();
                cannonParts.Add((cannonPart.GetComponent<CannonPart>(), image.sprite, image.color)); 
            }
            else
            {
                cannonParts.Add((emptyCannonPart, emptyCannonSprite, Color.white));
            }
        }
        cannon.SetCannon(cannonParts);
    }
}

namespace UnityEngine.EventSystems
{
    public interface IHasChanged : IEventSystemHandler
    {
        void HasChanged();
    }
}
