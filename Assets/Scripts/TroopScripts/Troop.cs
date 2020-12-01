using UnityEngine;
using UnityEngine.AI;

public class Troop : MonoBehaviour, ITroop
{
    private int _currentHealth;
    public int CurrentHealth { get{ return _currentHealth; } set{ _currentHealth = value;} }

    [SerializeField]
    private int _maxHealth;
    public int MaxHealth { get{ return _maxHealth; } set{ _maxHealth = value;} }
    
    [SerializeField]
    private int _damage;
    public int Damage { get{ return _damage; } set{ _damage = value;} }
    
    [SerializeField]
    private float _viewRange;
    public float ViewRange { get; set; }

    [SerializeField]
    private int _attackRange;
    public float AttackRange { get; set; }

     [SerializeField]
    private int _speed;
    public float Speed { get{ return _speed; } set{ _maxHealth = _speed; }  }

    public MeshRenderer selected;
    public HealthBar healthBar;
    public Laser laser;
    internal NavMeshAgent navAgent;
    internal Troop enemyTarget;
    private bool Dead;
    internal bool attacking;

    public void Attack()
    {
        if(enemyTarget != null && !enemyTarget.Dead)
        {
            enemyTarget.ChangeHealth(Damage);
            var copyLaser = Instantiate(laser, transform.position, transform.rotation);
            copyLaser.target = enemyTarget.transform;
        }
        else
        {
            attacking = false;
            Debug.Log("failed attack");
            CancelInvoke();
        }
    }

    public void ChangeHealth(int i)
    {
        //Debug.Log("Health changed from: " + CurrentHealth);
        CurrentHealth -= i;
        //Debug.Log("tothis" + CurrentHealth);
        healthBar.UpdateDamage(CurrentHealth, MaxHealth);
    }

    public bool Die()
    {
        if(CurrentHealth <= 0)
        {
            gameObject.SetActive(false);
            return true;
        }
        return false;
    }

    public virtual void Move(Vector3 point)
    {
        navAgent.SetDestination(point);
    }

    public void ToggleSelectionVisual(bool select)
    {
        if(select)
        {
            selected.enabled = true;
            healthBar.SetVisible(true);
            
        }
        else if(!select)
        {
            selected.enabled = false;
            healthBar.SetVisible(false);
        }
        
    }


     // Start gets overriden by child use initialize
    internal void Initialize()
    {
        navAgent = GetComponent<NavMeshAgent>();
        CurrentHealth = MaxHealth;
    }

   

    // Update is called once per frame
    internal void BaseUpdate()
    {
       Dead = Die();
    }

    void OnTriggerExit(Collider other) {
        if(enemyTarget != null)
        {
            if(other.tag == "Enemy" && other.gameObject == enemyTarget.gameObject)
            {
                enemyTarget = null;
                attacking = false;
                Debug.Log("out of range");
                CancelInvoke();
            }    
        }
    }

    void OnTriggerStay(Collider other) {
         if(!other.isTrigger && other.gameObject.tag == "Enemy" && !attacking)
        {
            Troop enemy; 
            other.gameObject.TryGetComponent<Troop>(out enemy);
            if(enemy != null)
            {
                enemyTarget = enemy;
                attacking = true;
                transform.LookAt(other.gameObject.transform);
                Debug.Log(this + " is attacking" + other.gameObject);
                InvokeRepeating("Attack", 0f, 2f);
            }
        }  
    }
}
