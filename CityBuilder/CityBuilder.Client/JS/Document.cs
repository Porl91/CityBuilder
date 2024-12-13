using CityBuilder.Client;

using Microsoft.JSInterop;

namespace CityBuilder;

public class Document : HTMLElement {
	private Document(IJSObjectReference nativeElement, IJSRuntime js) : base(nativeElement, js) { }

	public async ValueTask<T> GetElementById<T>(string id) where T : HTMLElement {
		var nativeElement = await JS.InvokeAsync<IJSObjectReference>("callMethod", _nativeElement, "getElementById", id); ;

		if (nativeElement is null) {
			return null;
		}

		var tagName = await JS.InvokeAsync<string>("getProperty", nativeElement, "tagName");

		if (tagName == "CANVAS") {
			return new CanvasElement(nativeElement, JS) as T;
		}

		return new HTMLElement(nativeElement, JS) as T;
	}

	public static async Task<Document> Get(IJSRuntime js) {
		var document = await js.InvokeAsync<IJSObjectReference>("getDocument");

		return new Document(document, js);
	}
}