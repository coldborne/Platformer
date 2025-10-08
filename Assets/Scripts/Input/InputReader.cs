using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);

    private const KeyCode LeftMovingKey = KeyCode.A;
    private const KeyCode LeftArrowMovingKey = KeyCode.LeftArrow;

    private const KeyCode RightMovingKey = KeyCode.D;
    private const KeyCode RightArrowMovingKey = KeyCode.RightArrow;

    public event Action<float> MoveButtonPressed;
    public event Action JumpButtonPressed;

    private void Update()
    {
        if (Input.GetKey(LeftMovingKey) || Input.GetKey(RightMovingKey) ||
            Input.GetKey(LeftArrowMovingKey) || Input.GetKey(RightArrowMovingKey))
        {
            MoveButtonPressed?.Invoke(Input.GetAxis(Horizontal));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpButtonPressed?.Invoke();
        }
    }
}