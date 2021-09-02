using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, ISaveable
{
    public static readonly List<Character> AllCharacters = new List<Character>();

    public Transform Visual;
    public float MoveForce;
    public float JumpForce;
    protected Rigidbody2D rigidBody2D;
    private TriggerDetector triggerDetector;
    private Animator animator;
    private float visualDirection;

    public bool IsGrounded => transform.parent;

    // Start is called before the first frame update
    private void Start()
    {
        visualDirection = 1.0f;
        rigidBody2D = GetComponent<Rigidbody2D>();
        triggerDetector = GetComponentInChildren<TriggerDetector>();
        animator = GetComponentInChildren<Animator>();
        AllCharacters.Add(this);
    }

    private void OnDestroy()
    {
        AllCharacters.Remove(this);
    }

    public void MoveLeft()
    {
        //if (triggerDetector.InTrigger)
        rigidBody2D.AddForce(new Vector2(-MoveForce, 0), ForceMode2D.Force);
    }

    public void MoveRight()
    {
        //if (triggerDetector.InTrigger)
        rigidBody2D.AddForce(new Vector2(MoveForce, 0), ForceMode2D.Force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<MovingPlatform>() != null)
            transform.SetParent(collision.transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<MovingPlatform>() != null)
            transform.SetParent(null);
    }

    public void Jump()
    {
        if (triggerDetector.InTrigger)
        {
            rigidBody2D.AddForce(new Vector2(0, JumpForce), ForceMode2D.Force);
            transform.SetParent(null);
        }
    }

    protected virtual void Update()
    {
        var vel = rigidBody2D.velocity.x;

        if (vel < -0.01f)
            visualDirection = -1.0f;
        else if (vel > 0.01f)
            visualDirection = 1.0f;

        var scale = Visual.localScale;
        scale.x = visualDirection;
        Visual.localScale = scale;

        animator.SetFloat("speed", Mathf.Abs(vel));
    }

    void ISaveable.Save(GameState gameState)
    {
        gameState.playerState.visualDirection = visualDirection;
        gameState.playerState.body.Init(rigidBody2D);
    }

    void ISaveable.Restore(GameState gameState)
    {
        visualDirection = gameState.playerState.visualDirection;
        gameState.playerState.body.Apply(rigidBody2D);
    }
}
