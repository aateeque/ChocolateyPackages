///////////////////////////////////////////////////////////////////////////////
// ADDINS
///////////////////////////////////////////////////////////////////////////////

#addin nuget:?package=Cake.Gitter&version=0.2.0

///////////////////////////////////////////////////////////////////////////////
// HELPER METHODS
///////////////////////////////////////////////////////////////////////////////

public void SendMessageToGitterRoom(string message)
{
    try
    {
        Information("Sending message to Gitter...");

        if(string.IsNullOrEmpty(parameters.Gitter.Token)) {
            throw new InvalidOperationException("Could not resolve Gitter Token.");
        }

        if(string.IsNullOrEmpty(parameters.Gitter.RoomId)) {
            throw new InvalidOperationException("Could not resolve Gitter Room Id.");
        }

        var postMessageResult = Gitter.Chat.PostMessage(
                    message: message,
                    messageSettings: new GitterChatMessageSettings { Token = parameters.Gitter.Token, RoomId = parameters.Gitter.RoomId}
            );

        if (postMessageResult.Ok)
        {
            Information("Message {0} succcessfully sent", postMessageResult.TimeStamp);
        }
        else
        {
            Error("Failed to send message: {0}", postMessageResult.Error);
        }
    }
    catch(Exception ex)
    {
        Error("{0}", ex);
    }
}