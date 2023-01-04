using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject bulletEffect;
    [SerializeField] private float speed;

    private Enemy target;
    private Tower tower;

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.transform.position - transform.position;
        float distance = speed * Time.deltaTime;

        if (dir.magnitude <= distance)
        {
            Hit();
            return;
        }

        transform.Translate(dir.normalized * distance, Space.World);
    }

    public void Seek(Enemy _target, Tower _tower)
    {
        target = _target;
        tower = _tower;
    }

    private void Hit()
    {
        switch (tower.GetTowerType())
        {
            case Tower.TowerType.A_Turret:
                target.TakeDamage(tower.GetAtk(), tower.GetTowerType());
                break;
            case Tower.TowerType.B_Cannon:
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

                foreach (GameObject enemy in enemies)
                {
                    if (enemy.GetComponent<Enemy>() != target)
                    {
                        float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

                        if (distanceToEnemy <= 1f)
                        {
                            enemy.GetComponent<Enemy>().TakeDamage(tower.GetAtk() * tower.GetAOEMod() / 100, tower.GetTowerType());
                        }
                    }
                }
                target.TakeDamage(tower.GetAtk(), tower.GetTowerType());
                break;
            case Tower.TowerType.C_Slow:
                target.Slow(tower.GetSlowEffect(),tower.GetSlowDuration());
                target.TakeDamage(tower.GetAtk(), tower.GetTowerType());
                break;
        }

        GameObject effect = Instantiate(bulletEffect, transform.position, transform.rotation);

        Destroy(effect, 1f);
        Destroy(gameObject);
    }
}
