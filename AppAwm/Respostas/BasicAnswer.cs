namespace AppAwm.Respostas
{
    public class BasicAnswer(bool success, string message)
    {
        public string Message { get; init; } = message;

        public bool Success { get; init; } = success;
    }
}
