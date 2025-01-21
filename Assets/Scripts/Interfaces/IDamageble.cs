using UnityEngine;

public interface IDamageble
{
    public int Health { get; set; }
    
    public void GetDamage(int value);

    public void CheckHealth();

}
