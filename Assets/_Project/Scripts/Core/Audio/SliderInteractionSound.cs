using Plugins.Audio;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Core.Audio
{
    public class SliderInteractionSound : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public void OnPointerDown(PointerEventData eventData) => AudioSystem.UI_Down.PlayOneShot();
        public void OnPointerUp(PointerEventData eventData) => AudioSystem.UI_Up.PlayOneShot();
    }
}