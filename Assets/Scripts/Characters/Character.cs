using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private int unitMove = 1;
    [SerializeField]
    private int minX = -5;
    [SerializeField]
    private int maxX = 5;
    [SerializeField]
    private int minY = -10;
    [SerializeField]
    private int maxY = 10;
    [SerializeField]

    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite spriteUp;
    [SerializeField]
    private Sprite spriteDown;
    [SerializeField]
    private Sprite spriteLeft;
    [SerializeField]
    private Sprite spriteRight;

    [SerializeField]
    private LayerMask obstacleLayer;

    private int checkDistance = 1;
    private Vector3 moveDirection;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move();
        }
    }

    private void Move()
    {
        moveDirection = Vector3.zero;
        Sprite newSprite = spriteRenderer.sprite;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveDirection = Vector3.up;
            newSprite = spriteUp;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveDirection = Vector3.down;
            newSprite = spriteDown;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveDirection = Vector3.left;
            newSprite = spriteLeft;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveDirection = Vector3.right;
            newSprite = spriteRight;
        }

        spriteRenderer.sprite = newSprite;

        if (moveDirection != Vector3.zero)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, checkDistance, obstacleLayer);

            if (hit.collider == null)
            {
                Vector3 nextPosition = transform.position + moveDirection;

                if (minX <= nextPosition.x && nextPosition.x <= maxX && minY <= nextPosition.y && nextPosition.y <= maxY)
                {
                    transform.position = nextPosition;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("FixedObstacle"))
        {
            // HandleFixedObstacleTrigger(other.gameObject);
        }
        else if ( other.gameObject.CompareTag("MovingObstacle") )
        {
            HandleMovingObstacleTrigger(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Item"))
        {
            HandleItemTrigger(other.gameObject);
        }
    }

    private void HandleFixedObstacleTrigger(GameObject obstacle)
    {
        // 현재로서는 충돌하지 않는다.
        Debug.Log("고정된 장애물과 충돌!");
    }
    
    private void HandleMovingObstacleTrigger(GameObject obstacle)
    {
        Debug.Log("움직이는 장애물과 충돌!");
        Destroy(gameObject);
        ScoreManager.instance.SetGameOver();
    }

    private void HandleItemTrigger(GameObject item)
    {
        Debug.Log("아이템과 충돌!");
    }

    public void UpgradeItem(Item item)
    {
        string itemName = item.GetItemName();
        if (itemName == "Coin")
        {
            Debug.Log("동전 획득");
        }
        else if (itemName == "Magnet")
        {
            Debug.Log("자석 획득");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = moveDirection != Vector3.zero ? moveDirection : Vector3.up;
        Gizmos.DrawRay(transform.position, direction * checkDistance);
    }
}