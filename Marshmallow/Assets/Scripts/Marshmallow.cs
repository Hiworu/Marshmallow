using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Marshmallow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform jumpPoint;
    Rigidbody2D rb;
    [SerializeField] private List<GameObject> sudicio;
    [SerializeField] private Image jumpbar;
    [SerializeField] private float PhaseTime;
    //private Dictionary<KeyCode, bool> keys = new Dictionary<KeyCode, bool>();
    private int phase;
    public float speed;
    private float acceleration;
    private float horizontal;
    public float jumpTimeCounter;
    public float jumpTime;
    public float jumpForce;
    public float TBarra = 5f;
    public bool isJumping;
    private bool isInLvl;
    public bool perfJump;
    public bool midJump;
    public bool jumpPhase = true;
    public float holdTimer;
    public bool isHolding;

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
        /*if(Input.GetKey(KeyCode.Space) && isJumping == false && isInLvl == false)
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
        }*/

        if(jumpPhase == true && Input.GetKeyDown(KeyCode.Space))
        {
            isHolding = true;
            StartCoroutine(Stanghett());
            JumpPhase();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isHolding = false;
        }

        if(isHolding == true)
        {
            holdTimer += Time.deltaTime;
            jumpbar.fillAmount = holdTimer / TBarra;
        }
        else
        {
            jumpbar.fillAmount = 0;
        }

    }

    private void JumpPhase()
    {
        if(perfJump == true)
        {
            jumpForce = 10;
            player.transform. position = Vector3.MoveTowards(player.position, jumpPoint.position, speed);
        }
        else if(midJump == true)
        {
            jumpForce = 5;
            player.transform. position = Vector3.MoveTowards(player.position, jumpPoint.position, speed);
        }
        else
        {
            jumpForce = 3;
            player.transform. position = Vector3.MoveTowards(player.position, jumpPoint.position, speed);
        }

        jumpPhase = false;
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

    private IEnumerator Stanghett()
    {
        while(holdTimer < TBarra)
        {          
            if(Input.GetKeyUp(KeyCode.Space) && holdTimer >= 3 && holdTimer < 4)
            {
                perfJump = true;
                Debug.Log("perfect");
            }
            else if (Input.GetKeyUp(KeyCode.Space) && holdTimer >= 4 && holdTimer < 5)
            {
                midJump = true;
                Debug.Log("troppo");
            }
            else if (Input.GetKeyUp(KeyCode.Space) && holdTimer < 3 && holdTimer > 0)
            {
                Debug.Log("meh");
            }
            yield return null;
        }
    }

}
