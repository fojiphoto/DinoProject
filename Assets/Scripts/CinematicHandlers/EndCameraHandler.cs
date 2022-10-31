using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCameraHandler : MonoBehaviour
{
    private Transform target;
    public float upDistance;
    public float speed;

    void Start()
    {
        transform.position = LevelsManager.Instance.player.transform.position + (Vector3.up*2);
        transform.rotation = LevelsManager.Instance.player.transform.rotation;
        Camera.main.enabled = false;

        var dist = Vector3.Distance(transform.position, target.position);
        if (dist > 10)
        {
            speed *= 8;
        }
        else if (dist > 2)
        {
            speed *= 3;
        }
    }

    public void SetTarget(Transform targ)
    {
        target = targ;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position+ (Vector3.up * upDistance), Time.deltaTime * speed);
        transform.forward = Vector3.MoveTowards(transform.forward, target.position - transform.position, Time.deltaTime * speed);
    }
}