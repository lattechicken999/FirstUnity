using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : MonoBehaviour,IOpserver
{
    [SerializeField] private subject _subject;
    public void OnNotify()
    {
        Debug.Log($"{gameObject.name} Received");
    }

    private void Awake()
    {
        _subject?.AddObserveer( this );
    }

    private void OnDestroy()
    {
        _subject?.ReemoveObserver( this );
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
