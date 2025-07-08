using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float gridSize = 1f;
    private bool isMoving = false;
    private Vector2 input;
    private Vector3 targetPos;

    public LayerMask obstacleLayer;  // ��Q���̃��C���[�w��

    void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                Vector3 direction = new Vector3(input.x, input.y, 0);
                Vector3 destination = transform.position + direction * gridSize;

                // �� ��Q�������邩�`�F�b�N�iBoxCast�܂���Raycast�Œ����j
                if (!IsBlocked(destination))
                {
                    StartCoroutine(Move(destination));
                }
            }
        }
    }

    System.Collections.IEnumerator Move(Vector3 destination)
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

    // �� ��Q���̊m�F�i�����߂�BoxCast���g�p�j
    bool IsBlocked(Vector3 destination)
    {
        Vector2 center = destination;
        float boxSize = gridSize * 0.4f;

        return Physics2D.OverlapBox(center, new Vector2(boxSize, boxSize), 0f, obstacleLayer) != null;
    }
}
