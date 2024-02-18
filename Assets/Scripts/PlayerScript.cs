using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Variables")]
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    [Header("Dash Settings")]
    public float DashSpeed = 10;
    public float DashDuration = 1f;
    public float DashCoolDown = 1f;

    [Header("Projectile Variables")]
    private Vector3 destination;
    public GameObject Projectile;
    public Transform FPSFirePoint;
    public float ProjectileSpeed = 30.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;

    public ParticleSystem SprayParticle;

    float rotationX = 0;


    [HideInInspector]
    public bool canMove = true;
    public bool canRun = true;
    public bool MeleeWeapon = false;
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        //lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        SprayParticle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        //Grounded and reacalculating move direction based axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        //Press Left shift to sprint
        bool isDashing = false;
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (MeleeWeapon == true)
            {
                MeleeWeaponUse();
            }
            else
            {
            ShootProjectile();
            }
        }
    }

    private void ShootProjectile()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000);
        }

        InstantiateProjectile(FPSFirePoint);

    }

    public void MeleeWeaponUse()
    {

    }

    void InstantiateProjectile(Transform firePoint)
    {
        var projectileObj = Instantiate(Projectile, firePoint.position, firePoint.rotation) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * ProjectileSpeed;
        SprayParticle.Play();
    }
}

