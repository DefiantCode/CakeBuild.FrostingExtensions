namespace DefiantCode.Cake.Frosting
{
    public class ValidateOptions
    {
        public bool SolutonFileMustExistInRoot { get; set; }

        public static ValidateOptions Default => new ValidateOptions { SolutonFileMustExistInRoot = false };
    }
}