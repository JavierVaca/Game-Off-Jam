using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System;

public class Troop : MonoBehaviour, ITroop
{
    [SerializeField]
    private int _health;
    public int Health { get{return _health; } set{_health = value;} }
    
    [SerializeField]
    private int _damage;
    public int Damage { get; set; }
    
    [SerializeField]
    private float _viewRange;
    public float ViewRange { get; set; }

     [SerializeField]
    private int _attackRange;
    public float AttackRange { get; set; }

     [SerializeField]
    private int _speed;
    public float Speed { get; set; }

    public Image selected;
    private NavMeshAgent navAgent;

    public void Attack()
    {
        throw new System.NotImplementedException();
    }

    public bool ChangeHealth(int i)
    {
        throw new System.NotImplementedException();
    }

    public bool Die()
    {
        throw new System.NotImplementedException();
    }

    public void Move(Vector3 point)
    {
        navAgent.SetDestination(point);
    }

    public void ToggleSelectionVisual(bool select)
    {
        Debug.Log(select);
        if(select)
        {
            selected.gameObject.SetActive(true);
            
        }
        else if(!select)
        {
            selected.gameObject.SetActive(false);
            
        }
        
    }
    
    internal void Initialize()
    {
        navAgent = GetComponent<NavMeshAgent>();
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
