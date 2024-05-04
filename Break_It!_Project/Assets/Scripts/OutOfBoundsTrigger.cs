using UnityEngine;
using System;


public class OutOfBoundsTrigger : MonoBehaviour
{
    public event Action OnBallOutOfBounds;

    void OnTriggerEnter(Collider other)
    {
        string ballTag = "Ball";

        if (other.tag == ballTag)
        {
            OnBallOutOfBounds?.Invoke();
        }
    }
}
