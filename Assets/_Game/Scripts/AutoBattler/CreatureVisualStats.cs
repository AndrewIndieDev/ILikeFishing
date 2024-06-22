using TMPro;
using UnityEngine;

public class CreatureVisualStats : MonoBehaviour
{
    [Header("Reference")]
    public CreatureBase Creature;

    [Header("UI")]
    public TMP_Text Health;
    public TMP_Text AttackDamage;
    
    private void Awake()
    {
        if (Creature == null)
        {
            Debug.LogError($"Creature Visual Stats: No creature attached to {gameObject.name}, disabling visuals!");
            enabled = false;
            return;
        }

        Creature.onHealthChanged += OnHealthChanged;
        Creature.onAttackDamageChanged += OnAttackDamageChanged;
    }

    private void OnDestroy()
    {
        if (Creature != null)
        {
            Creature.onHealthChanged -= OnHealthChanged;
            Creature.onAttackDamageChanged -= OnAttackDamageChanged;
        }
    }

    private void OnHealthChanged(int oldHealth, int newHealth)
    {
        Health.text = newHealth.ToString();
    }

    private void OnAttackDamageChanged(int oldAttackDamage, int newAttackDamage)
    {
        AttackDamage.text = newAttackDamage.ToString();
    }
}
