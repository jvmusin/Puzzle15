namespace Puzzle15
{
    public class ValidationResult
    {
        public bool Successful { get; }
        public string Cause { get; }

        private ValidationResult(bool successful, string cause)
        {
            Successful = successful;
            Cause = cause;
        }

        public static ValidationResult Success()
            => new ValidationResult(true, null);

        public static ValidationResult Unsuccess(string cause)
            => new ValidationResult(false, cause);
    }
}
