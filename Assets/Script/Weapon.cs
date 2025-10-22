using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField][Range(1,15)] private int _fireingRate;

    private LinkedList<GameObject> _bulletList;
    private LinkedListNode<GameObject> bulletNode;
    private float lastFireTime = 0;
    private ObjectManager ObjectManager;

    // Start is called before the first frame update
    void Start()
    {
        ObjectManager = ObjectManager.Instance;
        _bulletList = new LinkedList<GameObject>();
        //초기 총알 인스턴스 생성
        for(int i =0; i<10;i++)
        {
            _bulletList.AddLast(ObjectManager.CreateBullet(transform.position));
            _bulletList.Last.Value.SetActive(false);
        }
        bulletNode = _bulletList.First;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            if(CheckFireRate())
            {
                bulletNode = _bulletList.CycleNext(bulletNode);
                if (bulletNode.Value.activeSelf)
                {
                    _bulletList.AddAfter(bulletNode, ObjectManager.CreateBullet(transform.position));
                    bulletNode.Next.Value.transform.position = transform.position;
                }
                else
                {
                    bulletNode.Value.transform.position = transform.position;
                    ObjectManager.GameObjectActive(bulletNode.Value);
                }
                lastFireTime = Time.fixedTime;
            }
        }
    }

    bool CheckFireRate()
    {
        return (Time.fixedTime - lastFireTime) > (1 / (float)_fireingRate);
    }
}

public static class Ex_LinkedList
{
    public static LinkedListNode<T> CycleNext<T>(this LinkedList<T> linkedList, LinkedListNode<T> thisNode)
    {
        if (thisNode.Next == null)
        {
            return linkedList.First;
        }
        else
        {
            return thisNode.Next;
        }
    }

    public static LinkedListNode<T> CyclePrevious<T>(this LinkedList<T> linkedList, LinkedListNode<T> thisNode)
    {
        if (thisNode.Previous == null)
        {
            return linkedList.Last;
        }
        else
        {
            return thisNode.Previous;
        }
    }
}