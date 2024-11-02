using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private float _timeBooster = 0.01f;

    private bool _inGame = true;

    private void Start()
    {
        Debug.Log(Time.timeScale);
       // InvokeRepeating(nameof(AccelerateTime), 1, 1);
    }

    private void Update()
    {

    }

    private void AccelerateTime()
    {
            Time.timeScale += _timeBooster;

            Debug.Log(Time.timeScale);

            Debug.Log(Time.timeScale + "Added");

            //yield return new WaitForSeconds(1);
    }
}
