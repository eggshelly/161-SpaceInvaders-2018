using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;   //to tell the player hey you can shoot again
//if an enemy dies, check for win...

public class Bullet : MonoBehaviour
{

    private Transform trans;
    [SerializeField] private float speed;

    public UnityEvent playerBulleteCollide = new UnityEvent();


    private void Awake()
    {
        trans = this.GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //speed = this.CompareTag("PlayerBullet") ? speed: -1 * speed;
        trans.Translate(new Vector2(0,speed*Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
 
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
            if(this.CompareTag("PlayerBullet"))
                playerBulleteCollide.Invoke();
        }
        if (this.CompareTag("PlayerBullet") && other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            playerBulleteCollide.Invoke();
        }
        else if (this.CompareTag("EnemyBullet") && other.CompareTag("Player"))
        {
            //life--
            Destroy(gameObject);
        }
    }
   
}
