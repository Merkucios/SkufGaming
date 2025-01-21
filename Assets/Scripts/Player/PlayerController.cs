using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Transform _transform;
    private Vector2 _inputVector;
    private Animator _animator;
    
    [Header("GameInput элемент")]
    [SerializeField] private InputReader _inputReader;
    
    [Header("Настройки персонажа")]
    [SerializeField] private float speedMultiplier = 10f;
    [SerializeField] private float jumpMultiplier = 5f;
    
    [Header("Оружие")]    
    [SerializeField] private Transform _bulletPosTransform;
    [SerializeField] private GameObject _bulletPrefab;
    
    
    // Animation conditions
    private static readonly int RunAnim = Animator.StringToHash("Run");
    private static readonly int JumpAnim = Animator.StringToHash("Jump");
    private static readonly int AttackAnim = Animator.StringToHash("Attack");
    
    [Header("Настройки прыжка")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 0.1f;

    [Header("Подобранные предметы")]
    [SerializeField] private int _itemCount = 0;

    [Header("Звуки")] 
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _soundItemCollect; 

    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _inputReader.EnableGameplayInput();
    }

    private void OnEnable()
    {
        _inputReader.MoveEvent += OnMove;
        _inputReader.AttackEvent += OnAttack;
        _inputReader.JumpEvent += OnJump;Snacks.ItemCollected += OnItemCollected;
    }

    private void OnDisable()
    {
        _inputReader.MoveEvent -= OnMove;
        _inputReader.AttackEvent -= OnAttack;
        _inputReader.JumpEvent -= OnJump;Snacks.ItemCollected -= OnItemCollected;
    }

    private void OnJump()
    {
        if (IsGrounded())
        {
            _rigidbody.AddForce(new Vector2(0, jumpMultiplier * 300), ForceMode2D.Impulse);
            _animator.SetTrigger(JumpAnim);
        }
    }
    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(_transform.position, Vector2.down, groundCheckDistance, groundLayer);
        return hit.collider != null;
    }
    private void OnMove(Vector2 direction)
    {
        _inputVector = new Vector2(direction.x, 0).normalized;
        _animator.SetFloat(RunAnim, Mathf.Abs(_inputVector.x));
    }

    private void OnAttack()
    {
        _animator.SetTrigger(AttackAnim);
        GameObject bullet = Instantiate(_bulletPrefab, _bulletPosTransform.position, Quaternion.identity);

        Bullet script = bullet.GetComponent<Bullet>();

        if (Input.GetKey(KeyCode.W))
        {
            script.Launch(Vector2.up);
        }
        else
        {
            script.Launch(-_transform.right);
        }
        


    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 movement = speedMultiplier * Time.fixedDeltaTime * _inputVector ;
        _rigidbody.MovePosition(_rigidbody.position + movement);
        
        if (_inputVector.x != 0)
        {
            float angle = _inputVector.x > 0 ? 180 : 0; 
            _transform.rotation = new Quaternion(0, angle, 0, 0);
        }
    }
    
    private void OnItemCollected(ItemCollectedEventArgs e)
    {
        _audioSource.clip = _soundItemCollect;
        _audioSource.Play();
        _itemCount++;
        Debug.Log($"Предмет собран: {e.ItemName}. Всего предметов: {_itemCount}");
    }
}
