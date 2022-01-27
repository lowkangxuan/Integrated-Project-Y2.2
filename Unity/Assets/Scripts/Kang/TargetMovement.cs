using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    [SerializeField] private float _transitionSpeed;
    [SerializeField] private Transform _inPosition;
    [SerializeField] private Transform _outPosition;

    public bool canMove;
    [SerializeField] private bool _isOut;

    private Vector3 _basePosition;
    private float _elapsedTime;

    private void Start()
    {
        _basePosition = gameObject.transform.position;
    }

    private void Update()
    {
        if (_isOut && canMove)
        {
            _elapsedTime += Time.deltaTime;
            float percentageCompletion = _elapsedTime / _transitionSpeed;

            gameObject.transform.position = Vector3.Lerp(_basePosition, _inPosition.position, percentageCompletion);

            if (gameObject.transform.position == _inPosition.position)
            {
                _isOut = false;
                canMove = false;
                _elapsedTime = 0;

                _basePosition = gameObject.transform.position;
            }
        }
        else if (!_isOut && canMove)
        {
            _elapsedTime += Time.deltaTime;
            float percentageCompletion = _elapsedTime / _transitionSpeed;

            gameObject.transform.position = Vector3.Lerp(_basePosition, _outPosition.position, percentageCompletion);

            if (gameObject.transform.position == _outPosition.position)
            {
                _isOut = true;
                canMove = false;
                _elapsedTime = 0;

                _basePosition = gameObject.transform.position;
            }
        }
    }
}
