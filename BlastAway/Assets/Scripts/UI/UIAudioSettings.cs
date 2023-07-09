using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAudioSettings : MonoBehaviour {

	[SerializeField] private PlayerController player;
	[SerializeField] private GameObject audioCanvas;

	[SerializeField] private AudioSource musicSource;
	[SerializeField] private AudioSource environmentSource;

	[SerializeField] private Slider musicSlider;
	[SerializeField] private Slider environmentSlider;

	[SerializeField] private TMP_Text musicText;
	[SerializeField] private TMP_Text environmentText;

	[SerializeField] private KeyCode key;
	private bool isActive = false;

	private void Update() {
		if (Input.GetKeyDown(key)) {
			if (isActive) {
				audioCanvas.SetActive(false);
				OnDisableCanvas();
				isActive = false;
			}
			else {
				audioCanvas.SetActive(true);
				OnEnableCanvas();
				isActive = true;
			}
		}
	}

	private void OnEnableCanvas() {
		player.SetCanMove(false);
		Time.timeScale = 0;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		float volumeMusic = musicSource.volume;
		musicText.text = volumeMusic.ToString("0.0");
		musicSlider.value = volumeMusic;
		float volumeEnvironment = environmentSource.volume;
		environmentText.text = environmentSource.volume.ToString("0.0");
		environmentSlider.value = volumeEnvironment;
	}

	private void OnDisableCanvas() {
		player.SetCanMove(true);
		Time.timeScale = 1;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	public void SetVolumeMusic(float value) {
		musicText.text = musicSlider.value.ToString("0.0");
		float volumeMusic = musicSlider.value;
		PlayerPrefs.SetFloat("VolumeMusic", volumeMusic);
		LoadValues();
	}

	public void SetVolumeEnvironment(float value) {
		environmentText.text = environmentSlider.value.ToString("0.0");
		float volumeEnvironment = environmentSlider.value;
		PlayerPrefs.SetFloat("VolumeEnvironment", volumeEnvironment);
		LoadValues();
	}
	
	private void LoadValues() {
		float musicValue = PlayerPrefs.GetFloat("VolumeMusic");
		float environmentValue = PlayerPrefs.GetFloat("VolumeEnvironment");

		musicSlider.value = musicValue;
		environmentSlider.value = environmentValue;

		musicSource.volume = musicValue;
		environmentSource.volume = environmentValue;
	}

	public void CloseMenu() {
		audioCanvas.SetActive(false);
		isActive = false;
		OnDisableCanvas();
	}
}