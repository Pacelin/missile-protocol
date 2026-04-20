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
			private static readonly FMOD.Studio.PARAMETER_ID EnginePowerId = new FMOD.Studio.PARAMETER_ID() { data1 = 2069726730, data2 = 1365511981 };
			private static readonly FMOD.Studio.PARAMETER_ID AmplitudeId = new FMOD.Studio.PARAMETER_ID() { data1 = 1227227585, data2 = 3246553292 };
			private static readonly FMOD.Studio.PARAMETER_ID FrequencyId = new FMOD.Studio.PARAMETER_ID() { data1 = 623296286, data2 = 1117968546 };

			public static void SetPauseState(ELabel_PauseState value) => RuntimeManager.StudioSystem.setParameterByID(PauseStateId, (int) value);
			public static ELabel_PauseState GetPauseState()
			{
				RuntimeManager.StudioSystem.getParameterByID(PauseStateId, out var value);
				return (ELabel_PauseState) (int) value;
			}

			public static void SetEnginePower(float value) => RuntimeManager.StudioSystem.setParameterByID(EnginePowerId, value);
			public static float GetEnginePower()
			{
				RuntimeManager.StudioSystem.getParameterByID(EnginePowerId, out var value);
				return value;
			}

			public static void SetAmplitude(float value) => RuntimeManager.StudioSystem.setParameterByID(AmplitudeId, value);
			public static float GetAmplitude()
			{
				RuntimeManager.StudioSystem.getParameterByID(AmplitudeId, out var value);
				return value;
			}

			public static void SetFrequency(float value) => RuntimeManager.StudioSystem.setParameterByID(FrequencyId, value);
			public static float GetFrequency()
			{
				RuntimeManager.StudioSystem.getParameterByID(FrequencyId, out var value);
				return value;
			}

		}
    
		public static SoundEvent_UI_Up UI_Up { get; } = new();
		public static SoundEvent_UI_Down UI_Down { get; } = new();
		public static SoundEvent_UI_Slider UI_Slider { get; } = new();
		public static SoundEvent_UI_Select UI_Select { get; } = new();
		public static SoundEvent_UI_Hover UI_Hover { get; } = new();
		public static SoundEvent_MainMenu_Music MainMenu_Music { get; } = new();
		public static SoundEvent_Game_Machines_SliderStep Game_Machines_SliderStep { get; } = new();
		public static SoundEvent_Game_Machines_ButtonDown Game_Machines_ButtonDown { get; } = new();
		public static SoundEvent_Game_Machines_ButtonEnter Game_Machines_ButtonEnter { get; } = new();
		public static SoundEvent_Game_Machines_ButtonUp Game_Machines_ButtonUp { get; } = new();
		public static SoundEvent_Game_Machines_TutorialOpen Game_Machines_TutorialOpen { get; } = new();
		public static SoundEvent_Game_Map_ShipEngine Game_Map_ShipEngine { get; } = new();
		public static SoundEvent_Game_Music Game_Music { get; } = new();
		public static SoundEvent_Game_Machines_TutorialSlide Game_Machines_TutorialSlide { get; } = new();
		public static SoundEvent_Game_Map_MissilePing Game_Map_MissilePing { get; } = new();
		public static SoundEvent_Game_Machines_TutorialClose Game_Machines_TutorialClose { get; } = new();
		public static SoundEvent_Game_Machines_ButtonOff Game_Machines_ButtonOff { get; } = new();
		public static SoundEvent_Game_Machines_ButtonOn Game_Machines_ButtonOn { get; } = new();
		public static SoundEvent_Game_Machines_BitOne Game_Machines_BitOne { get; } = new();
		public static SoundEvent_Game_Machines_Sine Game_Machines_Sine { get; } = new();
		public static SoundEvent_Game_Lose Game_Lose { get; } = new();
		public static SoundEvent_Game_Machines_SignalSend Game_Machines_SignalSend { get; } = new();
		public static SoundEvent_Game_Map_MissileExplosion Game_Map_MissileExplosion { get; } = new();
		public static SoundEvent_Game_Machines_BitZero Game_Machines_BitZero { get; } = new();
		public static SoundEvent_Game_Win Game_Win { get; } = new();
		public static SoundEvent_Game_Machines_ScreenMessage Game_Machines_ScreenMessage { get; } = new();
		public static SoundEvent_Game_Machines_SignalSendFail Game_Machines_SignalSendFail { get; } = new();
    }

	public class SoundEvent_UI_Up : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 15;

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
		public float Length => 40;

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
		public float Length => 15;

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
		public float Length => 15;

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
		public float Length => 27;

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
		public bool IsOneShot => false;
		public float Length => 204768;

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

	public class SoundEvent_Game_Machines_SliderStep : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 15;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 232207952, Data2 = 1249842029, Data3 = 2018575238, Data4 = -1064773446 };

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

	public class SoundEvent_Game_Machines_ButtonDown : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 40;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 311207301, Data2 = 1222619873, Data3 = -1066547555, Data4 = -557252966 };

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

	public class SoundEvent_Game_Machines_ButtonEnter : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 27;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 775604104, Data2 = 1294667816, Data3 = 1560946090, Data4 = 2079152939 };

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

	public class SoundEvent_Game_Machines_ButtonUp : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 15;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 1221042098, Data2 = 1074104582, Data3 = 1901700509, Data4 = 33626182 };

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

	public class SoundEvent_Game_Machines_TutorialOpen : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 470;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = -1627508224, Data2 = 1176105701, Data3 = 156061848, Data4 = 1974053832 };

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

	public class SoundEvent_Game_Map_ShipEngine : ISoundEvent
	{
		public bool IsOneShot => false;
		public float Length => 2344;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = -1074782629, Data2 = 1144161177, Data3 = -1945357133, Data4 = -825333746 };

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

	public class SoundEvent_Game_Music : ISoundEvent
	{
		public bool IsOneShot => false;
		public float Length => 199608;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 922560125, Data2 = 1230402175, Data3 = 104905404, Data4 = 1795943507 };

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

	public class SoundEvent_Game_Machines_TutorialSlide : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 261;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = -1654945655, Data2 = 1147158340, Data3 = -1260304234, Data4 = -873281226 };

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

	public class SoundEvent_Game_Map_MissilePing : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 78;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = -839088425, Data2 = 1330336128, Data3 = -1904007040, Data4 = -1718072577 };

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

	public class SoundEvent_Game_Machines_TutorialClose : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 261;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = -91792257, Data2 = 1103781235, Data3 = 641284030, Data4 = -516686511 };

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

	public class SoundEvent_Game_Machines_ButtonOff : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 168;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = -907253717, Data2 = 1092231507, Data3 = 1684275385, Data4 = -169419285 };

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

	public class SoundEvent_Game_Machines_ButtonOn : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 168;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = -255747025, Data2 = 1253053082, Data3 = 1597848750, Data4 = -2030600704 };

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

	public class SoundEvent_Game_Machines_BitOne : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 157;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 1498553352, Data2 = 1114844110, Data3 = -930489979, Data4 = 2027090460 };

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

	public class SoundEvent_Game_Machines_Sine : ISoundEvent
	{
		public bool IsOneShot => false;
		public float Length => 2991;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 226336013, Data2 = 1157945867, Data3 = -779625072, Data4 = 1888198758 };

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

	public class SoundEvent_Game_Lose : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 1462;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = -1854121167, Data2 = 1135578920, Data3 = -312939390, Data4 = -1996759264 };

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

	public class SoundEvent_Game_Machines_SignalSend : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 950;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 2129679175, Data2 = 1325462934, Data3 = -154625621, Data4 = 1948394024 };

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

	public class SoundEvent_Game_Map_MissileExplosion : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 1008;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = -576373924, Data2 = 1297909591, Data3 = 905946534, Data4 = -646041395 };

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

	public class SoundEvent_Game_Machines_BitZero : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 110;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = -1252510065, Data2 = 1163043676, Data3 = -1319192149, Data4 = 1562956259 };

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

	public class SoundEvent_Game_Win : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 1671;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 801819793, Data2 = 1155554565, Data3 = 848573871, Data4 = -1912540242 };

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

	public class SoundEvent_Game_Machines_ScreenMessage : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 563;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 1550654449, Data2 = 1163558697, Data3 = -1027545442, Data4 = -1832235174 };

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

	public class SoundEvent_Game_Machines_SignalSendFail : ISoundEvent
	{
		public bool IsOneShot => true;
		public float Length => 891;

		private static readonly FMOD.GUID _guid = new FMOD.GUID() { Data1 = 508413180, Data2 = 1336040368, Data3 = 647653552, Data4 = -737891297 };

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