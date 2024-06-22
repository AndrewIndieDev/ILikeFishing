using UnityEngine;

public class AutoBattleManager : MonoBehaviour
{
    public static AutoBattleManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public Transform InventoryParent;
}
