using UnityEngine;

public class Object_NPC : MonoBehaviour
{
    protected Transform player;
    protected UI ui;

    [SerializeField] private Transform npc;
    [SerializeField] private GameObject interactionToolTip;
    private bool facingRight = true;

    [Header("Floay ToolTip")]
    [SerializeField] private float floatSpeed = 8f;
    [SerializeField] private float floatRange = .1f;
    private Vector3 startPosition;

    protected virtual void Awake()
    {
        ui = GameObject.FindFirstObjectByType<UI>();
        startPosition = interactionToolTip.transform.position;
        interactionToolTip.SetActive(false);
    }

    protected virtual void Update()
    {
        HandleNpcFlip();
        HandleToolTipFloat();
    }

    private void HandleToolTipFloat()
    {
        if (interactionToolTip.activeSelf)
        {
            float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatRange;
            interactionToolTip.transform.position = startPosition + new Vector3(0, yOffset);
        }
    }

    private void HandleNpcFlip()
    {
        if (player == null || npc == null)
            return;

        if (npc.position.x > player.position.x && facingRight)
        {
            npc.transform.Rotate(0f, 180f, 0f);
            facingRight = false;
        }
        else if (npc.position.x < player.position.x && !facingRight)
        {
            npc.transform.Rotate(0f, 180f, 0f);
            facingRight = true;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.transform;
        interactionToolTip.SetActive(true);
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        // player = null;
        interactionToolTip.SetActive(false);
    }
}
