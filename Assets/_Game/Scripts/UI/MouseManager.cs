using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public static MouseManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public TeamSlot HoveredSlot;
    
    public void SetHoveredSlot(TeamSlot slot)
    {
        HoveredSlot = slot;
    }
}
