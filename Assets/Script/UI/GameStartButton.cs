using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartButton : MonoBehaviour,IPlayCheckOberver
{
    private GameManager gameManager;
    private void Start()
    {
        GameManager.Instance.AddSubscriber(this);
    }
    public void PlayableNofity(bool isPlayable)
    {
        if (isPlayable)
        {
            ObjectManager.Instance.GameObjectDeactive(gameObject);
        }
        else
        {
            ObjectManager.Instance.GameObjectActive(gameObject);
        }
    }
}
