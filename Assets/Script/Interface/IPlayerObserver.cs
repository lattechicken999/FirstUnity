using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerHPObserver
{
    public void OnPlayerHpNotify(float playerHp);
}
