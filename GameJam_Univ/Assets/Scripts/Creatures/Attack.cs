using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Attack : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    private GameObject target = null;
    private GameObject projectile;
    private float range = 150;
    float time = 0;
    float fireTime = 0.6f;

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            FindTarget();
        } 
        else
        {
            if (TargetInRange(target) == false)
                target = null;

            time += Time.deltaTime;
            if (time > fireTime)
            {
                time = 0;
                SpawnProjectile();

            }
        }
    }

    void SpawnProjectile()
    {
        projectile = Instantiate(projectilePrefab);
        projectile.transform.SetParent(transform.parent);
        projectile.transform.position = transform.position;
        if(target != null)
        {
            projectile.GetComponent<Projectile>().SetTarget(target);
            projectile.GetComponent<Projectile>().SetRange(range);
            projectile.GetComponent<Projectile>().SetInitPos(transform.position);
        }
    }

    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++) {
            if (TargetInRange(enemies[i]) == true)
                target = enemies[i];
        }
    }

    bool TargetInRange(GameObject Target)
    {
        return (Vector2.Distance(Target.transform.position, transform.position) <= range);
    }
}
