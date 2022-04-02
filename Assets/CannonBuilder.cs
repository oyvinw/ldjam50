using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CannonBuilder : MonoBehaviour
{
    [SerializeField] Transform slots;
    private List<(CannonPart, Sprite)> cannonParts;
    private CannonController cannon;
    // Start is called before the first frame update
    void Start()
    {
        cannon = FindObjectOfType<CannonController>();
        HasChanged();    
    }

    public void HasChanged()
    {
        foreach (Transform slotTransform in slots)
        {
            GameObject cannonPart = slotTransform.GetComponent<DragAndDropSlot>().item;
            if (cannonPart)
            {
                cannonParts.Add((cannonPart.GetComponent<CannonPart>(), cannonPart.GetComponent<Image>().sprite)); 
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
