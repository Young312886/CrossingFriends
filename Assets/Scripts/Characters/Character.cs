using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class Character : MonoBehaviour {
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
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        Move();
    }

    private void Move() {
        Vector3 moveTo = Vector3.zero;
        if ( Input.GetKeyDown(KeyCode.UpArrow) ) {
            moveTo = new Vector3(0, unitMove, 0);
        } else if ( Input.GetKeyDown(KeyCode.DownArrow) ) {
            moveTo = new Vector3(0, -unitMove, 0);
        } else if ( Input.GetKeyDown(KeyCode.LeftArrow) ) {
            moveTo = new Vector3(-unitMove, 0, 0);
        } else if ( Input.GetKeyDown(KeyCode.RightArrow) ) {
            moveTo = new Vector3(unitMove, 0, 0);
        } 

        Vector3 nextPosition = transform.position + moveTo;

        // 다음 이동할 구역이 지정 구역을 벗어난 경우 
        nextPosition.x = Mathf.Clamp(nextPosition.x, minX, maxX);
        nextPosition.y = Mathf.Clamp(nextPosition.y, minY, maxY);
        transform.position = nextPosition;

        // if ( minX <= nextPosition.x && nextPosition.x <= maxX && minY <= nextPosition.y && nextPosition.y <= maxY ) {
        //     transform.position = nextPosition;
        // }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Obstacle")) {
            Debug.Log("장애물과 충돌: " + other.gameObject.name );
            // GameManager.instance.hasCollided(other.gameObject);
        } else if (other.gameObject.CompareTag("Item")) {
            Debug.Log("아이템과 충돌: " + other.gameObject.name );
            // GameManager.instance.GetItem(other.gameObject);
        }
    }

    public void UpgradeItem(Item item) {
        string itemName = item.GetItemName();
        if ( itemName == "Coin" ) {
            Debug.Log("동전 획득");
        } else if ( itemName == "Magnet" ) {
            Debug.Log("자석 획득");
        }
    }
}