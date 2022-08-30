using GamePlay.Player.Touch;
using UnityEngine;

public class GameController : MonoBehaviour
{
   [SerializeField]private TouchScreenInfoPanel touch;
   [SerializeField]private Ship shipPlayer;

   private void Start()
   {
      touch.OnTouchInPercent.AddListener(MovePlayer);
   }

   private void OnDestroy()
   {
      touch.OnTouchInPercent.RemoveListener(MovePlayer);
   }

   private void MovePlayer(Vector2 direction)
   {
      //float positionX = Mathf.Clamp(direction.x * moveSpeed, -2, 2);
      //positionX = Mathf.Clamp(shipPlayer.transform.position.x + positionX, -2, 2);

      float positionX = Mathf.Lerp(-2, 2, direction.x);
      shipPlayer.transform.position = new Vector3(positionX, shipPlayer.transform.position.y, shipPlayer.transform.position.z);
   }

  
   
   
}
