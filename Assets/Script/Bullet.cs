using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private float _bulletRange = 0;
    [SerializeField]private int _maxBulletRange;
    [SerializeField] private float _bulletSpeed;
    private ObjectManager ObjectManager;

    private void Awake()
    {
        ObjectManager = ObjectManager.Instance;
    }
    // Start is called before the first frame update

    void Start()
    {
        ObjectManager.GameObjectActive(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf)
        {
            transform.position += Vector3.forward* _bulletSpeed * Time.deltaTime; 
            if (transform.position.z >= _maxBulletRange)
            {
                ObjectManager.GameObjectDeactive(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ObjectManager.GameObjectDeactive(gameObject);
    }
}
