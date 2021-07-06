using System;
using UnityEngine;

public class FlippyBoxLoseCollider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.CompareTag("Player"))
            GetComponentInParent<FlippyBoxMinigame>().Lose();
    }
}