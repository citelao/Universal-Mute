using Windows.ApplicationModel.Calls;

var coordinator = VoipCallCoordinator.GetDefault();
coordinator.MuteStateChanged += (e, args) => {
    Console.WriteLine($"Mute changed! - {args.Muted}");

    // Respond that the app has muted/unmuted.
    if (args.Muted) {
        coordinator.NotifyMuted();
    } else {
        coordinator.NotifyUnmuted();
    }
};

Console.WriteLine("Press Enter to 'start' a call. Ctrl-C to exit.");
Console.ReadLine();

var call = coordinator.RequestNewOutgoingCall("context_link_todo", "Satya Nadella", "DummyPhone", VoipPhoneCallMedia.Audio);

// Unnecessary
// call.NotifyCallAccepted(VoipPhoneCallMedia.Audio);

// The call is active - mark a StartTime.
call.NotifyCallActive();
Console.WriteLine($"Call - {call.StartTime} {call.CallMedia} {call.ContactName}");
// Console.WriteLine($"Call - {call.CallMedia} {call.ContactName}");

Console.WriteLine("Press Enter to 'hold' the call.");
Console.ReadLine();
call.NotifyCallHeld();
Console.WriteLine($"Call - {call.StartTime} {call.CallMedia} {call.ContactName}");

Console.WriteLine("Press Enter to 'resume' the call.");
Console.ReadLine();
call.NotifyCallActive();
Console.WriteLine($"Call - {call.StartTime} {call.CallMedia} {call.ContactName}");

Console.WriteLine("Press Enter to 'end' the call.");
Console.ReadLine();
call.NotifyCallEnded();

// Crashes:
// Console.WriteLine($"Call - {call.StartTime} {call.CallMedia} {call.ContactName}");
