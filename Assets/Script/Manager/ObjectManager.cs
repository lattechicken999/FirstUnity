using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectManager  : Singleton<ObjectManager>
{
    [SerializeField] GameObject _playerPrefeb;
    [SerializeField] GameObject _monsterPrefeb;
    [SerializeField] GameObject _bulletPrefeb;

    private bool _isPlayerWorking;
    private int _nowEnemyNum;

    public int MaximumEnemy { get; set; }
    protected override void init()
    {
        _nowEnemyNum = 0;
    }

    public GameObject CreatePlayer()
    {
        if(!_isPlayerWorking)
        {
            return Instantiate(_playerPrefeb);
        }
        else
        {
            return null;
        }
    }
    public void GameObjectActive(GameObject target)
    {
        target.SetActive(true);
    }
    public void GameObjectDeactive(GameObject target)
    {
        target.SetActive(false);
    }
    public void Destroy(GameObject destroyTarget)
    {
        if(destroyTarget != null)
            Destroy(destroyTarget);
    }

    public GameObject CreateMonster()
    {
        if (_nowEnemyNum >= MaximumEnemy)
        {
            return null;
        }
        else
        {
            return Instantiate(_monsterPrefeb);
        }
    }

    public GameObject CreateMonster(bool isActive)
    {
        if (_nowEnemyNum >= MaximumEnemy)
        {
            return null;
        }
        else
        {
            var monstorInstance = Instantiate(_monsterPrefeb);
            monstorInstance.SetActive(isActive);
            return monstorInstance;
        }
    }

    public GameObject CreateBullet(Vector3 weaponPosition)
    {
        GameObject bullet = Instantiate(_bulletPrefeb);
        bullet.transform.position = weaponPosition;

        return bullet;
    }


}

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject newGameObj = new GameObject(typeof(T).Name);
                    _instance = newGameObj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if(_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
            init();
        }
        else
        {
            if(_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    protected virtual void init()
    {

    }
}
