using UnityEngine;

public class Tower : MonoBehaviour
{
    public enum TowerType
    {
        A_Turret,
        B_Cannon,
        C_Slow
    }

    [Header("Unity Setup")]

    [SerializeField] private Transform partToRotate;
    [SerializeField] private float turnSpeed;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private AudioSource audioSource;

    [Header("Attributes")]

    [SerializeField] private TowerType type;
    [SerializeField] private float range;
    [SerializeField] private float damage;
    [SerializeField] private float fireCoolDown;
    [SerializeField] private float slowEffect;
    [SerializeField] private float slowDuration;
    [SerializeField] private float aoeMod;

    private int level;
    private Enemy target;
    private float fireCountdown;
    [SerializeField] private int totalCost;

    // Start is called before the first frame update
    void Start()
    {
        if(type == TowerType.B_Cannon)
        {
            InvokeRepeating("UpdateFurthestTarget", 0f, 0.5f);
        }

        else
        {
            InvokeRepeating("UpdateTarget", 0f, 0.5f);
        }

        level = 1;
        fireCountdown = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }

        Vector3 dir = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, turnSpeed * Time.deltaTime).eulerAngles;
        partToRotate.rotation = Quaternion.Euler (0f, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = fireCoolDown;
        }

        fireCountdown -= Time.deltaTime;
    }

    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.GetComponent<Enemy>();
        }
    }

    private void UpdateFurthestTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float furthestDistance = 0;
        GameObject furthestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy > furthestDistance && distanceToEnemy <= range)
            {
                furthestDistance = distanceToEnemy;
                furthestEnemy = enemy;
            }
        }

        if (furthestEnemy != null)
        {
            target = furthestEnemy.GetComponent<Enemy>();
        }
    }

    private void Shoot()
    {
        GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletObject.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target, this);
        }

        ShootSFX();
    }

    private void ShootSFX()
    {
        switch (type)
        {
            case TowerType.A_Turret:
                SoundManager.Instance.Play(audioSource, SoundManager.Sound.turretA);
                break;
            case TowerType.B_Cannon:
                SoundManager.Instance.Play(audioSource, SoundManager.Sound.turretB);
                break;
            case TowerType.C_Slow:
                SoundManager.Instance.Play(audioSource, SoundManager.Sound.turretC);
                break;
        }
    }

    public void LevelUp()
    {
        level++;
        damage++;

        if (level == 3)
        {
            range++;
        }

        else if (level == 5)
        {
            switch (type)
            {
                case TowerType.A_Turret:
                    fireCoolDown -= 0.1f;
                    break;
                case TowerType.B_Cannon:
                    aoeMod = 100;
                    break;
                case TowerType.C_Slow:
                    slowDuration += 1;
                    break;
            }
        }
    }

    public void Addprice(int cost)
    {
        totalCost += cost;
    }

    public TowerType GetTowerType()
    {
        return type;
    }

    public int GetLvl()
    {
        return level;
    }

    public float GetAtk()
    {
        return damage;
    }

    public float GetAtkSpd()
    {
        return fireCoolDown;
    }

    public float GetRange()
    {
        return range;
    }

    public float GetAOEMod()
    {
        return aoeMod;
    }

    public float GetSlowEffect()
    {
        return slowEffect;
    }

    public float GetSlowDuration()
    {
        return slowDuration;
    }

    public int GetCost()
    {
        return totalCost;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
