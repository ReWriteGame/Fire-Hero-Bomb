using System;
using GamePlay.Player.Touch;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
   [SerializeField]private TouchScreenInfoPanel touch;
   [SerializeField]private Ship shipPlayer;
   [SerializeField]private LevelCounter levelSeeds;
   [SerializeField]private TextMeshProUGUI levelLabel;

   private void Awake()
   {
      if (!levelSeeds.IsLastLevel())
      {
         Random.seed = (int) levelSeeds.GetCurrentValue();
      }
   }

   private void Start()
   {
      levelLabel.text = $"{levelSeeds.CurrentLevel + 1}";
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
