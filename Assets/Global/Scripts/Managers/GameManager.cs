using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
	#region Properties
	private CardGame _currentGame = null;
	private PlayerController _playerController;
	private Wheeler _wheeler;
	private Camera _camera;

	public int deckCount = 1;
	#endregion

	#region MonoBehaviour
	private void Awake() => Set(this);
	private void OnDestroy() => Release();

	private void Start()
	{
		
	}

	private void Update()
	{
		
	}
	#endregion

	#region GettersSetters
	public PlayerController GetPlayerController()
	{
		_playerController ??= FindAnyObjectByType<PlayerController>() ?? new GameObject().AddComponent<PlayerController>();

		return _playerController;
	}
	public Wheeler GetWheeler()
	{
		_wheeler ??= new();

		return _wheeler;
	}
	#endregion

	#region Game Handling
	public enum Game { Blackjack, Poker, GoFish, Solitaire }
	public void NewGame(Game game)
	{
		switch (game)
		{
			case Game.Blackjack:
				_currentGame = new Blackjack(deckCount);
				break;
			case Game.Poker:
				_currentGame = new Poker(deckCount);
				break;
			case Game.GoFish:
				_currentGame = new GoFish(deckCount);
				break;
			case Game.Solitaire:
			//_currentGame = new Solitaire();
			default:
				break;
		}
	}
	#endregion
	#region Player Handling

	#endregion
}
