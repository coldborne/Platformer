using System;
using UnityEngine;

namespace Input
{
    public class InputReader : MonoBehaviour
    {
        private const string Horizontal = nameof(Horizontal);

        private const KeyCode LeftMovingKey = KeyCode.A;
        private const KeyCode LeftArrowMovingKey = KeyCode.LeftArrow;

        private const KeyCode RightMovingKey = KeyCode.D;
        private const KeyCode RightArrowMovingKey = KeyCode.RightArrow;
        
        private const KeyCode AbilityActivationKey = KeyCode.Q;

        public event Action<float> MoveButtonPressed;
        public event Action JumpButtonPressed;
        public event Action AbilityActivationPressed;

        private void Update()
        {
            if (UnityEngine.Input.GetKey(LeftMovingKey) || UnityEngine.Input.GetKey(RightMovingKey) ||
                UnityEngine.Input.GetKey(LeftArrowMovingKey) || UnityEngine.Input.GetKey(RightArrowMovingKey))
            {
                MoveButtonPressed?.Invoke(UnityEngine.Input.GetAxis(Horizontal));
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                JumpButtonPressed?.Invoke();
            }

            if (UnityEngine.Input.GetKeyDown(AbilityActivationKey))
            {
                AbilityActivationPressed?.Invoke();
            }
        }
    }
}