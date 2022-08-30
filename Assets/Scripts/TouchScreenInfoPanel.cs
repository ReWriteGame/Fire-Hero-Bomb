using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GamePlay.Player.Touch
{
    [RequireComponent(typeof(Image))]
    public class TouchScreenInfoPanel : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private CanvasScaler currentCanvasScaler;
        [SerializeField] private Image limitTouchPanel;

        private Vector2 _beginTouchPositionScreen;
        private Vector2 _touchPositionScreen;
        private Vector2 _limitTouchScreenPosition;//center
        private Vector2 _touchScreenPositionFromCenter;// center
        private Vector2 _endTouchPositionScreen;
        private Vector2 _deltaTouchScreen;
        private Vector2 _touchScreenInPercent;
        [SerializeField] private Vector2 _directionTouchMove;
        [SerializeField] private Vector2 _directionTouchMoveInPercent;
        private Image _mainTouchPanel;


        public UnityEvent<Vector2> OnTouchUp;
        public UnityEvent<Vector2> OnTouchDown;
        public UnityEvent<Vector2> OnTouchBeginDrag;
        public UnityEvent<Vector2> OnTouchDrag;
        public UnityEvent<Vector2> OnTouchEndDrag;
        public UnityEvent<Vector2> OnTouchDelta;
        public UnityEvent<Vector2> OnTouchDeltaInPercent;
        public UnityEvent<Vector2> OnTouchInPercent;// 0:0 left down 
        public UnityEvent<Vector2> OnDirectionTouchMove;
        public UnityEvent<Vector2> OnDirectionTouchMoveInPercent;

        public UnityEvent OnPointerInTouchZone;
        public UnityEvent OnPointerNotInTouchZone;
        
        public Vector2 TouchScreenInPercent => _touchScreenInPercent;
        public Vector2 DeltaTouchScreen => _deltaTouchScreen;
        public Vector2 DirectionTouchMove => _directionTouchMove;
        public Vector2 DirectionTouchMoveInPercent => _directionTouchMoveInPercent;

        
        private void Awake()
        {
            _mainTouchPanel = GetComponent<Image>();
            currentCanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
            _mainTouchPanel.rectTransform.anchorMax = Vector2.one;
            _mainTouchPanel.rectTransform.anchorMin = Vector2.zero;
            _mainTouchPanel.rectTransform.sizeDelta = Vector2.zero;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _beginTouchPositionScreen = eventData.position;
            OnTouchDown?.Invoke(_beginTouchPositionScreen);
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            // position from center
            Vector2Int screenCenter = new Vector2Int(Screen.width, Screen.height) / 2;
            _touchScreenPositionFromCenter.x = eventData.position.x - screenCenter.x;
            _touchScreenPositionFromCenter.y = eventData.position.y - screenCenter.y;
            
            _limitTouchScreenPosition = limitTouchPanel ? LimitTouchZone() : _touchScreenPositionFromCenter;

            _touchScreenInPercent.x = (_limitTouchScreenPosition.x + limitTouchPanel.rectTransform.rect.xMax) / (limitTouchPanel.rectTransform.rect.width + limitTouchPanel.rectTransform.anchoredPosition.x);
            _touchScreenInPercent.y = (_limitTouchScreenPosition.y + limitTouchPanel.rectTransform.rect.yMax) / (limitTouchPanel.rectTransform.rect.height + limitTouchPanel.rectTransform.anchoredPosition.y);
            
            _touchPositionScreen = eventData.position;
            _deltaTouchScreen = eventData.delta;
            
            _directionTouchMove = _touchScreenPositionFromCenter - (_beginTouchPositionScreen - screenCenter);
            _directionTouchMoveInPercent = _directionTouchMove / screenCenter;
            
            OnDirectionTouchMove?.Invoke(_directionTouchMove);
            OnDirectionTouchMoveInPercent?.Invoke(_directionTouchMoveInPercent);
            OnTouchInPercent?.Invoke(_touchScreenInPercent);
            OnTouchDrag?.Invoke(_touchPositionScreen);
            OnTouchDelta?.Invoke(_deltaTouchScreen);
            OnTouchDeltaInPercent?.Invoke(_deltaTouchScreen );
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            _endTouchPositionScreen = eventData.position;
            OnTouchUp?.Invoke(_endTouchPositionScreen);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            OnTouchBeginDrag?.Invoke(eventData.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnTouchEndDrag?.Invoke(eventData.position);
        }

        private Vector2 LimitTouchZone()
        {
            float xMin = limitTouchPanel.rectTransform.rect.xMin + limitTouchPanel.rectTransform.anchoredPosition.x;
            float xMax = limitTouchPanel.rectTransform.rect.xMax + limitTouchPanel.rectTransform.anchoredPosition.x;
            float xPos = Mathf.Clamp(_touchScreenPositionFromCenter.x, xMin, xMax);
            
            float yMin = limitTouchPanel.rectTransform.rect.yMin + limitTouchPanel.rectTransform.anchoredPosition.y;
            float yMax = limitTouchPanel.rectTransform.rect.yMax + limitTouchPanel.rectTransform.anchoredPosition.y;
            float yPos = Mathf.Clamp(_touchScreenPositionFromCenter.y, yMin, yMax);

            if (PositionInTouchZone(xMin, xMax,_touchScreenPositionFromCenter.x) || PositionInTouchZone(yMin, yMax,_touchScreenPositionFromCenter.y))
            {
                OnPointerInTouchZone?.Invoke();
            }
            else
            {
                OnPointerNotInTouchZone?.Invoke();
            }
            
            return new Vector2(xPos, yPos);
        }

        private bool PositionInTouchZone(float minValue, float maxValue, float value)
        {
            return value < maxValue && value > minValue ? true : false;
        }
    }
}

