using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Text;

public class DragAndDropSlot : MonoBehaviour, IDropHandler
{
    public TextMeshProUGUI description;
    private string blue = "<color=#64a1c2>";
    private string endColorAndLine = "</color>\n";

    public GameObject item
    {
        get
        {
            if(transform.childCount > 0)
            {
                return GetComponentInChildren<CannonPart>().gameObject;
            }

            return null;
        }

        set
        {
            for (int i = transform.childCount; i > 0; i--)
            {
                Destroy(transform.GetChild(i-1).gameObject);
            }

            if (value == null)
            {
                return;
            }

            Instantiate(value, transform);
        }
    }

    internal string GetEmptyText()
    {
        var sb = new StringBuilder();
        sb.Append(blue);
        sb.Append("empty");
        sb.Append(endColorAndLine);
        return sb.ToString();
    }

    public void OnDrag(PointerEventData eventData)
    {
        description.text = GetEmptyText();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!item)
        {
            DragHandler.itemBeingDragged.transform.SetParent(transform);
            var cannonPart = DragHandler.itemBeingDragged.GetComponent<CannonPart>();
            if (cannonPart != null && description != null)
            {
                description.text = cannonPart.GetDescription();
            }

            ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged());
        }
        else
        {
            ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged());
        }
    }

}
