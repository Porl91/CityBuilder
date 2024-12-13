using CityBuilder.Client;

using Microsoft.JSInterop;

namespace CityBuilder;

public class Window : HTMLElement {
	private Window(IJSObjectReference nativeElement, IJSRuntime js) : base(nativeElement, js) { }

	public static async Task<Window> Get(IJSRuntime js) {
		var window = await js.InvokeAsync<IJSObjectReference>("getWindow");

		return new Window(window, js);
	}
}