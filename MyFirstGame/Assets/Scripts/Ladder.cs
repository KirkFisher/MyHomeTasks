using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private Vector2 _vertMove;

    [SerializeField] private float _speed = 5;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.W))
            {
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, _speed);
            }
        } else if (Input.GetKey(KeyCode.S))
        {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -_speed);
        }
        else
        {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
}
