using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CannonBuilder : MonoBehaviour, IHasChanged
{
    [SerializeField] Transform slots;
    [SerializeField] CannonPart emptyCannonPart;
    [SerializeField] Sprite emptyCannonSprite;
    [SerializeField] List<CannonPart> initialCannonParts;

    private List<(CannonPart, Sprite, Color)> cannonParts;
    private CannonController cannon;
    private bool updateNextFrame;

    // Start is called before the first frame update
    void Start()
    {
        cannonParts = new List<(CannonPart, Sprite, Color)>();
        cannon = FindObjectOfType<CannonController>();
        Reset();
    }

    public void HasChanged()
    {
        cannonParts = new List<(CannonPart, Sprite, Color)>();
        foreach (Transform slotTransform in slots)
        {
            var dragAndDropSlot = slotTransform.GetComponent<DragAndDropSlot>();

            if (dragAndDropSlot == null)
                break;

            GameObject cannonPart = dragAndDropSlot.item;
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

    private void FixedUpdate()
    {
        if (updateNextFrame)
        {
            HasChanged();
            updateNextFrame = false;
        }
    }

    public void Reset()
    {
        for (int i = 0; i < slots.childCount; i++)
        {
            var dragAndDropSlot = slots.GetChild(i).GetComponent<DragAndDropSlot>();
            if (dragAndDropSlot == null)
                break;

            if (initialCannonParts[i] != null)
            {
                dragAndDropSlot.item = initialCannonParts[i].gameObject;
            }

            else
            {
                dragAndDropSlot.item = null;
            }
        }

        updateNextFrame = true;
    }
}

namespace UnityEngine.EventSystems
{
    public interface IHasChanged : IEventSystemHandler
    {
        void HasChanged();
    }
}
