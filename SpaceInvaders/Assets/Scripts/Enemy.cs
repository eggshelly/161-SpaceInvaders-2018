using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    private int points = 0;

    private BoxCollider2D boxCollider;
    private void Awake()
    {
        boxCollider = this.GetComponent<BoxCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void init(int x)
    {
        if (x == 4)
        {
            this.points = 40;
        }
        else if (x == 3 || x == 2)
        {
            this.points = 20;
        }
        else
        {
            this.points = 10;
        }
        
    }

    public void moveSide(float x)
    {
        this.transform.position = new Vector2(this.transform.position.x + x, this.transform.position.y);
    }
    public void moveDown()
    {
        this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y-1);
    }

    public void shoot()
    {
        GameObject shot = Instantiate(bullet);
        shot.transform.position = this.transform.position;

        //shot.GetComponent<Rigidbody2D>().velocity = Vector2.down;
        Bullet b = shot.GetComponent<Bullet>();
        Physics2D.IgnoreCollision(boxCollider, shot.GetComponent<BoxCollider2D>());
    }
    public int getPoints()
    {
        return points;
    }
    public void highlight()
    {
        this.GetComponent<SpriteRenderer>().color = Color.cyan;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GameOver"))
        {
            LevelController.levelController.gameOver();
        }
    }
}
