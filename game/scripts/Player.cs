using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public float dashSpeepModifier = 1;
	protected bool isAllowedToDash = true;

	[Export] protected Timer dashCooldownTimer;

	public override void _Ready()
	{
		dashCooldownTimer.Timeout += OnDashCooldownTimerTimeoutSignal;
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		dashSpeepModifier = Mathf.Lerp(dashSpeepModifier, 1, 0.3f);
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("left", "right", "up", "down");
		if (direction != Vector2.Zero)
		{
			velocity = direction * Speed * dashSpeepModifier;
		}
		else
		{
			velocity = Lerp(Velocity, new(0,0), 0.5f);
		}

		if (Input.IsActionJustPressed("dash") && isAllowedToDash)
		{
			GD.Print("Dash");
			dashSpeepModifier += 5;
			isAllowedToDash = false;
			dashCooldownTimer.Start();
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	public Vector2 Lerp(Vector2 origin, Vector2 destination, float speed)
	{
		return new Vector2(Mathf.Lerp(origin.X, destination.X, speed), Mathf.Lerp(origin.Y, destination.Y, speed));
	}

	protected void OnDashCooldownTimerTimeoutSignal()
	{
		GD.Print("Dash cooldown end");
		isAllowedToDash = true;
	}
}
