using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public LayerMask obstacleLayer;
    public LayerMask mobLayer;
    public float gridSize = 1f;
    public GameObject SmartPhonePanel;
    public CSVDialogueReader dialogueReader;

    private Vector2 input;
    private bool isMoving;
    private bool isPaneOpen;
    private Vector3 lastDirection = Vector3.down;
    public GameObject ChatPanel;

    void Update()
    {
        // スマホパネルが表示中か確認
        isPaneOpen = SmartPhonePanel.activeSelf;

        // スマホの開閉：Eキーでトグル
        if (Input.GetKeyDown(KeyCode.E))
        {
            SmartPhonePanel.SetActive(!SmartPhonePanel.activeSelf);
            return;
        }

        // スマホ or 会話中はプレイヤー操作不可
        if (isPaneOpen || dialogueReader.IsDialogueActive())
            return;

        // 話しかけ処理（エンター or クリック）
        if (!isMoving && (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)))
        {
            TryTalkToMob();
            return;
        }

        // プレイヤー移動処理
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0) input.y = 0; // 十字方向のみ

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

        while ((destination - transform.position).sqrMagnitude > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = destination;
        isMoving = false;
    }

    bool IsBlocked(Vector3 destination)
    {
        Collider2D hit = Physics2D.OverlapCircle(destination, 0.2f, obstacleLayer);
        return hit != null;
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

    public void OpenChatPanel()
    {
        ChatPanel.SetActive(true);
    }

    public void CloseSmartPhonePanel()
    {
        ChatPanel.SetActive(false);
    }
}
