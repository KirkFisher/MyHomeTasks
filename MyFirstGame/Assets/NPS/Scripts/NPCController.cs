using System.Collections;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    // Параметры персонажа
    public int health = 100;
    public float speed = 1.0f;
    public float waypointDelay = 2.0f;
    public Transform[] waypoints;
    public bool isMerchant;
    public GameObject hintUI;
    public DialogManager dialogManager;
    public Dialog dialog;

    private Transform target;
    private int waypointIndex = 0;
    private bool isWaiting = false;
    private Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();

        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogError("Waypoints not assigned for NPC: " + gameObject.name);
            return;
        }

        target = waypoints[0];
    }

    private void Update()
    {
        if (target == null)
            return;

        if (isWaiting)
        {
            animator.SetBool("isMove", false);
            return;
        }

        MoveTowardsTarget();

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            waypointIndex = (waypointIndex + 1) % waypoints.Length;
            target = waypoints[waypointIndex];
            StartCoroutine(WaitOnWaypoint());
        }
    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        animator.SetBool("isMove", true);

        if (transform.position.x < target.position.x)
            transform.localScale = new Vector3(-1, 1, 1); // Смотрит вправо
        else
            transform.localScale = new Vector3(1, 1, 1); // Смотрит влево
    }

    private IEnumerator WaitOnWaypoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waypointDelay);
        isWaiting = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hintUI.SetActive(true); // Показываем подсказку
            if (Input.GetKeyDown(KeyCode.F))
            {
                TriggerDialog();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hintUI.SetActive(false); // Скрываем подсказку
        }
    }

    public void TriggerDialog()
    {
        if (isMerchant)
        {
            dialogManager.StartDialogWithMerchant(dialog);
        }
        else
        {
            dialogManager.StartDialog(dialog);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}