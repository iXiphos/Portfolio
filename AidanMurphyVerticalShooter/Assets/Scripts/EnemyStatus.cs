﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public float health = 10;

    public int size = 0;

    float speed = 100;

    float boundsLeft = -4.5f;
    float boundsRight = 4.5f;
    float boundsDown = -11f;

    public GameObject manager;

    public GameObject drop;

    // Start is called before the first frame update
    void Start()
    {
        switch (size)
        {
            case 0: //Small
                gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                health = 5f;
                speed = 300;
                break;
            case 1: //Medium
                gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
                health = 11f;
                speed = 250;
                break;
            case 2: //Large
                gameObject.transform.localScale = new Vector3(2f, 2f, 1f);
                health = 17f;
                speed = 200;
                break;
        }
        gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.up * speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            int rand3 = Random.Range(0, 4);
            if (size == 1)
            {
                GameObject enemy = Instantiate(gameObject, new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y - 0.5f), Quaternion.Euler(0, 0, 10));
                enemy.GetComponent<SpriteRenderer>().sprite = manager.GetComponent<GameManager>().meteorSprites[rand3];
                manager.GetComponent<GameManager>().AddEnemy(enemy);
                enemy.GetComponent<EnemyStatus>().size = 0;
                GameObject enemy1 = Instantiate(gameObject, new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y - 0.5f), Quaternion.Euler(0, 0, -10));
                enemy1.GetComponent<SpriteRenderer>().sprite = manager.GetComponent<GameManager>().meteorSprites[rand3];
                manager.GetComponent<GameManager>().AddEnemy(enemy1);
                enemy1.GetComponent<EnemyStatus>().size = 0;
            }
            else if(size == 2)
            {
                GameObject enemy = Instantiate(gameObject, new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y - 0.5f), Quaternion.Euler(0, 0, 15));
                manager.GetComponent<GameManager>().AddEnemy(enemy);
                enemy.GetComponent<EnemyStatus>().size = 1;
                enemy.GetComponent<SpriteRenderer>().sprite = manager.GetComponent<GameManager>().meteorSprites[rand3];
                GameObject enemy1 = Instantiate(gameObject, new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y - 0.5f), Quaternion.Euler(0, 0, -15));
                manager.GetComponent<GameManager>().AddEnemy(enemy1);
                enemy1.GetComponent<EnemyStatus>().size = 1;
                enemy1.GetComponent<SpriteRenderer>().sprite = manager.GetComponent<GameManager>().meteorSprites[rand3];
            }
            manager.GetComponent<GameManager>().RemoveEnemy(gameObject);
            dropItem();
            Destroy(gameObject);
        }
    }

    private void dropItem()
    {
        int rand = Random.Range(0, 101);
        if(rand > 90)
        {
            rand = Random.Range(0, 2);
            GameObject upgrade = Instantiate(drop, transform.position, Quaternion.Euler(0, 0, 0));
            upgrade.GetComponent<UpgradeWeapon>().newWeapon = rand;
            upgrade.GetComponent<Rigidbody2D>().AddForce(-transform.up * 50);
            Destroy(gameObject);
        }

    }

    private void FixedUpdate()
    {
        movement();
    }

    void movement()
    {
        if (transform.position.x < boundsLeft)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            transform.Rotate(-gameObject.transform.rotation.eulerAngles);
            gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.up * speed);
        }
        else if (transform.position.x > boundsRight)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            transform.Rotate(-gameObject.transform.rotation.eulerAngles);
            gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.up * speed);
        }

        if (transform.position.y < boundsDown)
        {
            manager.GetComponent<GameManager>().RemoveEnemy(gameObject);
            Destroy(gameObject); 
        }
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().Health--;
            Destroy(gameObject);
        }
        //else if(collision.gameObject.tag == "Enemy")
        //{
        //    gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        //    transform.Rotate(-gameObject.transform.rotation.eulerAngles);
        //    gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.up * speed);
        //}
    }
}
