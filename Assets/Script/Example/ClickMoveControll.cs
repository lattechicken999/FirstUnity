using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ClickMoveControll : MonoBehaviour
{
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _rotateSpeed = 10f;

    private Vector3 _targetPos;
    private bool _hasTarget = false;

    private void Awake()
    {
        if(_playerCamera == null)
        {
            _playerCamera = Camera.main;

        }
        _targetPos = transform.position;
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _playerCamera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out RaycastHit hit, 100f,_groundMask))
            {
                _targetPos = hit.point;
                _hasTarget = true;
            }
        }
    }

    private void MoveToTarget()
    {
        if (_hasTarget == false)
        {
            return;
        }
        Vector3 direction = _targetPos - transform.position;
        direction.y = 0f;
        float distance = direction.magnitude;

        if (distance > 0.0025f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);

            Vector3 move = direction.normalized * _moveSpeed * Time.deltaTime;
            //transform.position = move;
            if (move.magnitude > distance)
            {
                move = direction.normalized * distance;
            }
            transform.position += move;
        }
        else
        {
            _hasTarget = false;
        }
    }
    private void OnDrawGizmos()
    {
        if (_hasTarget)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_targetPos + Vector3.up * 0.3f, 1f);
        }
    
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseInput();
        MoveToTarget();
    }
}
