/*
 * Misael Aponte Feb 4, 2014
 * The Game Master Script; Controls the game flow
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace BoogieDownGames {

	public class GameMaster : UnitySingletonPersistent<GameMaster>
	{
		enum GameLevel { Easy, Med, Hard, Harder, Killer };

		[SerializeField]
		private GameLevel m_gameLevel = GameLevel.Easy;

		[SerializeField]
		private bool m_isPaused;

		[SerializeField]
		private FiniteStateMachine<GameMaster> m_fsm = new FiniteStateMachine<GameMaster>();

		[SerializeField]
		private int m_playerScore;

		[SerializeField]
		private int m_playerHealth;

		[SerializeField]
		public TimeKeeper m_splashScreenTimer;

		[SerializeField]
		public List<AudioClip> m_sounds;

		[SerializeField]
		public AudioSource m_audioSource;


		public FiniteStateMachine<GameMaster> FSM
		{
			get { return m_fsm; }
			set { m_fsm = value; }
		}

		#region Properties

		public int PlayerHealth
		{
			get { return m_playerHealth; }
			set { m_playerHealth = value; }
		}

		public int PlayerScore
		{
			get{ return m_playerScore; }
			set { m_playerScore = value; }
		}

		#endregion

		// Use this for initialization
		void Start ()
		{
			Debug.LogError("Staring off");
			//load();
			//Always start off in the splash state
			m_fsm.Configure(this,GameStateSplash.Instance);
			m_audioSource = GetComponent<AudioSource>();

		}
		
		// Update is called once per frame
		void Update () 
		{
			m_fsm.runOnUpdate();
		}

		void FixedUpdate ()
		{
			m_fsm.runOnFixedUpdate();
		}

		public void sendMessage(string p_funcCall, string p_id, string p_data)
		{
			Hashtable dat = new Hashtable();
			dat.Add("stringID",p_id);
			dat.Add("data",p_data);
			NotificationCenter.DefaultCenter.PostNotification(this,p_funcCall,dat);
		}

		public void nextScene()
		{
			Application.LoadLevel(Application.loadedLevel + 1);
		}


		public void gameModePause()
		{
			Time.timeScale = 0.0f;
			m_isPaused = true;
		}

		public void gameModeRun() 
		{
			Time.timeScale = 1.0f;
			m_isPaused = false;
		}

		public void playAudio(int p_index)
		{
			//m_audioSource.PlayOneShot(m_sounds[p_index]);
		}

		public void save()
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream fs = File.Open(Application.persistentDataPath + "/gameSave.dat",FileMode.Create);
			SaveData dat = new SaveData();
			dat.m_playerScore = 100;
			bf.Serialize(fs,dat);
			fs.Close();

		}

		public void load()
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream fs = File.Open(Application.persistentDataPath + "/gameSave.dat",FileMode.Open);
			SaveData dat = (SaveData)bf.Deserialize(fs);
			fs.Close();

			//update data
			m_playerScore = dat.m_playerScore;
		}

		public void gotToGame()
		{
			Application.LoadLevel(2);
		}

		public void quitGame()
		{
			//so a save first
			//save();

			Application.Quit();
		}

	}

	[Serializable]
	public class SaveData
	{
		public int m_playerScore;
	}
}