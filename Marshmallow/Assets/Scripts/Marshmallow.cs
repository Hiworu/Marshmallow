using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Marshmallow : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private List<GameObject> sudicio;
    //[SerializeField] private Scrollbar jumpbar;
    [SerializeField] private float PhaseTime;
    private Dictionary<KeyCode, bool> keys = new Dictionary<KeyCode, bool>();
    private int phase;
    public float speed;
    private float acceleration;
    private float horizontal;
    public float jumpTimeCounter;
    public float jumpTime;
    public bool isJumping;
    private bool isInLvl;
    public float jumpForce;

    GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Space) && isJumping == false && isInLvl == false)
        {
           Debug.Log("JUMP BITCHHH");
           if(jumpTimeCounter > 0)
           {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
           }
           else
           {
            isJumping = false;
           }
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
           isJumping = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D start)
    {
        if(start.tag == "Start")
        {
            Debug.Log("miao");
            isInLvl = true;
            //rb.gravityScale = 0;
            rb.isKinematic = true;
            rb.velocity = new Vector2 (rb.velocity.x, 0);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private IEnumerator FallPhase()
    {
        float time = 0;
        while (time < PhaseTime)
        {
            time += Time.deltaTime;
            yield return null;
        }
        phase++;
    }

}
