using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputKeyComponent))]
[RequireComponent(typeof(SphereCollider))]
public class NewBehaviourScript : MonoBehaviour
{
    private InputKeyComponent _inputComponent;
    private SphereCollider _myCollider;

    private Vector3 _minWorlBound;
    private Vector3 _maxWorlBound;

    [SerializeField] private GameObject _movingPlane;
    [SerializeField] private float _moveSpeed;
    private void Start()
    {
        _inputComponent = GetComponent<InputKeyComponent>();
        _myCollider = GetComponent<SphereCollider>();

        if (_movingPlane != null)
        {
            Bounds planeBounds = _movingPlane.GetComponent<MeshRenderer>().bounds;
            _minWorlBound = planeBounds.center - planeBounds.extents;// + gameObject.GetComponent<MeshRenderer>().bounds.extents *2;
            _maxWorlBound = planeBounds.center + planeBounds.extents;// - gameObject.GetComponent<MeshRenderer>().bounds.extents *2;
        }
        if (_myCollider != null)
        {
            _minWorlBound += _myCollider.bounds.extents *2 ;
            _maxWorlBound -= _myCollider.bounds.extents *2;
        }
    }
    // Update is called once per frame
    void Update()
    {

        Move();
    }

    private void Move()
    {
        Vector3 inputVec = new Vector3(_inputComponent.HorInput, 0f, _inputComponent.VerInput).normalized;
        Vector3 deltaMovement = inputVec*_moveSpeed*Time.deltaTime;
        Vector3 nextPosition = transform.position + deltaMovement;

        nextPosition.x = Mathf.Clamp(nextPosition.x,_minWorlBound.x, _maxWorlBound.x);
        nextPosition.z = Mathf.Clamp(nextPosition.z,_minWorlBound.z, _maxWorlBound.z);

        transform.position = nextPosition;


    }

}
