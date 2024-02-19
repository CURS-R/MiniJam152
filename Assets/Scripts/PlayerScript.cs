using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerScript : MonoBehaviour
{
    public CharacterController characterController;
    
    [Header("Player Variables")]
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed => PlayerConfiguration.CameraSensitivity;
    public float lookXLimit = 45.0f;
    [FormerlySerializedAs("ToothPickAnimation")] public Animator ToothPickAnimator;
    public Animator BallAnimator;

    [Header("Dash Settings")]
    public float DashSpeed = 10;
    public float DashDuration = 1f;
    public float DashCoolDown = 1f;

    [Header("Projectile Variables")]
    private Vector3 destination;
    public GameObject Projectile;
    public Transform FPSFirePoint;
    public float ProjectileSpeed = 30.0f;

    Vector3 moveDirection = Vector3.zero;

    public ParticleSystem SprayParticle;

    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;
    public bool canRun = true;
    public bool MeleeWeapon = false;
    void Start()
    {
        //lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        SprayParticle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        //Grounded and reacalculating move direction based axes
        Vector3 forward = characterController.transform.TransformDirection(Vector3.forward);
        Vector3 right = characterController.transform.TransformDirection(Vector3.right);
        //Press Left shift to sprint
        bool isDashing = false;
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            AudioManager.Instance.PlayASound(AudioClips.Instance.ToothpickStab, transform.position);
            moveDirection.y = jumpSpeed;
        }
        else
            moveDirection.y = movementDirectionY;

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
            var calculatedLookSpeed = lookSpeed * 100 * Time.deltaTime;
            rotationX += -Input.GetAxis("Mouse Y") * calculatedLookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, playerCamera.transform.localRotation.y, playerCamera.transform.localRotation.z);
            characterController.transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * calculatedLookSpeed, 0);
            //playerCamera.transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        if (Input.GetButtonDown("Fire1"))
            ShootProjectile();

        if (Input.GetButtonDown("Fire2"))
            UseMeleeWeapon();
    }

    private Coroutine projectileCoroutine;
    private void ShootProjectile()
    {
        if (projectileCoroutine != null)
        {
            return;
        }
        //StopCoroutine(ProjectileCoroutine());
        ToothPickAnimator.SetBool("IsPoking", false);
        //projectileCoroutine = null;
        
        projectileCoroutine = StartCoroutine(ProjectileCoroutine());
    }
    IEnumerator ProjectileCoroutine()
    {
        BallAnimator.SetBool("HideBall", true);
        yield return new WaitForSeconds(0.2f);
        InstantiateProjectile(FPSFirePoint);
        yield return new WaitForSeconds(1f);
        BallAnimator.SetBool("HideBall", false);
        yield return new WaitForSeconds(0.2f);
        projectileCoroutine = null;
    }
    
    private Coroutine weaponCoroutine;
    public void UseMeleeWeapon()
    {
        if (weaponCoroutine != null)
        {
            return;
        }
        //StopCoroutine(weaponCoroutine);
        ToothPickAnimator.SetBool("IsPoking", false);
        //weaponCoroutine = null;
        
        weaponCoroutine = StartCoroutine(MeleeCoroutine());
    }
    IEnumerator MeleeCoroutine()
    {
        ToothPickAnimator.SetBool("IsPoking", true);
        yield return new WaitForSeconds(.2f);
        DoMeleeRaycast();
        AudioManager.Instance.PlayASound(AudioClips.Instance.ToothpickStab, transform.position);
        ToothPickAnimator.SetBool("IsPoking", false);
        yield return new WaitForSeconds(.2f);
        weaponCoroutine = null;
    }
    private void DoMeleeRaycast()
    {
        var ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out var hit, 4f))
        {
            var rat = hit.collider.GetComponent<Rat>();
            if (rat != null)
            {
                rat.Controller.TryDie();
            }
        }
    }

    private void InstantiateProjectile(Transform firePoint)
    {
        var ray = playerCamera.ViewportPointToRay(new(0.5f, 0.5f, 0));
        destination = Physics.Raycast(ray, out var hit) ? hit.point : ray.GetPoint(1000);
        var projectileObj = Instantiate(Projectile, firePoint.position, firePoint.rotation);
        var projectile = projectileObj.GetComponent<Projectile>();
        projectile.Rigidbody.velocity = (destination - firePoint.position).normalized * ProjectileSpeed;
        //SprayParticle.Play(); // TODO: spray?
        AudioManager.Instance.PlayASound(AudioClips.Instance.BallThrow, characterController.transform.position);
    }
}

