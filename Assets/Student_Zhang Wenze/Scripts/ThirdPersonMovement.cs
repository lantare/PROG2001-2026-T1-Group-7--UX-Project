using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ThirdPersonMovement : MonoBehaviour
{
    [Header("Mobile")]
    public float moveSpeed = 5f;
    public float rotateSpeed = 10f;

    [Header("Gravity setting (newly added)")]
    public float gravity = -9.8f; 
    public float groundedOffset = -2f; 

    [Header("Animation")]
    public string RunBool = "Run";

    [Header("Quoted")]
    public Transform mainCamera;
    public Animator anim;

    [Header("Gold coin setting")]
    public Text coinText;       // 
    public int coinCount = 0;   

    public GameObject gameOver; 
    public bool isGameOver = false;

    private CharacterController controller;
    private Vector3 moveDir;
    private Vector3 velocity; 

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (mainCamera == null) mainCamera = Camera.main.transform;
        if (anim == null) anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (isGameOver) return; 
        Move();
        ApplyGravity(); 
        UpdateAnimation();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 camF = mainCamera.forward;
        Vector3 camR = mainCamera.right;
        camF.y = 0; camR.y = 0;
        camF.Normalize(); camR.Normalize();

        moveDir = (camF * v + camR * h).normalized;

        if (moveDir.magnitude > 0.1f)
        {
            controller.Move(moveDir * moveSpeed * Time.deltaTime);
            Quaternion rot = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotateSpeed * Time.deltaTime);
        }
    }


    void ApplyGravity()
    {
  
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = groundedOffset;
        }

     
        velocity.y += gravity * Time.deltaTime;

     
        controller.Move(velocity * Time.deltaTime);
    }

    void UpdateAnimation()
    {
        bool isWalking = moveDir.magnitude > 0.1f;

    
        anim.SetBool(RunBool, isWalking);
    }

 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            coinCount++;
            UpdateCoinUI();
            gameObject.GetComponent<AudioSource>().Play();
            if (coinCount >= 12)
            {
                GameOver();
            }
        }
    }

    void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = coinCount.ToString();
        }
    }

    void GameOver()
    {
        isGameOver = true;

        gameOver.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLevel_1()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadLevel_2()
    {
        SceneManager.LoadScene(3);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
