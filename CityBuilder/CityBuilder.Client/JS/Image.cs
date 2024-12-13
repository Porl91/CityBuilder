using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CityBuilder.Client;

public class Image {
	[Inject]
	public required IJSRuntime JS { get; set; }

	internal readonly IJSObjectReference _nativeElement;

	private Image(IJSObjectReference nativeElement) {
		_nativeElement = nativeElement;
	}

	public static async Task<Image> FromURL(string url, IJSRuntime js) {
		var image = await js.InvokeAsync<IJSObjectReference>("loadImage", url);

		return new Image(image) {
			JS = js
		};
	}
}
