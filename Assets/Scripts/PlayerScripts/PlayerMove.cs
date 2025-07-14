using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gridSize = 1f;
    private bool isMoving = false;
    private Vector2 input;
    private Vector3 lastDirection = Vector3.down;

    public LayerMask obstacleLayer;
    public LayerMask mobLayer;
    public CSVDialogueReader dialogueReader;

    void Update()
    {
        // 会話中は移動も話しかけも無効
        if (dialogueReader.IsDialogueActive())
            return;

        // 話しかけ処理（Enter または クリック）
        if (!isMoving && (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)))
        {
            TryTalkToMob();
            return; // ← 進行処理と分離
        }

        // 移動処理
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                lastDirection = new Vector3(input.x, input.y, 0);
                Vector3 destination = transform.position + lastDirection * gridSize;

                if (!IsBlocked(destination))
                {
                    StartCoroutine(Move(destination));
                }
            }
        }
    }

    IEnumerator Move(Vector3 destination)
    {
        isMoving = true;

        while ((destination - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = destination;
        isMoving = false;
    }

    bool IsBlocked(Vector3 destination)
    {
        return Physics2D.OverlapBox(destination, new Vector2(0.9f, 0.9f), 0f, obstacleLayer) != null;
    }

    void TryTalkToMob()
    {
        Vector3 checkPosition = transform.position + lastDirection * gridSize;
        Collider2D mob = Physics2D.OverlapBox(checkPosition, new Vector2(0.9f, 0.9f), 0f, mobLayer);

        if (mob != null)
        {
            MobTalkData data = mob.GetComponent<MobTalkData>();
            if (data != null && dialogueReader != null)
            {
                TalkTracker tracker = FindObjectOfType<TalkTracker>();

                dialogueReader.StartDialogueFrom(
                    data.startRow,
                    data.nameColumnIndex,
                    data.messageColumnIndex,
                    data.characterName,
                    tracker
                );
            }
        }
    }
}
