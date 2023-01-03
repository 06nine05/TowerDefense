using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject bulletEffect;
    [SerializeField] private float speed;

    private Enemy target;
    private float atk;
    private Tower.TowerType bulletType;

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

    public void Seek(Enemy _target, float damage, Tower.TowerType type)
    {
        target = _target;
        atk = damage;
        bulletType = type;
    }

    private void Hit()
    {
        switch (bulletType)
        {
            case Tower.TowerType.A_Turret:
                target.TakeDamage(atk, bulletType);
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
                            enemy.GetComponent<Enemy>().TakeDamage(atk * 80 / 100, bulletType);
                        }
                    }
                }
                target.TakeDamage(atk, bulletType);
                break;
            case Tower.TowerType.C_Slow:
                target.Slow(35,1f);
                target.TakeDamage(atk, bulletType);
                break;
        }

        GameObject effect = Instantiate(bulletEffect, transform.position, transform.rotation);

        Destroy(effect, 1f);
        Destroy(gameObject);
    }
}
