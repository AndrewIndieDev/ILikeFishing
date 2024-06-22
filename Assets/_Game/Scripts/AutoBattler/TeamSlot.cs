using UnityEngine;
using UnityEngine.EventSystems;

public class TeamSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool SlotTaken { get { return transform.childCount > 0; } }
    public CreatureBase CreatureInSlot { get { return SlotTaken ? transform.GetChild(0).GetComponent<CreatureBase>() : null; } }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!SlotTaken)
            MouseManager.Instance.HoveredSlot = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (MouseManager.Instance.HoveredSlot == this)
            MouseManager.Instance.HoveredSlot = null;
    }
}
