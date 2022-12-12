using BlogCity.Models;


namespace BlogCity

{
    public class RecipientFactory
    {
        public Recipient factory(int ID, string Type, string Contact)
        {
            Console.WriteLine($"{ID}, {Type}, {Contact}");

            if (Type == "Email")
            {
                return new EmailRecipient(ID, Type, Contact);
            }
            return null;
        }
    }
}
