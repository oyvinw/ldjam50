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

    private List<(CannonPart, Sprite)> cannonParts;
    private CannonController cannon;
    // Start is called before the first frame update
    void Start()
    {
        cannonParts = new List<(CannonPart, Sprite)>();
        cannon = FindObjectOfType<CannonController>();
        HasChanged();    
    }

    public void HasChanged()
    {
        cannonParts = new List<(CannonPart, Sprite)>();
        foreach (Transform slotTransform in slots)
        {
            GameObject cannonPart = slotTransform.GetComponent<DragAndDropSlot>().item;
            if (cannonPart)
            {
                cannonParts.Add((cannonPart.GetComponent<CannonPart>(), cannonPart.GetComponent<Image>().sprite)); 
            }
            else
            {
                cannonParts.Add((emptyCannonPart, emptyCannonSprite));
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
