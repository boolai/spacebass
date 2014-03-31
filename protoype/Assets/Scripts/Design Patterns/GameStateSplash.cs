/*
 *	Game State Splash
 *	This should be used to handle the Splash screen
 */
using UnityEngine;
using System.Collections;
using BoogieDownGames;

public sealed class GameStateSplash :  FSMState<GameMaster> {
	
	static readonly GameStateSplash instance = new GameStateSplash();
	public static GameStateSplash Instance
	{
		get { return instance; }
	}
	
	static GameStateSplash() { }
	private GameStateSplash() { }
		
	public override void Enter (GameMaster m)
	{
		Application.LoadLevel(0);
		Debug.LogError ("Im in splash");
		m.m_splashScreenTimer.startClock();
	}
	
	public override void ExecuteOnUpdate (GameMaster m)
	{

		m.m_splashScreenTimer.run();
		if(m.m_splashScreenTimer.IsDone) {
			m.FSM.ChangeState(GameStateMenu.Instance);
		}
		if(Input.anyKey) {
			m.FSM.ChangeState(GameStateMenu.Instance);
		}

	}

	public override void ExecuteOnFixedUpdate(GameMaster m)
	{

	}
	
	public override void Exit(GameMaster m) 
	{
		Debug.LogError("Im leaving");

	}
}
