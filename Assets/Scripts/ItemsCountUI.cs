using TMPro;
using UnityEngine;

public class ItemsCountUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    private int _itemCount = 0;

    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        Snacks.ItemCollected += OnItemCollected; 
    }

    private void OnDisable()
    {
        Snacks.ItemCollected -= OnItemCollected;
    }

    private void OnItemCollected(ItemCollectedEventArgs e)
    {
        _itemCount++; 
        UpdateUI(); 
    }

    private void UpdateUI()
    {
        _textMeshPro.text = $"{_itemCount}"; 
    }
}