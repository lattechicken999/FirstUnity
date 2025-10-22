using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeyComponent : MonoBehaviour
{
    private float _horInput;
    private float _verInput;

    public float HorInput => _horInput;
    public float VerInput => _verInput;

    // Update is called once per frame
    void Update()
    {
        _horInput = Input.GetAxisRaw("Horizontal");
        _verInput = Input.GetAxisRaw("Vertical");

        Debug.Log($"Horizontal  = {_horInput}");
        Debug.Log($"Vertical  = {_verInput}");

    }

}
