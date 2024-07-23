using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GhostsWander : MonoBehaviour
{
    private Vector3 _targetPos;
    private void Wander()
    {
        Vector3 currentPos = transform.position;
        float offset = 0.5f;

        float randomX = Random.Range(currentPos.x - offset, currentPos.x + offset);
        float randomY = Random.Range(currentPos.y - offset, currentPos.y + offset);
        float randomZ = Random.Range(currentPos.z - offset, currentPos.z + offset);

        if (randomY <= 0.5) randomY = 1;

        this._targetPos = new Vector3(
            randomX, randomY, randomZ
            );
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Wander", 0, 2);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, 
            this._targetPos,
            0.1f
            );
    }
}
