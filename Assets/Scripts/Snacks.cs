using UnityEngine;
using System;

public class Snacks : MonoBehaviour, ICollectible
{
    public static event Action<ItemCollectedEventArgs> ItemCollected;
    public string itemName;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollect();
        }
    }

    public void OnCollect()
    {
        ItemCollected?.Invoke(new ItemCollectedEventArgs(itemName));
        
        Destroy(gameObject);
    }
}
