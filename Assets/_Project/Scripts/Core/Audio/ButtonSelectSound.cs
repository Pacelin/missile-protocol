using Plugins.Audio;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Core.Audio
{
    public class ButtonSelectSound : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
    {
        public void OnPointerEnter(PointerEventData eventData) => AudioSystem.UI_Hover.PlayOneShot();
        public void OnPointerDown(PointerEventData eventData) => AudioSystem.UI_Down.PlayOneShot();
        public void OnPointerUp(PointerEventData eventData) => AudioSystem.UI_Select.PlayOneShot();
    }
}