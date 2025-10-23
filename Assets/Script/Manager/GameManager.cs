using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private bool _gamePlaying;
    private List<IPlayCheckOberver> subscribers;
    private IPlayTime _playTimeUI;
    private float _playStartTime;
    protected override void init()
    {
        _gamePlaying = false;
        subscribers = new List<IPlayCheckOberver>();

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name=""></param>
    public void AddSubscriber(IPlayCheckOberver subscriber)
    {
        subscribers.Add(subscriber);
    }
    public void AddSubscriber(IPlayTime subscriber)
    {
        _playTimeUI = subscriber;
    }
    public void DeleteSubscriber(IPlayCheckOberver subscriber)
    {
        subscribers.Remove(subscriber);
    }
    public void DeleteSubscriber(IPlayTime subscriber)
    {
        _playTimeUI = null;
    }

    private void Update()
    {
        if(_gamePlaying)
            _playTimeUI.PlayTimeNotify((int)(Time.time -_playStartTime));
    }
    public void GameStart()
    {
        if (_gamePlaying == false)
        {
            _playStartTime = Time.time;
            _gamePlaying = true;
            PlayerHpManager.Instance.InitPlayerHp();
            Notify();
        }
    }

    public void GameStop()
    {
        if (_gamePlaying == true)
        {
            _gamePlaying = false;
            Notify();
        }
    }

    private void Notify()
    {
        foreach (var subscriber in subscribers)
        {
            subscriber.PlayableNofity(_gamePlaying);
        }
    }
}
