using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour,IPlayCheckOberver
{
    //[SerializeField] GameObject _enemyPrefeb;
    [SerializeField] int _maximumEnemy;
    [SerializeField] float _spawnCoolTime;

    List<GameObject> _enemyList;
    WaitForSeconds _cooltimeCoroutine;
    System.Random _rnd;
    Coroutine _monsterRoutine;

    private int _minRange;
    private int _maxRange;

    Vector3 _monsterSpwanPosition;

    private void Awake()
    {
        _enemyList = new List<GameObject>();
        _rnd = new System.Random();

        //코루틴 반환값 초기화
        _cooltimeCoroutine = new WaitForSeconds(_spawnCoolTime);
        _monsterRoutine = null;


    }

    private void Start()
    {
        //최대 몬스터 갯수 초기화
        ObjectManager.Instance.MaximumEnemy = _maximumEnemy;

        //몬스터 오브젝트 생성
        for (int i = 0; i < _maximumEnemy; i++)
        {
            _enemyList.Add(ObjectManager.Instance.CreateMonster(false));
        }
       
        _minRange = (int)(transform.position.x - transform.localScale.x / 2f) * 10;
        _maxRange = (int)(transform.position.x + transform.localScale.x / 2f) * 10;

        //구독
        GameManager.Instance.AddSubscriber(this);
    }

    //몬스터 소환 시작 함수
    private void StartMonsterSpwan()
    {
        if (_monsterRoutine != null)
        {
            return;
        }
        _monsterRoutine = StartCoroutine(SpawnMonster());
    }

    //몬스터 소환 중지 함수
    private void StopMonsterSpwan()
    {
        if (_monsterRoutine != null)
        {
            StopCoroutine(_monsterRoutine);
            _monsterRoutine = null;
        }
    }
    private IEnumerator SpawnMonster()
    {
        while (true)
        {
            //몬스터 배열중 비활성화 오브젝트를 활성화 시킴
            foreach (var enemyObj in _enemyList)
            {
                if (enemyObj != null && !enemyObj.activeSelf)
                {
                    ObjectManager.Instance.GameObjectActive(enemyObj);
                    _monsterSpwanPosition = transform.position;
                    _monsterSpwanPosition.x = _rnd.Next(_minRange, _maxRange)/10f;
                    enemyObj.transform.position = _monsterSpwanPosition;
                    break;
                }
            }
            yield return _cooltimeCoroutine;
        }
    }
    private void ClearMonsterSpawn()
    {
        foreach(var enemyObj in _enemyList)
        {
            if (enemyObj.activeSelf)
            {
                ObjectManager.Instance.GameObjectDeactive(enemyObj);
            }
        }
    }

    public void PlayableNofity(bool isPlayable)
    {
        if(!isPlayable)
        {
            StopMonsterSpwan();
            ClearMonsterSpawn();
        }
        else
        {
            StartMonsterSpwan();
        }
    }
}
