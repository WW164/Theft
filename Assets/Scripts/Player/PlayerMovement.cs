using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : NetworkBehaviour
{

    [SerializeField] private float speed = 3f;

    void Update()
    {
        // Only the owning client should control this player's movement
        if (!IsOwner) return;

        float h = 0f, v = 0f;
        if (Keyboard.current.wKey.isPressed) v = 1f;
        if (Keyboard.current.sKey.isPressed) v = -1f;
        if (Keyboard.current.dKey.isPressed) h = 1f;
        if (Keyboard.current.aKey.isPressed) h = -1f;

        Vector3 move = new Vector3(h, 0, v) * speed * Time.deltaTime;

        transform.Translate(move);
    }
}
