using System;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public event Action Destroyed;

    private void OnTriggerEnter(Collider other)
    {
        Destroyed?.Invoke();

        if (other.TryGetComponent(out PlayerEngine player))
        {
            player.UpBoost();
        }
    }
}
