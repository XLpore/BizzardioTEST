using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Animator animator;

    [SerializeField] private Gunner gunner; //  rreferencia al Gunner

    private float baseSpeed = 10;
    private float currentSpeed;

    [SerializeField] float pushForce;
    bool canMove = true;
    Vector3 direction;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (gunner == null)
        {
            gunner = GetComponent<Gunner>();
        }
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        direction = new Vector3(x, y);

        if (direction.x != 0)
        {
            animator.SetFloat("X", direction.x);
            animator.SetFloat("Y", 0);
        }
        else
        {
            animator.SetFloat("X", 0);
            animator.SetFloat("Y", direction.y);
        }

        if (rb2D.velocity.magnitude < 1.8f)
        {
            canMove = true;
        }


        switch (gunner.CurrentColor)
        {
            case ColorState.Green:
                currentSpeed = baseSpeed * 0.55f;
                break;
            case ColorState.Blue:
                currentSpeed = baseSpeed * 0.545f;
                break;
            case ColorState.Red:
                currentSpeed = baseSpeed * 0.40f;
                break;
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Move(direction);
        }
    }

    void Move(Vector3 dir)
    {
        rb2D.velocity = dir.normalized * currentSpeed * 100 * Time.fixedDeltaTime;
    }

    public void Push(Vector3 dir)
    {
        canMove = false;
        rb2D.velocity = Vector2.zero;
        rb2D.AddForce(dir * pushForce, ForceMode2D.Impulse);
    }
}