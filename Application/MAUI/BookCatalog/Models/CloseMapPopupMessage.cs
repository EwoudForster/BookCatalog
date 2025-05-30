using CommunityToolkit.Mvvm.Messaging.Messages;

namespace BookCatalog.Models;

public class CloseMapPopupMessage(bool value) : ValueChangedMessage<bool>(value) { }
