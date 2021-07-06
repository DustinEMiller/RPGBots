using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InspectionPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _hintText;
    [SerializeField] private TMP_Text _completedInspectionText;
    [SerializeField] private Image _progressBarFilledImage;
    [SerializeField] private GameObject _progressBar;

    private void Update()
    {
        if (InspectionManager.Inspecting)
        {
            _progressBarFilledImage.fillAmount = InspectionManager.InspectionProgress;
            _progressBar.SetActive(true);
        } 
        else if(_progressBar.activeSelf)
        {
            _progressBar.SetActive(false);
        }
    }
    
    private void OnEnable()
    {
        _hintText.enabled = false;
        _completedInspectionText.enabled = false;
        Inspectable.InspectablesInRangeChanged += UpdateHintTextState;
        Inspectable.OnAnyInspectionComplete += ShowCompletedInspectiontext;
    }

    private void ShowCompletedInspectiontext(Inspectable inspectable, string completedInspectionMessage)
    {
        _completedInspectionText.SetText(completedInspectionMessage);
        _completedInspectionText.enabled = true;
        float messageTime = completedInspectionMessage.Length / 5f;
        messageTime = Mathf.Clamp(messageTime, 3f, 10f);
        StartCoroutine(FadeCompletedText(messageTime));
    }

    private IEnumerator FadeCompletedText(float messageTime)
    {
        _completedInspectionText.alpha = 1;
        while (_completedInspectionText.alpha > 0)
        {
            yield return null;
            _completedInspectionText.alpha -= Time.deltaTime / messageTime;
        }

        _completedInspectionText.enabled = false;
    }

    private void OnDisable()
    {
        Inspectable.InspectablesInRangeChanged -= UpdateHintTextState;
    }

    private void UpdateHintTextState(bool enableHint)
    {
        _hintText.enabled = enableHint;
    }
}