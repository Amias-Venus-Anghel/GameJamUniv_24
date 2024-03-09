using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Attack : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab = null;
    private GameObject target = null;
    private GameObject projectile;
    private float range = 150;
    private float time = 0;
    private float fireTime = 0.6f, destroyTime = 10f;
    private bool fireStarted = false;

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
        if (fireStarted == true)
        {
            destroyTime -= Time.deltaTime;
            if (destroyTime <= 0)
                Destroy(this.gameObject);
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
            fireStarted = true;
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
