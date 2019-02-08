using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float speed = 2f;
    [SerializeField] private int maxLife = 3;
    private int life;// = maxLife;

    //public UnityEvent playerBulleteCollide = new UnityEvent();

    private bool bulletExists = false;

    private Rigidbody2D rgb2D;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        rgb2D = this.GetComponent<Rigidbody2D>();
        boxCollider = this.GetComponent<BoxCollider2D>();
        life = maxLife;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");
        //this.transform.Translate(Vector2.right * move * speed * Time.deltaTime);
        rgb2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, 0);

        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space))
        {    
            shoot();
        }
    }


    public void shoot()
    {
        if (!bulletExists)
        {
            GameObject shot = Instantiate(bullet, this.transform.position, Quaternion.identity);
            //shot.GetComponent<Rigidbody2D>().velocity = Vector2.up;
            Bullet b= shot.GetComponent<Bullet>();
            Physics2D.IgnoreCollision(boxCollider, shot.GetComponent<BoxCollider2D>());
            b.playerBulleteCollide.AddListener(bulletAvaliable);
            bulletExists = true;
        }
       
    }

    public void bulletAvaliable()
    {
        bulletExists = false;
    }
    public void onHit()
    {
        life--;
    }
}
