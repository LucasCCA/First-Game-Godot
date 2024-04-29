using Godot;

public partial class Slime : Node2D
{
	public const float Speed = 60f;
	private int _direction = 1;
	private RayCast2D _rayCastRight;
	private RayCast2D _rayCastLeft;
	private AnimatedSprite2D _animatedSprite;

	public override void _Ready()
	{
		_rayCastRight = GetNode<RayCast2D>("RayCastRight");
		_rayCastLeft = GetNode<RayCast2D>("RayCastLeft");
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_rayCastRight.IsColliding())
		{
			_direction = -1;
			_animatedSprite.FlipH = true;
		}

		if (_rayCastLeft.IsColliding())
		{
			_direction = 1;
			_animatedSprite.FlipH = false;
		}

		Vector2 position = Position;
		position.X += _direction * Speed * (float)delta;
		Position = position;
	}
}
