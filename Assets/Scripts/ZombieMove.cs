using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMove : MonoBehaviour
{
    private Rigidbody2D rb;

    public float minY = -10f;
    public float moveSpeed;
    public float maxX;

    public float dropChance = 0.3f;        
    public float dropForce = 5f;           
    public float dropCooldown = 1.5f;      
    private float dropTimer = 0f;

    public float jumpChance = 0.5f;
    public float jumpForce = 5f;
    public float jumpCooldown = 1.5f;
    private float jumpTimer = 0f;

    public float stuckTimeThreshold = 1.5f; 
    private float stuckTimer = 0f;
    public float horizontalSpeedThreshold = 0.1f;



    public string normalLayer = "";
    public string otherLayer = "JumpingZombie";

    public List<GameObject> ls = new List<GameObject>(); 

    public bool truckCol = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!truckCol && transform.position.x > maxX)
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);

        else
            rb.velocity = new Vector2(0, rb.velocity.y);

        if (dropTimer > 0)
            dropTimer -= Time.deltaTime;
        if (jumpTimer > 0)
            jumpTimer -= Time.deltaTime;


        if (Mathf.Abs(rb.velocity.x) < horizontalSpeedThreshold)
            stuckTimer += Time.deltaTime;

        else
            stuckTimer = 0f;

        if (stuckTimer >= stuckTimeThreshold && dropTimer <= 0)
        {
            if (Random.value < dropChance)
            {
                DropDown();
                dropTimer = dropCooldown;
            }
        }

        if (!truckCol && jumpTimer <= 0 && ls.Count > 0)
        {
            foreach (GameObject enemy in ls)
            {
                if (enemy != null && enemy.transform.position.x > transform.position.x)
                {
                    if (Random.value < jumpChance)
                    {
                        Jump();
                        jumpTimer = jumpCooldown;
                        break;
                    }
                }
            }
        }

        
        if (transform.position.y < minY && rb.velocity.y < 0)
        {
            transform.position = new Vector3(transform.position.x, minY, transform.position.z);
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    void DropDown()
    {
        gameObject.layer = LayerMask.NameToLayer(otherLayer);
        rb.AddForce(Vector2.down * dropForce, ForceMode2D.Impulse);
        Invoke("RollbackLayer", 0.2f);
    }

    void RollbackLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(normalLayer);
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Hero"))
            truckCol = true;

        else if (col.transform.CompareTag("Enemy"))
            ls.Add(col.gameObject);
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.transform.CompareTag("Hero"))
            truckCol = false;

        else if (col.transform.CompareTag("Enemy"))
            ls.Remove(col.gameObject);
    }
}
