using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Box : MonoBehaviour, IDamageble
{
    [SerializeField] private int health;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    
    [SerializeField] private GameObject[] _collectibles;
    
    public int Health { get; set; }

    public void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Start()
    {
        Health = health;
    }


    private IEnumerator VisualDamageEffect()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        _spriteRenderer.color = Color.white;
    }

    public void GetDamage(int value)
    {
        if (Health > 0)
        {
            Health -= value;
            StartCoroutine(VisualDamageEffect());
            CheckHealth();
        }
    }

    public void CheckHealth()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnDestroy()
    {
        Instantiate(_collectibles[Random.Range(0, _collectibles.Length - 1)], gameObject.transform.position, Quaternion.identity);
    }
}
