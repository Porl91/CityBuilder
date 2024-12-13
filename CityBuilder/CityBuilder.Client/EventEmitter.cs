using CityBuilder.Client;

using Microsoft.JSInterop;

using System.Runtime.CompilerServices;

namespace CityBuilder;

public class EventEmitter<TEvent> : IAsyncDisposable {
	private readonly IJSRuntime _jsRuntime;
	private readonly string _emitterId;
	private readonly DotNetObjectReference<EventEmitter<TEvent>> _dotNetHelper;
	private readonly Queue<TEvent> _eventQueue = new();
	private readonly SemaphoreSlim _semaphore = new(0);

	private bool _isDisposed;

	public EventEmitter(IJSRuntime jsRuntime, HTMLElement element, string eventName) {
		_jsRuntime = jsRuntime;
		_emitterId = Guid.NewGuid().ToString();
		_dotNetHelper = DotNetObjectReference.Create(this);

		_jsRuntime.InvokeVoidAsync("EventEmitter.createEmitter", element._nativeElement, eventName, _emitterId, _dotNetHelper);
	}

	public async IAsyncEnumerable<TEvent> GetEventsAsync([EnumeratorCancellation] CancellationToken cancellationToken = default) {
		while (!cancellationToken.IsCancellationRequested && !_isDisposed) {
			await _semaphore.WaitAsync(cancellationToken);

			TEvent ev = default;

			lock (_eventQueue) {
				if (_eventQueue.Count > 0) {
					ev = _eventQueue.Dequeue();
				}
			}

			if (ev is not null) {
				yield return ev;
			}
		}
	}

	[JSInvokable]
	public void OnEvent(TEvent ev) {
		lock (_eventQueue) {
			_eventQueue.Enqueue(ev);
		}

		_semaphore.Release();
	}

	public async ValueTask DisposeAsync() {
		if (_isDisposed) {
			return;
		}

		await _jsRuntime.InvokeVoidAsync("EventEmitter.removeEmitter", _emitterId);

		_isDisposed = true;
	}
}