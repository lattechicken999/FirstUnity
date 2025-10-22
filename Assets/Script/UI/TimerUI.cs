using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimerUI : MonoBehaviour,IPlayTime
{
    //private Component _textComponent;
    private TextMeshProUGUI _textMeshPro;

    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }
    public void PlayTimeNotify(int playingTimeSec)
    {
        _textMeshPro.text = $"Play Time : {playingTimeSec/60:D2}:{(int)playingTimeSec % 60:D2}";
        
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.AddSubscriber(this);
    }

}
