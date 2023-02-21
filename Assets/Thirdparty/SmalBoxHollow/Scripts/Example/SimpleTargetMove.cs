using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTargetMove : MonoBehaviour
{
    [Header("速度")]
    public float speed = 1;

    private Vector3 _startPos;

    private void Start()
    {
        _startPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, speed * Time.deltaTime, 0, Space.Self);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -speed * Time.deltaTime, 0, Space.Self);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0, Space.Self);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0, Space.Self);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            transform.position = _startPos;
        }
    }
}
