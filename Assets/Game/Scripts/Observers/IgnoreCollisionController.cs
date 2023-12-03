using UnityEngine;

public class IgnoreCollisionController : MonoBehaviour, IGameStartListener, IGameFinishListener
{
    private const int PLAYER_LAYER = 3;
    private const int ROAD_LAYER = 8;


    public void OnStartGame() => Physics.IgnoreLayerCollision(PLAYER_LAYER, ROAD_LAYER, true);
    public void OnFinishGame() => Physics.IgnoreLayerCollision(PLAYER_LAYER, ROAD_LAYER, false);
}