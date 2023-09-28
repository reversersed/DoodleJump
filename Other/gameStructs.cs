using System;

namespace Doodle_Jump
{
	//Для игровых событий
	struct gameEvents
	{
		public int lastEventScore;
		public eventType lastEvent;
		public System.Collections.Generic.List<eventType> eventCompleted;
	}
	enum eventType
	{
		eventNull = 0,
		eventFading,
		eventMoving
	};
	//Статус игры
	enum gameStatus
	{
		inMenu = 0,
		gameRunning,
		gamePaused,
		gameFalling,
		gameOver,
		recordsTable
	}
	//Типы платформ и пересечений
	public enum platformType
	{
		Null = 0,
		Regular,
		Breakable,
		Moving,
		Fading,
		Depending
	}
	public enum IntersectType
	{
		Null = 0,
		Platform,
		Spring,
		Tramp,
		Jetpack
	}
}
