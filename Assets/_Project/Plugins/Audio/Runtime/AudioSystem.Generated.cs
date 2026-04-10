// ReSharper disable RedundantUsingDirective
#pragma warning disable CS1998

using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using FMODUnity;
using UnityEngine;

namespace Plugins.Audio
{
    public static partial class AudioSystem
    {
		public static class Global
		{
			public enum ELabel_PauseState
			{
				NotOnPause = 0,
				OnPause = 1,
			}
			private static readonly FMOD.Studio.PARAMETER_ID PauseStateId = new FMOD.Studio.PARAMETER_ID() { data1 = 998884499, data2 = 1992308322 };

			public static void SetPauseState(ELabel_PauseState value) => RuntimeManager.StudioSystem.setParameterByID(PauseStateId, (int) value);
			public static ELabel_PauseState GetPauseState()
			{
				RuntimeManager.StudioSystem.getParameterByID(PauseStateId, out var value);
				return (ELabel_PauseState) (int) value;
			}

		}
    
		public static SoundEvent_UI_Up UI_Up { get; } = new();
		public static SoundEvent_UI_Down UI_Down { get; } = new();
		public static SoundEvent_UI_Slider UI_Slider { get; } = new();
		public static SoundEvent_UI_Select UI_Select { get; } = new();
		public static SoundEvent_UI_Hover UI_Hover { get; } = new();
		public static SoundEvent_MainMenu_Music MainMenu_Music { get; } = new();
    }

	public class SoundEvent_UI_Up : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 0;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = -1302993147, Data2 = 1281622016, Data3 = -1868696186, Data4 = 790085203 };

		public void PlayOneShot() => RuntimeManager.PlayOneShot(_guid);
		public void PlayOneShotAttached(GameObject attachTo) => RuntimeManager.PlayOneShotAttached(_guid, attachTo);
		public void PlayOneShotInPoint(Vector3 point) => RuntimeManager.PlayOneShot(_guid, point);

		public Instance CreateInstance() => new Instance(RuntimeManager.CreateInstance(_guid));
		ISoundEventInstance ISoundEvent.CreateInstance() => CreateInstance();

		public class Instance : SoundEventInstance
		{
			public Instance(FMOD.Studio.EventInstance eventInstance) : base(eventInstance) { }

		}
	}

	public class SoundEvent_UI_Down : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 0;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = -1725450952, Data2 = 1312468931, Data3 = 1079365051, Data4 = 1941834710 };

		public void PlayOneShot() => RuntimeManager.PlayOneShot(_guid);
		public void PlayOneShotAttached(GameObject attachTo) => RuntimeManager.PlayOneShotAttached(_guid, attachTo);
		public void PlayOneShotInPoint(Vector3 point) => RuntimeManager.PlayOneShot(_guid, point);

		public Instance CreateInstance() => new Instance(RuntimeManager.CreateInstance(_guid));
		ISoundEventInstance ISoundEvent.CreateInstance() => CreateInstance();

		public class Instance : SoundEventInstance
		{
			public Instance(FMOD.Studio.EventInstance eventInstance) : base(eventInstance) { }

		}
	}

	public class SoundEvent_UI_Slider : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 0;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = -1502482606, Data2 = 1127176891, Data3 = -1953010792, Data4 = -1709114476 };

		public void PlayOneShot() => RuntimeManager.PlayOneShot(_guid);
		public void PlayOneShotAttached(GameObject attachTo) => RuntimeManager.PlayOneShotAttached(_guid, attachTo);
		public void PlayOneShotInPoint(Vector3 point) => RuntimeManager.PlayOneShot(_guid, point);

		public Instance CreateInstance() => new Instance(RuntimeManager.CreateInstance(_guid));
		ISoundEventInstance ISoundEvent.CreateInstance() => CreateInstance();

		public class Instance : SoundEventInstance
		{
			public Instance(FMOD.Studio.EventInstance eventInstance) : base(eventInstance) { }

		}
	}

	public class SoundEvent_UI_Select : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 0;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = -291369823, Data2 = 1280683505, Data3 = 1729532826, Data4 = -1685885936 };

		public void PlayOneShot() => RuntimeManager.PlayOneShot(_guid);
		public void PlayOneShotAttached(GameObject attachTo) => RuntimeManager.PlayOneShotAttached(_guid, attachTo);
		public void PlayOneShotInPoint(Vector3 point) => RuntimeManager.PlayOneShot(_guid, point);

		public Instance CreateInstance() => new Instance(RuntimeManager.CreateInstance(_guid));
		ISoundEventInstance ISoundEvent.CreateInstance() => CreateInstance();

		public class Instance : SoundEventInstance
		{
			public Instance(FMOD.Studio.EventInstance eventInstance) : base(eventInstance) { }

		}
	}

	public class SoundEvent_UI_Hover : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 0;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 376591030, Data2 = 1335020301, Data3 = -1467930437, Data4 = 979732966 };

		public void PlayOneShot() => RuntimeManager.PlayOneShot(_guid);
		public void PlayOneShotAttached(GameObject attachTo) => RuntimeManager.PlayOneShotAttached(_guid, attachTo);
		public void PlayOneShotInPoint(Vector3 point) => RuntimeManager.PlayOneShot(_guid, point);

		public Instance CreateInstance() => new Instance(RuntimeManager.CreateInstance(_guid));
		ISoundEventInstance ISoundEvent.CreateInstance() => CreateInstance();

		public class Instance : SoundEventInstance
		{
			public Instance(FMOD.Studio.EventInstance eventInstance) : base(eventInstance) { }

		}
	}

	public class SoundEvent_MainMenu_Music : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 0;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 1834001975, Data2 = 1326438945, Data3 = 1922417588, Data4 = 821119010 };

		public void PlayOneShot() => RuntimeManager.PlayOneShot(_guid);
		public void PlayOneShotAttached(GameObject attachTo) => RuntimeManager.PlayOneShotAttached(_guid, attachTo);
		public void PlayOneShotInPoint(Vector3 point) => RuntimeManager.PlayOneShot(_guid, point);

		public Instance CreateInstance() => new Instance(RuntimeManager.CreateInstance(_guid));
		ISoundEventInstance ISoundEvent.CreateInstance() => CreateInstance();

		public class Instance : SoundEventInstance
		{
			public Instance(FMOD.Studio.EventInstance eventInstance) : base(eventInstance) { }

		}
	}

}