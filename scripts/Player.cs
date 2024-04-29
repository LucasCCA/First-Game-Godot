using Godot;

public partial class Player : CharacterBody2D
{
	public const float Speed = 130.0f;
	public const float JumpVelocity = -300.0f;
	private AnimatedSprite2D _animatedSprite;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{ 
			velocity.Y += gravity * (float)delta;
        }

        // Handle Jump.
        if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{ 
			velocity.Y = JumpVelocity;
        }

        // Get the input direction : -1, 0, 1
        float direction = Input.GetAxis("move_left", "move_right");

		// Flip the Sprite
		if (direction > 0)
		{
			_animatedSprite.FlipH = false;
		}
		else if (direction < 0)
		{
			_animatedSprite.FlipH = true;
		}

		// Play animations
		if (IsOnFloor())
		{
			if (direction == 0)
			{
				_animatedSprite.Play("idle");
			}
			else
			{
				_animatedSprite.Play("run");
			}
		}
		else
		{
			_animatedSprite.Play("jump");
		}

		// Apply movement
		if (direction != 0)
		{
			velocity.X = direction * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
