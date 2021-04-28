using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Weapon currentWeapon;
    public float speed;
    public GameObject projectile;

    [SerializeField] private int health;
    [SerializeField] private float xBounds = 40f;
    [SerializeField] private float yBounds = 20f;

    private Animator anim;
    private Rigidbody2D playerBody;
    private Vector2 movement;
    private Vector2 mousePos;

    private float nextTimeOfFire = 0;

    bool hit = true;

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Changes the default weapon sprite to the current one
        transform.GetChild(4).GetComponent<SpriteRenderer>().sprite = currentWeapon.currentWeaponSpr;
    }

    void Update()
    {

        if (health > 0)
        {
            if (Input.GetMouseButton(0))
            {
                if (Time.time >= nextTimeOfFire)
                {
                    currentWeapon.Shoot();
                    nextTimeOfFire = Time.time + 1 / currentWeapon.fireRate;
                }
            }

            Rotation();
        }

        // Hard-coded map boundries of the Player
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -xBounds, xBounds), Mathf.Clamp(transform.position.y, -yBounds, yBounds));
    }

    void FixedUpdate()
    {
        if (health > 0)
        {
            Movement();
        }
    }

    void Rotation()
    {
        // Sets the Player to face the direction of the mouse pointer
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10 * Time.deltaTime);
    }

    void Movement()
    {
        // Basic movement
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movement = moveInput.normalized * speed;
        playerBody.MovePosition(playerBody.position + movement * Time.fixedDeltaTime);
    }

    IEnumerator HitBoxOff()
    {
        hit = false;
        anim.SetTrigger("Hit");
        yield return new WaitForSeconds(1.5f);
        hit = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            if (hit)
            {
                health--;

                if (GameObject.Find("Health Bar").transform.GetChild(0).gameObject && health > 0)
                {
                    Destroy(GameObject.Find("Health Bar").transform.GetChild(0).gameObject);
                }

                if (health > 0)
                {
                    StartCoroutine(HitBoxOff());
                }

                if (health <= 0)
                {
                    StartCoroutine(Death());
                }
            }
        }
    }

    IEnumerator Death()
    {
        // Plays a death animation and resets the current scenes
        anim.SetBool("NoHealth", true);
        yield return new WaitForSecondsRealtime(4);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
