using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Config/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    public float xSpeed = 5f;
    public float jumpVelocity = 14f;
    public float jumpForce = 10f;
    public float terminalVelocity = -20f;
    public int maxJumps = 2;
    public float gravityScale = 3f;
    public float respawnDelay = 3f; // Shared respawn delay
}

