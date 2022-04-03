using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BinDragAndDropSlot : MonoBehaviour, IDropHandler
{
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    public void OnDrop(PointerEventData eventData)
    {
        Destroy(DragHandler.itemBeingDragged);
        ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged());
        audioSource.Play();
    }
}
