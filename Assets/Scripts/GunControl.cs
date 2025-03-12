using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    public GameObject bulletPrefaps;
    public int createBulletCount;
    public float bulletSpeed;
    public float spreadAngle = 15f;
    void Update()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        Vector3 direction = mouseWorldPosition - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 30));

        if(Input.GetMouseButtonDown(0))
        {
            ShotBullet();
        }
    }

    void ShotBullet()
    {
        for (int i = 0; i < createBulletCount; i++)
        {
            float randomAngle = Random.Range(-spreadAngle, spreadAngle);
            Quaternion bulletRotation = Quaternion.Euler(0f, 0f, transform.eulerAngles.z + randomAngle + 30);

            GameObject bullet = Instantiate(bulletPrefaps, transform.position, bulletRotation);
            bullet.GetComponent<BulletControl>().HeroGame = gameObject;

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = bullet.transform.right * bulletSpeed;
            }
        }
    }
}
