using Microsoft.JSInterop;

namespace CityBuilder.Client;

public class MouseEvent {
	public bool IsTrusted { get; set; }
	public int ScreenX { get; set; }
	public int ScreenY { get; set; }
	public int ClientX { get; set; }
	public int ClientY { get; set; }
	public bool CtrlKey { get; set; }
	public bool ShiftKey { get; set; }
	public bool AltKey { get; set; }
	public bool MetaKey { get; set; }
	public int Button { get; set; }
	public int Buttons { get; set; }
	public int PageX { get; set; }
	public int PageY { get; set; }
	public int X { get; set; }
	public int Y { get; set; }
	public int OffsetX { get; set; }
	public int OffsetY { get; set; }
	public int MovementX { get; set; }
	public int MovementY { get; set; }
	public int LayerX { get; set; }
	public int LayerY { get; set; }
	public int Detail { get; set; }
	public int Which { get; set; }
	public string Type { get; set; }
	public int EventPhase { get; set; }
	public bool Bubbles { get; set; }
	public bool Cancelable { get; set; }
	public bool DefaultPrevented { get; set; }
	public bool Composed { get; set; }
	public double TimeStamp { get; set; }
	public object ReturnValue { get; set; }
	public bool CancelBubble { get; set; }

	public ValueTask StopPropagation() {
		return NativeEvent.InvokeVoidAsync("stopPropagation");
	}

	public IJSObjectReference NativeEvent { get; set; }
}