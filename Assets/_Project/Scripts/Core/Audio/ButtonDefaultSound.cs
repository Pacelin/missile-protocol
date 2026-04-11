using Plugins.Audio;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Core.Audio
{
    public class ButtonDefaultSound : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerClickHandler
    {
        public void OnPointerEnter(PointerEventData eventData) => AudioSystem.UI_Hover.PlayOneShot();
        public void OnPointerDown(PointerEventData eventData) => AudioSystem.UI_Down.PlayOneShot();
        public void OnPointerClick(PointerEventData eventData) => AudioSystem.UI_Up.PlayOneShot();
    }
}