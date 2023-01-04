using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType
    {
        A,
        B,
        C
    }

    [SerializeField] private float maxHp;
    [SerializeField] private float speed;
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private int moneyGain;

    private float currentHp;
    private float currentSpeed;
    private Waypoints waypoints;
    private int waypointIndex;

    private bool isSlow;
    private float slowDuration;

    public delegate void HealthChangedDelegate(float currentHealth, float maxHealth);

    public event HealthChangedDelegate EventHealthChanged;

    // Start is called before the first frame update
    void Start()
    {
        StatBuff();
        isSlow = false;
        waypoints = GameObject.FindGameObjectWithTag("Waypoints").GetComponent<Waypoints>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();

        if (isSlow)
        {
            slowDuration -= Time.deltaTime;
        }

        if (slowDuration <= 0f)
        {
            currentSpeed = speed;
            isSlow = false;
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints.waypoints[waypointIndex].position, currentSpeed * Time.deltaTime);

        Vector3 direction = (waypoints.waypoints[waypointIndex].position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        if (transform.position == waypoints.waypoints[waypointIndex].position)
        {
            if (waypointIndex == waypoints.waypoints.Length - 1)
            {
                EndPath();
            }

            else
            {
                waypointIndex++;
            }
        }
    }

    private void EndPath()
    {
        PlayerStat.life--;
        Destroy(gameObject);
    }

    private void StatBuff()
    {
        maxHp = maxHp * (100 + (0.5f * (Spawn.Instance.GetWave() - 1))) / 100;
        speed = speed * (100 + (0.5f * (Spawn.Instance.GetWave() - 1))) / 100;

        currentHp = maxHp;
        currentSpeed = speed;
    }

    public void TakeDamage(float damage, Tower.TowerType type)
    {
        switch (type)
        {
            case Tower.TowerType.A_Turret:
                if (enemyType == EnemyType.A)
                {
                    damage = damage * 1.5f;
                }
                break;
            case Tower.TowerType.B_Cannon:
                if (enemyType == EnemyType.B)
                {
                    damage = damage * 1.5f;
                }
                break;
            case Tower.TowerType.C_Slow:
                if (enemyType == EnemyType.C)
                {
                    damage = damage * 1.5f;
                }
                break;
        }

        currentHp -= damage;

        EventHealthChanged?.Invoke(currentHp, maxHp);

        if (currentHp <= 0)
        {
            Die();
        }
    }

    public void Slow(float effect, float duration)
    {
        if (isSlow)
        {
            slowDuration = duration;
        }

        currentSpeed = (speed * (100 - effect)) / 100;
        slowDuration = duration;

        isSlow = true;
    }

    private void Die()
    {
        PlayerStat.Money += moneyGain;
        Destroy(gameObject);
    }
}
