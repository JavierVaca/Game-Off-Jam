using UnityEngine;

public interface ITroop 
{
    int CurrentHealth { get; set; }
    int MaxHealth {get; set; }
    int Damage { get; set; }
    float ViewRange { get; set; }
    float AttackRange { get; set; }
    float Speed { get; set; }

    void Move(Vector3 point);
    void Attack();
    void ChangeHealth(int i);
    bool Die();
    void ToggleSelectionVisual(bool v);
}