using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExtendControls : MonoBehaviour
{
    public TMP_Text arrow;
    public Button button;

    private const string ARROW_SHOWING = "<";
    private const string ARROW_HIDING = ">";

    public void Show()
    {
        arrow.text = ARROW_SHOWING;
        Tween tween = transform.DOMoveX(0f, 1f);
        button.onClick.RemoveAllListeners();
        tween.onComplete = () => button.onClick.AddListener(Hide);
    }

    public void Hide()
    {
        arrow.text = ARROW_HIDING;
        Tween tween = transform.DOMoveX(-1200f, 1f);
        button.onClick.RemoveAllListeners();
        tween.onComplete = () => button.onClick.AddListener(Show);
    }
}
