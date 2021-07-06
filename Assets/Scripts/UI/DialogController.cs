using System.Runtime.CompilerServices;
using System.Text;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : ToggleablePanel
{
    
    [SerializeField] private TMP_Text _storyText;
    [SerializeField] private Button[] _choiceButtons;

    private Story _story;
    
    

    [ContextMenu("Start Dialog")]
    public void StartDialog(TextAsset dialog)
    {
        _story = new Story(dialog.text);
        Show();
        RefreshView();
    }

    private void RefreshView()
    {
        StringBuilder storyTextBuilder = new StringBuilder();

        while (_story.canContinue)
        {
            storyTextBuilder.AppendLine(_story.Continue());
            HandleTags();
        }
        
        _storyText.SetText(storyTextBuilder);

        if (_story.currentChoices.Count == 0)
        {
            Hide();
        }
        else
        {
            ShowChoiceButtons();
        }
    }

    private void ShowChoiceButtons()
    {
        for (int i = 0; i < _choiceButtons.Length; i++)
        {
            var button = _choiceButtons[i];
            button.gameObject.SetActive(i < _story.currentChoices.Count);
            button.onClick.RemoveAllListeners();
            if (i < _story.currentChoices.Count)
            {
                var choice = _story.currentChoices[i];
                button.GetComponentInChildren<TMP_Text>().SetText(choice.text);
                button.onClick.AddListener(() =>
                {
                    _story.ChooseChoiceIndex(choice.index);
                    RefreshView();
                });
            }
        }
    }

    private void HandleTags()
    {
        foreach (var tag in _story.currentTags)
        {
            if (tag.StartsWith("E."))
            {
                string eventName = tag.Remove(0, 2);
                GameEvent.RaiseEvent(eventName);
            }
            else if (tag.StartsWith("Q."))
            {
                string questName = tag.Remove(0, 2);
                QuestManager.Instance.AddQuestByName(questName);
            }
            else if (tag.StartsWith("F."))
            {
                var values = tag.Split('.');
                // #F.BrokenPanelsInspected.9
                //string flagName = tag.Remove(0, 2);
                FlagManager.Instance.Set(values[1], values[2]);
            }
        }
    }
    
}