using UnityEngine;

namespace EpicToonFX
{
	public class ETFXPitchRandomizer : MonoBehaviour
	{
		[SerializeField] private float _minPitchValue;
		[SerializeField] private float _maxPitchValue;

		public void ChangePitch(AudioSource audioSource)
		{
			audioSource.pitch = Random.Range(_minPitchValue, _maxPitchValue);
		}
	}
}