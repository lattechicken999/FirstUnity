using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _hpSetting;
    [SerializeField] private int _movingForce;
    [SerializeField] private float _EnemyDamage;

    private int _hp;
    private Rigidbody _myRigd;
    private System.Random _rand;
    private ObjectManager ObjectManager;

    private void Awake()
    {
        _hp = _hpSetting;
        _myRigd = GetComponent<Rigidbody>();
        _rand = new System.Random();
        ObjectManager = ObjectManager.Instance;
    }
    private void Start()
    {
        //시작시 움직이고 시작
        RandomForce();
    }

    private void Update()
    {
        //1%확률로 조작
        if (_rand.Next(0, 101) % 100 == 0)
        {
            RandomForce();
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
         if (collision.gameObject.CompareTag("DeleteWall"))
        {
            _myRigd.velocity = Vector3.zero;
            _myRigd.angularVelocity = Vector3.zero;
            _hp = _hpSetting;
            ObjectManager.GameObjectDeactive(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            _hp--;

            if(_hp <=0)
            {
                _myRigd.velocity = Vector3.zero;
                _myRigd.angularVelocity = Vector3.zero;
                _hp = _hpSetting;
                ObjectManager.GameObjectDeactive(gameObject);
            }
        }

        else if(other.gameObject.CompareTag("Player"))
        {
            var playerComponent = other.GetComponent<Player>();
            if (playerComponent != null)
            {
                playerComponent.OnTakeDamage(_EnemyDamage);
            }

        }
    }
    
    private void RandomForce()
    {
        Vector3 rndVec = new Vector3(_rand.Next(-10, 10), 0, _rand.Next(-10, 2));
        _myRigd.AddForce(rndVec.normalized* _movingForce);
    }
}
