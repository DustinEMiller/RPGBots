using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogGiver : MonoBehaviour
{
    [SerializeField] private TextAsset _dialog;
    private DialogController _dialogController;

    private void Awake()
    {
        _dialogController = FindObjectOfType<DialogController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<ThirdPersonMovement>();
        if (player != null)
        {
            _dialogController.StartDialog(_dialog);
            transform.LookAt(player.transform);
        }
    }
    
}
