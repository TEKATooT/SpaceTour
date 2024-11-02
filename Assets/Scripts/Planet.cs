using System;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public event Action Destroyed;

    private void Start()
    {

    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Destroyed?.Invoke();
        Debug.Log("TRIGER");

        if (other.TryGetComponent(out PlayerMover player))
        {
            player.MoveBoost();
        }
        

        gameObject.SetActive(false);

    }
}
