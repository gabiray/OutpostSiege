using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Player stats")]
    [SerializeField] private float moveForce = 10f;

    private float movementX;

    private Rigidbody2D playerBody;
    private Animator animator;

    private string RUNNING_ANIMATION = "running";
    private SpriteRenderer spriteRenderer;

    // Added 07.05.25 for player stats 
    [Header("Player coins")]
    [SerializeField] private Text coinText;

    [SerializeField] private int maxCoins = 20;
    [SerializeField] private int startingCoins = 20;

    private int currentCoins;


    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentCoins = Mathf.Clamp(startingCoins, 0, maxCoins);
        UpdateCoinUI();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer();
    }

    void PlayerMoveKeyboard()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;
    }

    void AnimatePlayer() // player movement on X axis
    {
        // player goes to the right
        if (movementX > 0f)
        {
            animator.SetBool(RUNNING_ANIMATION, true);
            spriteRenderer.flipX = false;
        }
        else if (movementX < 0f) // player goes to the left
        { 
            animator.SetBool(RUNNING_ANIMATION, true);
            spriteRenderer.flipX = true;
        }
        else
        {
            animator.SetBool(RUNNING_ANIMATION, false);
        }
    }

    // added 07.05.25 for coint update
    void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = $"x{currentCoins}";
        }
    }

}
