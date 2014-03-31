/*
 *	Game State Splash
 *	This should be used to handle the Splash screen
 */
using UnityEngine;
using System.Collections;
using BoogieDownGames;

public sealed class GameStateMenu :  FSMState<GameMaster> {
	
	static readonly GameStateMenu instance = new GameStateMenu();
	public static GameStateMenu Instance
	{
		get { return instance; }
	}
	
	static GameStateMenu() { }
	private GameStateMenu() { }
	
	public override void Enter (GameMaster m)
	{
		Application.LoadLevel(1);
		Debug.LogError ("Im in Menu");
	}
	
	public override void ExecuteOnUpdate (GameMaster m)
	{
		
	}
	
	public override void ExecuteOnFixedUpdate(GameMaster m)
	{
		
	}
	
	public override void Exit(GameMaster m) 
	{
		
	}
}