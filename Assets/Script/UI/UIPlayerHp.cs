
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHp : MonoBehaviour, IPlayerHPObserver,IPlayCheckOberver
{
    //[SerializeField] private Image _imgHpBar;
    [SerializeField] Transform _playerTransform;
    [SerializeField] Vector3 _hpBarGap;
    private PlayerHpManager _PlayerHpManager;
    private Image _Image;
    private Camera _camera;
    private Transform _parent;

    private void Awake()
    {
        _Image = GetComponent<Image>();
        if (_camera == null )
        {
            _camera = Camera.main;
        }
        if (_parent == null)
        {
            _parent = transform.parent;
        }
    }
    private void Start()
    {
        PlayerHpManager.Instance.AddSubscriber(this);
        GameManager.Instance.AddSubscriber(this);
        ObjectManager.Instance.GameObjectDeactive(_parent.gameObject);
    }
    public void OnPlayerHpNotify(float playerHp)
    {
        _Image.fillAmount = playerHp;
    }
    private void Update()
    {
        _parent.position = _camera.WorldToScreenPoint(_playerTransform.position) + _hpBarGap;
    }

    public void PlayableNofity(bool isPlayable)
    {
        if (isPlayable)
        {
            ObjectManager.Instance.GameObjectActive(_parent.gameObject);
        }
        else
        {
            ObjectManager.Instance.GameObjectDeactive(_parent.gameObject);
        }
    }
}
