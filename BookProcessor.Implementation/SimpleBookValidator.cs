namespace BookProcessor.Implementation
{
    public class SimpleBookValidator : IBookValidator
    {
        private readonly ILogger _logger;

        public SimpleBookValidator(ILogger logger)
        {
            _logger = logger;
        }

        public bool Validate(string[] fields)
        {
            if (fields.Length != 2)
            {
                _logger.LogWarning($"Line malformed. Only {fields.Length} field(s) found.");
                return false;
            }

            if (!decimal.TryParse(fields[1], out _))
            {
                _logger.LogWarning($"Book price is not a valid decimal: '{fields[1]}'");
                return false;
            }

            return true;
        }
    }
}
