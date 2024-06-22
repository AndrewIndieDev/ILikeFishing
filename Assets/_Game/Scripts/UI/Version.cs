using TMPro;
using UnityEngine;

public class Version : MonoBehaviour
{
    public TMP_Text version;
    private void Awake()
    {
        version.text = Application.version;

    }
}
