using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    GameObject target = null;
    float speed = 0.3f;
    float range;
    Vector3 initiPos;
    public float distance;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            Destroy(this.gameObject);
        else
        {
            if (TargetInRange() == false)
                Destroy(this.gameObject);

            if (transform.position != target.transform.position)
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);

            if (Mathf.Abs(transform.position.x - target.transform.position.x) <= 0.05f &&
                Mathf.Abs(transform.position.y - target.transform.position.y) <= 0.05f)
                Destroy(this.gameObject);
        }
    }

    bool TargetInRange()
    {
        distance = Vector2.Distance(initiPos, target.transform.position);
        if (Vector2.Distance(initiPos, target.transform.position) <= range)
            return true;
        return false;
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    public void SetRange(float range)
    {
        this.range = range;
    }

    public void SetInitPos(Vector3 pos)
    {
        this.initiPos = pos;
    }
}
