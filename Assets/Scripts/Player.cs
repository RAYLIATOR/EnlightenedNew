using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Image bloodOnScreenEffect;
    public GameObject muzzleFlash;
    Animator anim;
    public Transform firePoint;
    float fireRate;
    float nextTimeToFire;
    AudioSource playerAs;
    public Image healthBar;
    //public Image powerBar;
    Material defaultMat;
    //public Material stealthMat;
    Renderer rend;
    bool canShoot;
    bool canMove;
    //bool stealth;
    public Camera mainCamera;
    public Camera statCamera;
    bool isPaused;
    public static Vector3 pointToLookAt;
    public GameObject powerShot;
    public float playerHealth;
    //public float playerPower;
    float maxHealth;
    //float maxPower;
    public float maxForce;
    public float maxSpeed;
    public Light playerGlow;
    public Light playerTorch;
    Rigidbody playerRb;
    float timer;

    void Start ()
    {
        timer = 0.3f;
        anim = GetComponent<Animator>();
        playerAs = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();
        fireRate = 10;
        nextTimeToFire = 0;
        playerHealth = 200f;
        maxHealth = 200f;
        //playerPower = 100f;
        canMove = true;
        canShoot = true;
        rend = GetComponent<Renderer>();
        //playerGlow.intensity = (playerPower * 3) / 100;
        //Mathf.Clamp(playerPower, 0, maxPower);
    }

    void Update()
    {
        //Dash();
        healthBar.fillAmount = playerHealth / 200;
        bloodOnScreenEffect.color = new Color(1, 1, 1, 1-(playerHealth / 200));
        Shoot();
        //Stealth();
        Pause();
        Move();
        Mathf.Clamp(playerHealth, 0, maxHealth);
    }
    void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
        if (isPaused)
        {
            canMove = false;
            canShoot = false;
            mainCamera.gameObject.SetActive(false);
            statCamera.gameObject.SetActive(true);
        }
        else
        {
            canMove = true;
            canShoot = true;
            mainCamera.gameObject.SetActive(true);
            statCamera.gameObject.SetActive(false);
            Look();
        }
    }
    public void LoseHealth(float damage)
    {
        //take damage
        playerHealth -= damage;
        if(playerHealth <= 0)
        {
            gameOverPanel.SetActive(true);
        }
    }
    /*void Stealth()
    {
        //enable/disable stealth mode
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            //stealth = !stealth;
        }
        //activate stealth material
        if(stealth)
        {            
            rend.material = stealthMat;
            playerGlow.gameObject.SetActive(false);
            playerTorch.gameObject.SetActive(false);
        }
        //enable normal material
        else if (!stealth)
        {
            rend.material = defaultMat;
            playerGlow.gameObject.SetActive(true);
            playerTorch.gameObject.SetActive(true);
        }
    }*/
     //look in position of mouse cursor using raycasting
    void Look()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if(groundPlane.Raycast(cameraRay, out rayLength))
        {
            pointToLookAt = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(pointToLookAt.x, transform.position.y, pointToLookAt.z));
        }
    }
    void Move()
    {
        //move forward
        if(Input.GetKey(KeyCode.W) && canMove)
        {
            anim.SetTrigger("Walk");
            playerRb.AddForce(Vector3.forward * maxForce);
        }
        //move back
        if (Input.GetKey(KeyCode.S) && canMove)
        {
            anim.SetTrigger("Walk");
            playerRb.AddForce(Vector3.back * maxForce);
        }
        //move left
        if (Input.GetKey(KeyCode.A) && canMove)
        {
            anim.SetTrigger("Walk");
            playerRb.AddForce(Vector3.left * maxForce);
        }
        //move right
        if (Input.GetKey(KeyCode.D) && canMove)
        {
            anim.SetTrigger("Walk");
            playerRb.AddForce(Vector3.right * maxForce);
        }
        //stop moving if nothing pressed
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            anim.SetTrigger("Idle");
            playerRb.velocity = Vector3.zero;
        }
        //clamp player velocity
        playerRb.velocity = Vector3.ClampMagnitude(playerRb.velocity, maxSpeed);
    }
    void Shoot()
    {
        //disable shooting if power is low or player is in stealth mode.
        /*if(stealth)
        {
            canShoot = false;
        }
        //enable shooting is power is sufficient and player is in normal mode.
        else if(!stealth)
        {
            canShoot = true;
        }*/
        //shooting code
        if(Input.GetKey(KeyCode.Mouse0))
        {
            muzzleFlash.SetActive(true);
            if (canShoot && Time.time >= nextTimeToFire)
            {
                playerAs.Play();
                Instantiate(powerShot, firePoint.position, firePoint.rotation);
                nextTimeToFire = Time.time + 1 / fireRate;
                //playerPower -= 2;
            }
        }
        else
        {
            muzzleFlash.SetActive(false);
            playerHealth += 0.05f;
        }
    }

    void Dash()
    {
        print(timer);
        if (Input.GetKey(KeyCode.Mouse1)&& (timer>0))
        {
            { 
                playerRb.AddForce(transform.forward * 5000);
                timer -= Time.deltaTime;
            }
        }
        if(Input.GetKeyUp(KeyCode.Mouse1))
        {
            timer = 0.3f;
        }
    }
}
