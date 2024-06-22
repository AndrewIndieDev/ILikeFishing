using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreatureBase : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    #region OnPointerHandlers
    private Vector2 offsetToMouse = Vector2.zero;
    private TeamSlot slotMovedFrom = null;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;
        
        offsetToMouse = eventData.position - (Vector2)transform.position;
        gameObject.GetComponent<Image>().raycastTarget = false;
        slotMovedFrom = MouseManager.Instance.HoveredSlot;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        transform.position = eventData.position - offsetToMouse;
        if (slotMovedFrom == MouseManager.Instance.HoveredSlot)
            MouseManager.Instance.HoveredSlot = null;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        if (MouseManager.Instance.HoveredSlot != null)
        {
            transform.SetParent(MouseManager.Instance.HoveredSlot.transform);
            transform.localPosition = Vector2.zero;
        }
        else
        {
            transform.SetParent(AutoBattleManager.Instance.InventoryParent);
            LayoutRebuilder.ForceRebuildLayoutImmediate(AutoBattleManager.Instance.InventoryParent.GetComponent<RectTransform>());
        }
        gameObject.GetComponent<Image>().raycastTarget = true;
    }
    #endregion

    [Header("Details")]
    public string Name;
    [TextArea]
    public string Description;
    public Sprite Artwork;

    [Header("Stats")]
    public int Health;
    public int AttackDamage;

    [Header("UI")]
    public Image ArtworkImage;

    public delegate void OnHealthChanged(int oldAmount, int newAmount);
    public event OnHealthChanged onHealthChanged;
    public delegate void OnAttackDamageChanged(int oldAmount, int newAmount);
    public event OnAttackDamageChanged onAttackDamageChanged;

    public virtual void Start()
    {
        ArtworkImage.sprite = Artwork;
        onHealthChanged?.Invoke(Health, Health);
        onAttackDamageChanged?.Invoke(AttackDamage, AttackDamage);
    }

    public virtual void ChangeHealthBy(int amount)
    {
        Health += amount;
        onHealthChanged?.Invoke(Health - amount, Health);
    }

    public virtual void SetHealthTo(int amount)
    {
        if (Health != amount)
        {
            int oldHealth = Health;
            Health = amount;
            onHealthChanged?.Invoke(oldHealth, Health);
        }
    }

    public virtual void ChangeAttackDamageBy(int amount)
    {
        AttackDamage += amount;
        onAttackDamageChanged?.Invoke(AttackDamage - amount, AttackDamage);
    }

    public virtual void SetAttackDamageTo(int amount)
    {
        if (Health != amount)
        {
            int oldAttackDamage = AttackDamage;
            AttackDamage = amount;
            onAttackDamageChanged?.Invoke(oldAttackDamage, AttackDamage);
        }
    }
}