using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Player : MonoBehaviour
{
    float depthScore;
    Rigidbody2D rb;
    BoxCollider2D box;

    [SerializeField]
    GameObject ScoreCounter;

    [SerializeField]
    LayerMask Mask;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        depthScore = 0.00f;
        ScoreCounter.GetComponent<TextMeshPro>().text = "Score: " + depthScore.ToString();
    }

   public void OnMove(InputAction.CallbackContext context)
    {
        if (context.action.triggered)
        {
            transform.Translate(context.ReadValue<Vector2>());
        }
    }

    bool isTag(string tag, string layer)
    {
        // Box project
        RaycastHit2D hit = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0f, Vector2.down, 1f, LayerMask.GetMask(layer));
        if (hit.collider)
        {
            Debug.Log("Name: " + hit.collider.gameObject.name);
            return hit.collider.CompareTag(tag);
        }
        else
        {
            return false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isTag("Enemy", "Enemy"))
        {
            Debug.Log("DEAD");
            GetComponent<Rigidbody2D>().gravityScale = 0;
            Destroy(gameObject);
        }
        else
        {
            depthScore += 1;
            ScoreCounter.GetComponent<TextMeshPro>().text = "Score: " + depthScore.ToString();
        }
    }
}
