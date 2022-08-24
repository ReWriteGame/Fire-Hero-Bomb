using GamePlay.Player.Touch;
using UnityEngine;

public class GameController : MonoBehaviour
{
   [SerializeField]private TouchScreenInfoPanel touch;
   [SerializeField]private Ship shipPlayer;
   [SerializeField]private float moveSpeed;

   private void Start()
   {
      touch.OnTouchDown.AddListener(StartFire);
      touch.OnTouchUp.AddListener(StopFire);
      touch.OnTouchDelta.AddListener(MovePlayer);
   }

   private void OnDestroy()
   {
      touch.OnTouchDown.RemoveListener(StartFire);
      touch.OnTouchUp.RemoveListener(StopFire);
      touch.OnTouchDelta.RemoveListener(MovePlayer);
   }

   private void MovePlayer(Vector2 direction)
   {
      float positionX = Mathf.Clamp(direction.x * moveSpeed, -2, 2);
      positionX = Mathf.Clamp(shipPlayer.transform.position.x + positionX, -2, 2);
      
      shipPlayer.transform.position = new Vector3(positionX, shipPlayer.transform.position.y, shipPlayer.transform.position.z);
   }

   private void StartFire(Vector2 direction)
   {
      shipPlayer.StartFire();
   }
   
   private void StopFire(Vector2 direction)
   {
      shipPlayer.StopFire();
   }
}
