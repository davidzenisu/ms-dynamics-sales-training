public class ConsoleReader : IDynamicsToolInput
{
    public ConsoleReader(IDynamicsToolLogger logger) {
        _logger = logger;
    }
    
    private IDynamicsToolLogger _logger;
    private bool _isInterActive = true;

    private void ValidateInteractiveMode<T>(T? defaultValue) {
        if (defaultValue != null  || _isInterActive) {
           return;
        }
         var errorMessage = "Interactive mode is active but no default input was provided!";
        _logger.WriteMessage(errorMessage, LoggerFormatOptions.Error);
        throw new Exception(errorMessage);
    }

    public char GetCharacterInput(char? defaultInput = null)
    {
        ValidateInteractiveMode(defaultInput);
        if (defaultInput != null && !_isInterActive) {
            return defaultInput.Value;
        }
        var input = Console.ReadKey(true);
        return input.KeyChar;
    }

    public bool GetConfirmationInput(bool? defaultInput = null)
    {
        ValidateInteractiveMode(defaultInput);
        if (defaultInput != null && !_isInterActive) {
            return defaultInput.Value;
        }
        bool confirmed;
        ConsoleKey response;
        _logger.WriteMessage("Please confirm [y/n] ", LoggerFormatOptions.Prompt);
        var isValidInput = false;
        do
        {
            response = Console.ReadKey(false).Key;
            if (response != ConsoleKey.Enter)
                Console.WriteLine();
            isValidInput = response == ConsoleKey.Y || response == ConsoleKey.N;
            if (!isValidInput)
            {
                _logger.WriteMessage("No valid entry, please try again!", LoggerFormatOptions.Warning);
            }
            confirmed = response == ConsoleKey.Y;
        } while (!isValidInput);
        return confirmed;
    }

    public string GetStringInput(string? defaultInput = null)
    {
        ValidateInteractiveMode(defaultInput);
        if (defaultInput != null && !_isInterActive) {
            return defaultInput;
        }
        return Console.ReadLine() ?? String.Empty;
    }

    public void SetInteractiveMode(bool isInterActive)
    {
        _isInterActive = isInterActive;
    }

}