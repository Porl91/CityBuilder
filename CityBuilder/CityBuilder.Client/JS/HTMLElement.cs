using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CityBuilder.Client;

public class HTMLElement {
	[Inject]
	public IJSRuntime JS { get; set; }

	internal readonly IJSObjectReference _nativeElement;

	public HTMLElement(IJSObjectReference nativeElement, IJSRuntime js) {
		_nativeElement = nativeElement;

		JS = js;
	}

	public ValueTask AddEventListener<TEvent>(string eventName, string callbackName, object callingObject) {
		var dotnetHelper = DotNetObjectReference.Create(callingObject);

		return JS.InvokeVoidAsync("addElementEventListener", _nativeElement, eventName, callbackName, dotnetHelper);
	}

	public ValueTask<DOMRect> GetBoundingClientRect() {
		return _nativeElement.InvokeAsync<DOMRect>("getBoundingClientRect");
	}
}
