using CityBuilder.Client;

using Microsoft.JSInterop;

public class CanvasElement : HTMLElement {
	public CanvasElement(IJSObjectReference nativeElement, IJSRuntime js) : base(nativeElement, js) {
	}

	public ValueTask<int> GetWidth() => JS.InvokeAsync<int>("getProperty", _nativeElement, "width");
	public ValueTask SetWidth(int width) => JS.InvokeVoidAsync("setProperty", _nativeElement, "width", width);

	public ValueTask<int> GetHeight() => JS.InvokeAsync<int>("getProperty", _nativeElement, "height");
	public ValueTask SetHeight(int height) => JS.InvokeVoidAsync("setProperty", _nativeElement, "height", height);

	public async Task<T> GetContext<T>(string type) where T : RenderingContext {
		var context = await JS.InvokeAsync<IJSObjectReference>("callMethod", _nativeElement, "getContext", type);

		RenderingContext result = type switch {
			"2d" => new CanvasRenderingContext2D(context) {
				JS = JS
			},
			_ => throw new InvalidOperationException($"Unsupported context type {type}.")
		};

		return (T)result;
	}
}