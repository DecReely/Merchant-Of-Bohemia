using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace MerchantOfBohemia
{
    [SelectionBase]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float movementDuration = 1; // Player Stats'e taşınacak, geçici.
        [SerializeField] private float rotationDuration = 0.3f; // Player Stats'e taşınacak, geçici.

        private GlowHighlight _glowHighlight;
        private Queue<Vector3> _pathPositions = new Queue<Vector3>();

        private void Awake()
        {
            _glowHighlight = GetComponent<GlowHighlight>();
        }

        public void Deselect()
        {
            _glowHighlight.ToggleGlow(false);
        }
        
        public void Select()
        {
            _glowHighlight.ToggleGlow(true);
        }

        public void MoveThroughPath(List<Vector3> currentPath)
        {
            _pathPositions = new Queue<Vector3>(currentPath);
            Vector3 firstTarget = _pathPositions.Dequeue();
            StartCoroutine(RotationCoroutine(firstTarget, rotationDuration));
        }

        private IEnumerator RotationCoroutine(Vector3 endPosition, float rotationDuration)
        {
            Transform _transform = transform;
            Vector3 _position = _transform.position;
            
            Quaternion startRotation = _transform.rotation;
            endPosition.y = _position.y;
            Vector3 direction = endPosition - _position;
            Quaternion endRotation = Quaternion.LookRotation(direction, Vector3.up);;

            if (Mathf.Approximately(MathF.Abs(Quaternion.Dot(startRotation, endRotation)), 1.0f) == false)
            {
                float timeElapsed = 0;
                while (timeElapsed < rotationDuration)
                {
                    timeElapsed += Time.deltaTime;
                    float lerpStep = timeElapsed / rotationDuration;
                    transform.rotation = Quaternion.Lerp(startRotation, endRotation, lerpStep);
                    yield return null;
                }
                transform.rotation = endRotation;
            }
            StartCoroutine(MovementCoroutine(endPosition));
        }

        private IEnumerator MovementCoroutine(Vector3 endPosition)
        {
            Vector3 startPosition = transform.position;
            endPosition.y = startPosition.y;
            float timeElapsed = 0;

            while (timeElapsed < movementDuration)
            {
                timeElapsed += Time.deltaTime;
                float lerpStep = timeElapsed / movementDuration;
                transform.position = Vector3.Lerp(startPosition, endPosition, lerpStep);
                yield return null;
            }

            transform.position = endPosition;

            if (_pathPositions.Count > 0)
            {
                StartCoroutine(RotationCoroutine(_pathPositions.Dequeue(), rotationDuration));
            }
            else
            {
                EventHandler.CallMovementFinishedEvent(this);
            }
        }
    }
}
