 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision ObjectWeHit)
    {
        if(ObjectWeHit.gameObject.CompareTag("Target"))
        {
            print("HIT " + ObjectWeHit.gameObject.name + "!");

            CreateBulletImpactEffect(ObjectWeHit);
            
            Destroy(gameObject);
        }

        if(ObjectWeHit.gameObject.CompareTag("Wall"))
        {
            print("HIT a Wall");

            CreateBulletImpactEffect(ObjectWeHit);

            Destroy(gameObject);
        }

    }

    void CreateBulletImpactEffect(Collision ObjectWeHit)
    {
        ContactPoint contact = ObjectWeHit.contacts[0];
    }
}
