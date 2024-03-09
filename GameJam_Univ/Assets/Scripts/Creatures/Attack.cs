using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Attack : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab = null;
    [SerializeField] private float fireTime = 0.6f, destroyTime = 10f;
    [SerializeField] private float range = 150;
    [SerializeField] private float power = 1;
    private GameObject target = null;
    private GameObject projectile;
    private float time = 0;
    private bool fireStarted = false;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

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
            projectile.GetComponent<Projectile>().SetPower(power);
            fireStarted = true;
            audioManager.PlaySFX(audioManager.attack);
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

    public void SetPowers(float newPower, float lifeTime, float fireRate) {
        power = newPower;
        destroyTime = lifeTime;
        fireTime = fireRate;
    }
}
