using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamagable>(out IDamagable damagable))
        {
            damagable.TakeDamage(_damage);
        }

        gameObject.SetActive(false);
    }
}
