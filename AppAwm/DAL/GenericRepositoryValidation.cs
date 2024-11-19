namespace AppAwm.DAL
{
    public class GenericRepositoryValidation
    {
        public enum GenericRepositoryExceptionStatus
        {
            Success = 0,
            ArgumentNullException = 1,
            SqlException = 2
        }
    }
}
