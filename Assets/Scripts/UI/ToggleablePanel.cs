using System.Collections.Generic;
using UnityEngine;

public class ToggleablePanel : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    private static HashSet<ToggleablePanel> _visiblePanels = new HashSet<ToggleablePanel>();
    
    public static bool IsVisible => _visiblePanels.Count > 0;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        Hide();
    }
    public void Show()
    {
        _visiblePanels.Add(this);
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        _visiblePanels.Remove(this);
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }
}