using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorMover : MonoBehaviour
{
    [SerializeField] private Transform start;
    [SerializeField] private float speed;

    private float randomX;
    private float randomY;
    [SerializeField] private int amount = 0;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(amount <= 2)
        {
            randomX = Random.Range(0f, 25f);
            randomY = Random.Range(13f, 26f);

            amount++;

            Debug.Log("HIT!");

            start.position = new Vector3(randomX, randomY, start.position.z);

            transform.position = start.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, -speed * Time.deltaTime);
    }
}
