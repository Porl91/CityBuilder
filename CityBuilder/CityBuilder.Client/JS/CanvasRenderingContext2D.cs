using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CityBuilder.Client;

public class CanvasRenderingContext2D : RenderingContext {
	[Inject]
	public required IJSRuntime JS { get; set; }

	private readonly IJSObjectReference _nativeElement;

	public CanvasRenderingContext2D(IJSObjectReference nativeElement) {
		_nativeElement = nativeElement;
	}

	public ValueTask<string> GetFillStyle() => JS.InvokeAsync<string>("getProperty", _nativeElement, "fillStyle");
	public ValueTask SetFillStyle(string value) => JS.InvokeVoidAsync("setProperty", _nativeElement, "fillStyle", value);

	public ValueTask FillRect(int x, int y, int width, int height) => JS.InvokeVoidAsync("callMethod", _nativeElement, "fillRect", x, y, width, height);

	public ValueTask DrawImage(Image image,
		int sx, int sy, int sw, int sh,
		int dx, int dy, int dw, int dh) => JS.InvokeVoidAsync("callMethod", _nativeElement, "drawImage", image._nativeElement,
		sx, sy, sw, sh,
		dx, dy, dw, dh);

	public ValueTask DrawImage(Image image, int x, int y, int w, int h) =>
		JS.InvokeVoidAsync("callMethod", _nativeElement, "drawImage", image._nativeElement, x, y, w, h);

	public ValueTask DrawImage(Image image, int x, int y) =>
		JS.InvokeVoidAsync("callMethod", _nativeElement, "drawImage", image._nativeElement, x, y);
}
