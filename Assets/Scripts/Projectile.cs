using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed;
    public GameObject explosion;
    public Vector3 direction;

    private void Start()
    {
        direction = GameObject.FindGameObjectWithTag("Wizard").transform.forward;
    }

    void Update()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.GetComponent<Player>().TakeDamage(10);
            var explosionEffect = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(explosionEffect.gameObject, 0.8f);
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}
