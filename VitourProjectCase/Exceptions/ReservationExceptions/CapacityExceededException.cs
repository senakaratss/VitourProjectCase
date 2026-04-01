namespace VitourProjectCase.Exceptions.ReservationExceptions
{
    public class CapacityExceededException:Exception
    {
        public CapacityExceededException(string message) : base(message)
        {
        }
    }
}
