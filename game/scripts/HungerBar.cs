using Godot;
using Godot.NativeInterop;

public partial class HungerBar : Control
{
	[Export] protected Timer hungerFallofTimer;
	protected ColorRect HungerBarGraphic;
	protected Label hungerBarNumerValueLabel;

	const float MAX_HUNGER_BAR_WIDTH = 300;
	const float HUNGER_BAR_HEIGHT = 30;

	float hungerBarWidth = MAX_HUNGER_BAR_WIDTH;

	public override void _Ready()
	{
		HungerBarGraphic = this.GetChild<ColorRect>(0);
		hungerBarNumerValueLabel = this.GetNode<Label>("HungerBarNumberValue");
		HungerBarGraphic.Size = new(hungerBarWidth, HUNGER_BAR_HEIGHT);
		hungerFallofTimer.Timeout += OnHungerFallofTimerTimeoutSignal;
	}


	protected void OnHungerFallofTimerTimeoutSignal()
	{
		hungerBarWidth *= 0.9f;
		HungerBarGraphic.Size = new(hungerBarWidth, HUNGER_BAR_HEIGHT);
		hungerBarNumerValueLabel.Text = (hungerBarWidth/MAX_HUNGER_BAR_WIDTH).ToString();
		GD.Print(hungerBarWidth);
	}
}
