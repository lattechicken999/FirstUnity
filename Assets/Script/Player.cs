using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]

public class Player : MonoBehaviour,IPlayerHPObserver,IPlayCheckOberver
{
    [SerializeField] private GameObject _movingPlane;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _curHp;
    [SerializeField] private Transform _startPoint;
    //[SerializeField] private UnityEvent<float> _takeDamage;

    private SphereCollider _myCollider;
    private Vector3 _minWorlBound;
    private Vector3 _maxWorlBound;

    private float _horInput;
    private float _verInput;


    private void Awake()
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("CollisionImpossible"), LayerMask.NameToLayer("Wall"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("CollisionImpossible"), LayerMask.NameToLayer("Monster"), true);

    }
    private void Start()
    {
        _myCollider = GetComponent<SphereCollider>();

        if (_movingPlane != null)
        {
            Bounds planeBounds = _movingPlane.GetComponent<MeshRenderer>().bounds;
            _minWorlBound = planeBounds.center - planeBounds.extents;// + gameObject.GetComponent<MeshRenderer>().bounds.extents *2;
            _maxWorlBound = planeBounds.center + planeBounds.extents;// - gameObject.GetComponent<MeshRenderer>().bounds.extents *2;
        }
        if (_myCollider != null)
        {
            _minWorlBound += _myCollider.bounds.extents ;
            _maxWorlBound -= _myCollider.bounds.extents ;
        }
        PlayerHpManager.Instance.AddSubscriber(this);
        GameManager.Instance.AddSubscriber(this);
        ObjectManager.Instance.GameObjectDeactive(gameObject);
    }
    private void OnEnable()
    {
        transform.position = _startPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        _horInput = Input.GetAxisRaw("Horizontal");
        _verInput = Input.GetAxisRaw("Vertical");

        Move();
    }

    private void Move()
    {
        Vector3 inputVec = new Vector3(_horInput, 0f, _verInput).normalized;
        Vector3 deltaMovement = inputVec * _moveSpeed * Time.deltaTime;
        Vector3 nextPosition = transform.position + deltaMovement;

        nextPosition.x = Mathf.Clamp(nextPosition.x, _minWorlBound.x, _maxWorlBound.x);
        nextPosition.z = Mathf.Clamp(nextPosition.z, _minWorlBound.z, _maxWorlBound.z);

        transform.position = nextPosition;
    }
    public void OnTakeDamage(float damage)
    {
        //_takeDamage?.Invoke(damage);
        PlayerHpManager.Instance.OnTakeDamage(damage);
    }

    public void OnPlayerHpNotify(float playerHp)
    {
        _curHp = playerHp;
        if (_curHp <= 0)
        {
            ObjectManager.Instance.GameObjectDeactive(gameObject);
        }
    }

    public void PlayableNofity(bool isPlayable)
    {
        if (isPlayable)
        {
            ObjectManager.Instance.GameObjectActive(gameObject);
        }
        else
        {
            ObjectManager.Instance.GameObjectDeactive(gameObject);
        }
    }
}
