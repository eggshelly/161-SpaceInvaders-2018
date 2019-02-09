using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;   //to move , for win condition

public class EnemyController : MonoBehaviour
{
    public static EnemyController enemyController;
    [SerializeField] private GameObject enemy;
    //[SerializeField] private int width = 11;
    //[SerializeField] private int height = 5;
    private bool moveRight = true;
    private float move_time = 0f;
    private float shoot_time = 0f;
    private float rng_shoot_time;// = Random.Range(0.3f,1.5f);

    private GameObject[,] enemyList = new GameObject[11, 5];
    private void Awake()
    {
        enemyController = this;
        //Debug.Log(enemyList.GetLength(0));
        generate();
        rng_shoot_time = Random.Range(.3f, 3f);
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        /*int i = 5;
        int j = 0;

        Vector2 move = moveRight ? Vector2.right : Vector2.left;
        RaycastHit2D hitInfo = Physics2D.Raycast(enemyList[i, j].transform.position, move, 1f);
        Debug.DrawRay(enemyList[i, j].transform.position, move, Color.magenta, 1f);
        Debug.Log(hitInfo.collider.tag);*/ //needed to turn off queries start in collider in proj settings
        float t = Time.deltaTime;
        move_time += t;
        shoot_time += t;
        if (move_time > 2)
        {
            move();
            move_time = 0f;
        }
        if(shoot_time > rng_shoot_time)
        {
            shoot();
            //Debug.Log("enemy shoot now");
            rng_shoot_time = Random.Range(.3f, 3f);
            shoot_time = 0;
        }

    }

    public void generate()
    {
        for(int i = 0; i < enemyList.GetLength(0); i++)
        {
            for(int j=0; j < enemyList.GetLength(1); j++)
            {
                Vector2 spawn = new Vector2(i, j);
                GameObject insta = Instantiate(enemy, this.transform);
                insta.transform.localPosition = spawn;
                Enemy temp = insta.GetComponent<Enemy>();
                temp.init(j);
                enemyList[i, j] = insta;
            }
        }
    }

    public void move()
    {
        if (canMoveSide())
        {
            float move = moveRight ? 0.3f : -.3f;
            this.transform.position = new Vector2(this.transform.position.x +move, this.transform.position.y);
        }
        else
        {
            this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 1);
        }
    }
    public bool canMoveSide()
    {
        Vector2 move = moveRight ? Vector2.right : Vector2.left;
        for (int i = 0; i < enemyList.GetLength(0); i++)
        {
            for (int j = 0; j < enemyList.GetLength(1); j++)
            {
                if(enemyList[i, j]!= null)
                {
                    RaycastHit2D hitInfo = Physics2D.Raycast(enemyList[i, j].transform.position, move, 1.5f);
                    //Debug.DrawRay(enemyList[i, j].transform.position, move, Color.magenta, 1f);
                    if (hitInfo && hitInfo.collider.CompareTag("Wall"))
                    {
                        moveRight = !moveRight;
                        return false;
                    }
                }
                
            }
        }
        return true;
    }

    public bool win()
    {
        for (int i = 0; i < enemyList.GetLength(0); i++)
        {
            for (int j = 0; j < enemyList.GetLength(1); j++)
            {
                if (enemyList[i, j] != null)
                {
                    //Debug.Log(i + " " + j);
                    return false;
                }
                    
            }
        }
        return true;
    }
    public void shoot()
    {
        while (true)
        {
            int col = Random.Range(0, enemyList.GetLength(0)+1);

            int row = getBottomIndex(col);

            if(row != -1 && enemyList[col,row] != null)
            {
                //Debug.Log(col+" "+row);
                enemyList[col, row].GetComponent<Enemy>().shoot();
                //enemyList[col, row].GetComponent<Enemy>().highlight();
                return;
            }
                
        }
    }
    public int getBottomIndex(int col)
    {
        for(int i = 0; i< enemyList.GetLength(1); i++)
        {
            try
            {
                if (enemyList[col, i] != null)
                    return i;
            }
            catch
            {
                return -1;
            }

        }
        return -1;
    }
}
 