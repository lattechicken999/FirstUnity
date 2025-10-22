using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHpManager : Singleton<PlayerHpManager>
{
    [SerializeField] private float _playerMaxHp;
    private float  _playerCurHp;
    List<IPlayerHPObserver> subscribers;


    protected override void init()
    {
        _playerCurHp = _playerMaxHp;
        subscribers = new List<IPlayerHPObserver>();
    }

    public void AddSubscriber(IPlayerHPObserver subscriber)
    {
        subscribers.Add(subscriber);
    }
    public void DeleteSubscriber(IPlayerHPObserver subscriber)
    {
        subscribers.Remove(subscriber);
    }

    public void OnTakeDamage(float damage)
    {
        _playerCurHp -=  damage;
        if (_playerCurHp <=0)
        {
            _playerCurHp = 0;
            GameManager.Instance.GameStop();
        }
        UpdatePlayerHP();
    }
    public void InitPlayerHp()
    {
        _playerCurHp = _playerMaxHp;
        UpdatePlayerHP();
    }

    private void UpdatePlayerHP()
    {
        foreach (var subscriber in subscribers)
        {
            subscriber.OnPlayerHpNotify(_playerCurHp/ _playerMaxHp);
        }
    }

}
